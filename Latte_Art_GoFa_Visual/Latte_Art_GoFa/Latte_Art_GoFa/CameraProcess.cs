using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;                 // <— thêm
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.RapidDomain;
using Timer = System.Windows.Forms.Timer;

namespace DetectCoffeeCup
{
    public partial class CameraProcess : Form
    {
        // ===== PC SDK (giữ nguyên) =====
        private Controller _controller;
        private Timer _pollingTimer;
        private RapidData _triggerDetectVar;
        private RapidData _detectCupFlagVar;

        // ===== Camera & hiển thị =====
        private VideoCapture _capture;
        private volatile bool _isDetecting = false;
        private PictureBox _display;

        // ===== Debounce/temporal smoothing (từ code 1) =====
        private int _presentStreak = 0;
        private int _absentStreak = 0;
        private bool _cupPresent = false;

        private const int REQUIRED_PRESENT_FRAMES = 3; // >=3 frame liên tiếp => CÓ ly
        private const int REQUIRED_ABSENT_FRAMES = 5;  // >=5 frame liên tiếp => KO ly

        public CameraProcess(Controller controller)
        {
            InitializeComponent();
            _controller = controller;

            try
            {
                // Access RAPID variables
                var task = _controller.Rapid.GetTask("T_ROB1");
                _triggerDetectVar = task.GetRapidData("CalibData", "triggerDetect");
                _detectCupFlagVar = task.GetRapidData("CalibData", "detectCupFlag");

                // Polling RAPID trigger
                _pollingTimer = new Timer { Interval = 500 };
                _pollingTimer.Tick += PollTriggerDetect;
                _pollingTimer.Start();

                // Panel hiển thị
                panelCamera.Location = new Point(12, 12);
                panelCamera.Size = new Size(371, 300);
                panelCamera.TabIndex = 0;

                _display = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                panelCamera.Controls.Add(_display);

                // Khởi tạo camera (giữ nguyên index 1 như code 2)
                _capture = new VideoCapture(1);
                if (!_capture.IsOpened)
                {
                    MessageBox.Show("Camera not available or cannot be opened.", "Camera Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                // Đảm bảo không đăng ký event 2 lần
                _capture.ImageGrabbed -= ProcessFrame;
                _capture.ImageGrabbed += ProcessFrame;
                _capture.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Initialization error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        // ===== Poll ABB triggerDetect để chụp & detect =====
        private void PollTriggerDetect(object sender, EventArgs e)
        {
            try
            {
                bool trigger = ((Bool)_triggerDetectVar.Value).Value;
                if (trigger)
                {
                    _pollingTimer.Stop();
                    bool cupDetected = CaptureAndDetect();

                    using (Mastership.Request(_controller))
                    {
                        _detectCupFlagVar.Value = new Bool(cupDetected);
                        _triggerDetectVar.Value = new Bool(false); // reset trigger
                    }

                    _pollingTimer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Polling error: " + ex.Message);
            }
        }

        // ===== Chụp liên tục tới khi trạng thái ổn định CÓ ly =====
        private bool CaptureAndDetect()
        {
            _isDetecting = true;
            try
            {
                // reset debounce trước mỗi lần detect “đến khi thấy”
                _presentStreak = 0;
                _absentStreak = 0;
                _cupPresent = false;

                while (true)
                {
                    if (IsDisposed || _capture == null || !_capture.IsOpened)
                        return false;

                    using (Mat frame = new Mat())
                    {
                        bool ok = _capture.Read(frame); // lấy frame MỚI
                        if (ok && !frame.IsEmpty)
                        {
                            // AnalyzeFrame sẽ tự cập nhật _cupPresent bằng debounce
                            var _ = AnalyzeFrame(frame);

                            if (_cupPresent)     // đã đạt ngưỡng ổn định
                                return true;
                        }
                    }

                    Application.DoEvents();
                    System.Threading.Thread.Sleep(20);
                }
            }
            finally
            {
                _isDetecting = false;
            }
        }

        // ===== Stream bình thường: cứ có frame là phân tích & vẽ =====
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (_isDetecting) return; // khi đang detect blocking thì bỏ qua event

            using (Mat frame = new Mat())
            {
                _capture.Retrieve(frame);
                if (!frame.IsEmpty) AnalyzeFrame(frame);
            }
        }

        /// <summary>
        /// Xử lý khung, tìm ứng viên ly (code 1), debounce trạng thái, vẽ trục & offset, cập nhật UI.
        /// Trả về danh sách (Center, Radius) để tương thích với code 2 (dù logic chính dùng _cupPresent).
        /// </summary>
        private List<(Point Center, int Radius)> AnalyzeFrame(Mat frame)
        {
            var detected = new List<(Point Center, int Radius)>();
            Mat output = frame.Clone();

            // ===== Vẽ trục toạ độ (0,0) ở giữa khung) =====
            int cx = frame.Width / 2;
            int cy = frame.Height / 2;

            CvInvoke.Line(output, new Point(cx, 0), new Point(cx, frame.Height - 1),
                          new MCvScalar(255, 255, 255), 1);               // Oy
            CvInvoke.Line(output, new Point(0, cy), new Point(frame.Width - 1, cy),
                          new MCvScalar(255, 255, 255), 1);               // Ox
            CvInvoke.Circle(output, new Point(cx, cy), 3, new MCvScalar(255, 255, 255), -1);
            CvInvoke.PutText(output, "0,0", new Point(cx + 6, cy - 6),
                             FontFace.HersheySimplex, 0.5, new MCvScalar(255, 255, 255), 1);

            try
            {
                var candidates = new List<(Point Center, int Radius, double Score)>();

                using (Mat gray = new Mat())
                {
                    CvInvoke.CvtColor(frame, gray, ColorConversion.Bgr2Gray);
                    CvInvoke.GaussianBlur(gray, gray, new Size(5, 5), 0);

                    using (Mat edges = new Mat())
                    {
                        CvInvoke.Canny(gray, edges, 40, 120);

                        using var contours = new VectorOfVectorOfPoint();
                        CvInvoke.FindContours(edges, contours, null,
                                              RetrType.External, ChainApproxMethod.ChainApproxSimple);

                        // Ngưỡng theo tỉ lệ kích thước ảnh
                        double imgArea = frame.Width * frame.Height;
                        double minArea = 0.003 * imgArea; // ~0.3%
                        double maxArea = 0.30 * imgArea;  // ~30%

                        for (int i = 0; i < contours.Size; i++)
                        {
                            using var contour = contours[i];
                            if (contour.Size < 5) continue;

                            double area = CvInvoke.ContourArea(contour);
                            if (area < minArea || area > maxArea) continue;

                            // Loại contour chạm viền
                            var rect = CvInvoke.BoundingRectangle(contour);
                            if (rect.X <= 1 || rect.Y <= 1 ||
                                rect.Right >= frame.Width - 2 || rect.Bottom >= frame.Height - 2)
                                continue;

                            // Circularity
                            double perim = CvInvoke.ArcLength(contour, true);
                            if (perim <= 1e-6) continue;
                            double circularity = 4.0 * Math.PI * area / (perim * perim);
                            if (circularity < 0.70) continue;

                            // Solidity
                            using var hull = new VectorOfPoint();
                            CvInvoke.ConvexHull(contour, hull, true);
                            double hullArea = CvInvoke.ContourArea(hull);
                            if (hullArea <= 1e-6) continue;
                            double solidity = area / hullArea;
                            if (solidity < 0.90) continue;

                            // Gần-tròn theo ellipse ratio
                            RotatedRect ellipse = CvInvoke.FitEllipse(contour);
                            double ratio = ellipse.Size.Width / Math.Max(1e-6, ellipse.Size.Height);
                            if (ratio < 0.85 || ratio > 1.20) continue;

                            var center = Point.Round(ellipse.Center);
                            int radius = (int)(Math.Max(ellipse.Size.Width, ellipse.Size.Height) / 2.0);

                            int minR = Math.Min(frame.Width, frame.Height) / 30;
                            int maxR = Math.Min(frame.Width, frame.Height) / 2;
                            if (radius < minR || radius > maxR) continue;

                            // Kiểm tra tương phản trong/ngoài
                            using var mask = new Mat(frame.Size, DepthType.Cv8U, 1);
                            mask.SetTo(new MCvScalar(0));
                            CvInvoke.Circle(mask, center, Math.Max(1, radius - 2), new MCvScalar(255), -1);

                            MCvScalar meanIn = CvInvoke.Mean(gray, mask);
                            using var maskInv = new Mat();
                            CvInvoke.BitwiseNot(mask, maskInv);
                            MCvScalar meanOut = CvInvoke.Mean(gray, maskInv);

                            if (!(meanIn.V0 > meanOut.V0 + 8)) continue;

                            double score = circularity * (meanIn.V0 - meanOut.V0);
                            candidates.Add((center, radius, score));
                        }

                        // Fallback: HoughCircles nếu chưa có ứng viên
                        if (candidates.Count == 0)
                        {
                            using var thr = new Mat();
                            CvInvoke.Threshold(gray, thr, 0, 255,
                                ThresholdType.Otsu | ThresholdType.Binary);

                            int minR = Math.Min(frame.Width, frame.Height) / 30;
                            int maxR = Math.Min(frame.Width, frame.Height) / 2;

                            var circles = CvInvoke.HoughCircles(
                                thr, HoughModes.Gradient,
                                dp: 1.2, minDist: frame.Height / 6,
                                param1: 180, param2: 28,
                                minRadius: minR, maxRadius: maxR);

                            foreach (var c in circles)
                            {
                                var center = new Point((int)c.Center.X, (int)c.Center.Y);
                                int radius = (int)c.Radius;

                                using var mask = new Mat(frame.Size, DepthType.Cv8U, 1);
                                mask.SetTo(new MCvScalar(0));
                                CvInvoke.Circle(mask, center, Math.Max(1, radius - 2), new MCvScalar(255), -1);

                                MCvScalar meanIn = CvInvoke.Mean(gray, mask);
                                using var maskInv = new Mat();
                                CvInvoke.BitwiseNot(mask, maskInv);
                                MCvScalar meanOut = CvInvoke.Mean(gray, maskInv);

                                if (meanIn.V0 > meanOut.V0 + 8)
                                {
                                    double score = meanIn.V0 - meanOut.V0;
                                    candidates.Add((center, radius, score));
                                }
                            }
                        }
                    }
                }

                // Chọn ứng viên tốt nhất & cập nhật debounce
                (Point Center, int Radius)? best = null;
                if (candidates.Count > 0)
                {
                    candidates.Sort((a, b) => b.Score.CompareTo(a.Score));
                    best = (candidates[0].Center, candidates[0].Radius);
                    detected = candidates.Select(c => (c.Center, c.Radius)).ToList();
                }

                if (best.HasValue)
                {
                    _presentStreak++;
                    _absentStreak = 0;
                    if (!_cupPresent && _presentStreak >= REQUIRED_PRESENT_FRAMES)
                        _cupPresent = true;
                }
                else
                {
                    _absentStreak++;
                    _presentStreak = 0;
                    if (_cupPresent && _absentStreak >= REQUIRED_ABSENT_FRAMES)
                        _cupPresent = false;
                }

                // Vẽ kết quả + offset (tọa độ có dấu) hoặc “No cup”
                if (_cupPresent && best.HasValue)
                {
                    var center = best.Value.Center;
                    int radius = best.Value.Radius;

                    CvInvoke.Circle(output, center, radius, new MCvScalar(0, 255, 0), 2);
                    CvInvoke.PutText(output, "Cup",
                        new Point(center.X - radius, center.Y - radius - 5),
                        FontFace.HersheySimplex, 0.6, new MCvScalar(255, 0, 0), 2);

                    int dx = center.X - cx;     // phải dương, trái âm
                    int dy = cy - center.Y;     // ảnh y xuống dương => đảo dấu để “lên” dương
                    double dist = Math.Sqrt(dx * dx + dy * dy);

                    CvInvoke.Line(output, new Point(cx, cy), center, new MCvScalar(0, 255, 0), 1);

                    string txt = $"Offset (px): x={(dx >= 0 ? "+" : "")}{dx}, y={(dy >= 0 ? "+" : "")}{dy} | r={dist:F1}";
                    CvInvoke.PutText(output, txt,
                        new Point(10, 25),
                        FontFace.HersheySimplex, 0.6, new MCvScalar(0, 255, 255), 2);
                }
                else
                {
                    CvInvoke.PutText(output, "No cup",
                        new Point(10, 25),
                        FontFace.HersheySimplex, 0.7, new MCvScalar(0, 0, 255), 2);
                }

                // ===== Cập nhật UI an toàn, không rò bộ nhớ =====
                var bmp = output.ToImage<Bgr, byte>().ToBitmap();
                output.Dispose();

                if (!IsDisposed && _display?.IsDisposed == false)
                {
                    Invoke(new Action(() =>
                    {
                        if (_display?.IsDisposed == false)
                        {
                            _display.Image?.Dispose();
                            _display.Image = bmp;
                        }
                        else
                        {
                            bmp.Dispose();
                        }
                    }));
                }
                else
                {
                    bmp.Dispose();
                }
            }
            catch (Exception ex)
            {
                output.Dispose();
                MessageBox.Show("Analyze error: " + ex.Message);
            }

            return detected;
        }

        // ===== Cleanup (giữ nguyên + dọn timer) =====
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (_capture != null)
                {
                    _capture.ImageGrabbed -= ProcessFrame;
                    _capture.Stop();
                    _capture.Dispose();
                    _capture = null;
                }

                if (_pollingTimer != null)
                {
                    _pollingTimer.Stop();
                    _pollingTimer.Tick -= PollTriggerDetect;
                    _pollingTimer.Dispose();
                    _pollingTimer = null;
                }

                _display?.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cleanup error: " + ex.Message);
            }

            base.OnFormClosing(e);
        }

        private void panelCamera_Paint(object sender, PaintEventArgs e)
        {
            // để trống
        }
    }
}

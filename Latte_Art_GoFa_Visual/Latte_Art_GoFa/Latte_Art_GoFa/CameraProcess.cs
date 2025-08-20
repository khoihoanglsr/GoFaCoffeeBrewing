using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.RapidDomain;
using Timer = System.Windows.Forms.Timer;

namespace DetectCoffeeCup
{
    public partial class CameraProcess : Form
    {
        private Controller _controller;
        private Timer _pollingTimer;
        private RapidData _triggerDetectVar;
        private RapidData _detectCupFlagVar;

        // VideoCapture object for accessing the camera
        private VideoCapture _capture;
        private volatile bool _isDetecting = false;

        // PictureBox for displaying the camera feed
        private PictureBox _display;

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

                // Set up polling timer for triggerDetect 
                _pollingTimer = new Timer { Interval = 500 };
                _pollingTimer.Tick += PollTriggerDetect;
                _pollingTimer.Start();

                // Initialize the display panel
                panelCamera.Location = new Point(12, 12);
                panelCamera.Size = new Size(371, 300);
                panelCamera.TabIndex = 0;

                // PictureBox for displaying camera frames
                _display = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.StretchImage
                };
                panelCamera.Controls.Add(_display);

                // Initialize and validate the camera
                _capture = new VideoCapture(1);
                if (!_capture.IsOpened)
                {
                    MessageBox.Show("Camera not available or cannot be opened.", "Camera Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                // Ensure event is not subscribed multiple times
                _capture.ImageGrabbed -= ProcessFrame;
                _capture.ImageGrabbed += ProcessFrame;

                // Start camera capture
                _capture.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Initialization error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }



        // Trigger capturing a image to detect cup
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
                        _triggerDetectVar.Value = new Bool(false); // Reset trigger
                    }

                    _pollingTimer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Polling error: " + ex.Message);
            }
        }


        // Capture image continously
        private bool CaptureAndDetect()
        {
            _isDetecting = true;         // <- thêm
            try
            {
                while (true)
                {
                    if (this.IsDisposed || _capture == null || !_capture.IsOpened)
                        return false;

                    using (Mat frame = new Mat())
                    {
                        bool ok = _capture.Read(frame);   // luôn lấy frame MỚI
                        if (ok && !frame.IsEmpty)
                        {
                            var cups = AnalyzeFrame(frame);
                            if (cups.Count > 0)
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



        // Called automatically whenever a new frame is grabbed from the webcam
        private void ProcessFrame(object sender, EventArgs e)
        {
            if (_isDetecting) return; // đang detect liên tục thì bỏ qua event

            using (Mat frame = new Mat())
            {
                _capture.Retrieve(frame);
                if (!frame.IsEmpty) AnalyzeFrame(frame);
            }
        }


        // Analyzes each frame to detect coffee cup-like contours
        // Analyzes each frame to detect coffee cup-like contours
        private List<(Point Center, int Radius)> AnalyzeFrame(Mat frame)
        {
            // Kết quả phát hiện
            List<(Point Center, int Radius)> detectedCups = new();

            // Ảnh xuất để vẽ
            using Mat output = frame.Clone();

            try
            {
                // ===== Chuẩn bị ảnh xám, làm mượt, Canny =====
                using Mat gray = new();
                CvInvoke.CvtColor(frame, gray, ColorConversion.Bgr2Gray);

                CvInvoke.GaussianBlur(gray, gray, new Size(3, 3), 0);

                using Mat edges = new();
                CvInvoke.Canny(gray, edges, 30, 100);

                // ===== Tìm contour “tròn” cỡ vừa =====
                using var contours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(edges, contours, null,
                                      RetrType.External, ChainApproxMethod.ChainApproxSimple);

                for (int i = 0; i < contours.Size; i++)
                {
                    using var contour = contours[i];
                    if (contour.Size < 5) continue;

                    double area = CvInvoke.ContourArea(contour);
                    // Cỡ này tuỳ khung, bạn có thể chỉnh lại cho hợp camera
                    if (area < 800 || area > 20000) continue;

                    RotatedRect ellipse = CvInvoke.FitEllipse(contour);
                    double ratio = ellipse.Size.Width / Math.Max(1e-6, ellipse.Size.Height);
                    if (ratio < 0.85 || ratio > 1.15) continue;

                    Point center = Point.Round(ellipse.Center);
                    int radius = (int)(Math.Max(ellipse.Size.Width, ellipse.Size.Height) / 2.0);
                    detectedCups.Add((center, radius));
                }

                // ===== VẼ TRỤC TỌA ĐỘ (gốc giữa khung) =====
                int cx = frame.Width / 2;
                int cy = frame.Height / 2;

                // Trục Oy
                CvInvoke.Line(output, new Point(cx, 0), new Point(cx, frame.Height - 1),
                              new MCvScalar(255, 255, 255), 1);
                // Trục Ox
                CvInvoke.Line(output, new Point(0, cy), new Point(frame.Width - 1, cy),
                              new MCvScalar(255, 255, 255), 1);

                // Gốc (0,0)
                CvInvoke.Circle(output, new Point(cx, cy), 3, new MCvScalar(255, 255, 255), -1);
                CvInvoke.PutText(output, "0,0", new Point(cx + 6, cy - 6),
                                 FontFace.HersheySimplex, 0.5, new MCvScalar(255, 255, 255), 1);

                // ===== VẼ LY & HIỂN THỊ OFFSET =====
                if (detectedCups.Count > 0)
                {
                    // Chọn “ly chính”: ly có bán kính lớn nhất (có thể đổi tiêu chí tuỳ bạn)
                    var (center, radius) = detectedCups
                        .OrderByDescending(c => c.Radius)
                        .First();

                    // Vẽ đường tròn và nhãn
                    CvInvoke.Circle(output, center, radius, new MCvScalar(0, 255, 0), 2);
                    CvInvoke.PutText(output, "Cup",
                        new Point(center.X - radius, center.Y - radius - 5),
                        FontFace.HersheySimplex, 0.6, new MCvScalar(255, 0, 0), 2);

                    // Tính offset có dấu: x phải dương, trái âm; y LÊN dương, xuống âm
                    int dx = center.X - cx;
                    int dy = cy - center.Y;     // đảo dấu trục ảnh để “lên” là dương
                    double dist = Math.Sqrt(dx * dx + dy * dy);

                    // Vẽ tia từ gốc tới tâm ly (tùy chọn)
                    CvInvoke.Line(output, new Point(cx, cy), center, new MCvScalar(0, 255, 0), 1);

                    // Hiển thị offset ở góc trái trên
                    string txt = $"Offset (px): x={(dx >= 0 ? "+" : "")}{dx}, y={(dy >= 0 ? "+" : "")}{dy} | r={dist:F1}";
                    CvInvoke.PutText(output, txt,
                        new Point(10, 25),
                        FontFace.HersheySimplex, 0.6, new MCvScalar(0, 255, 255), 2);
                }
                else
                {
                    // Không có ly: báo "No cup" ở góc trái trên
                    CvInvoke.PutText(output, "No cup",
                        new Point(10, 25),
                        FontFace.HersheySimplex, 0.7, new MCvScalar(0, 0, 255), 2);
                }

                // ===== Cập nhật UI an toàn, không rò bộ nhớ =====
                if (!IsDisposed && _display?.IsDisposed == false)
                {
                    Invoke(new Action(() =>
                    {
                        if (_display?.IsDisposed == false)
                        {
                            _display.Image?.Dispose();
                            using var tmp = output.ToImage<Bgr, byte>();
                            _display.Image = tmp.ToBitmap(); // Bitmap mới sẽ “sống” sau khi tmp dispose
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Analyze error: " + ex.Message);
            }

            return detectedCups;
        }




        // Clean up resources properly when the form is closing
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                if (_capture != null)
                {
                    _capture.ImageGrabbed -= ProcessFrame; // Detach the event handler
                    _capture.Stop();                       // Stop capturing
                    _capture.Dispose();                    // Release camera resources
                    _capture = null;
                }

                if (_pollingTimer != null)
                {
                    _pollingTimer.Stop();
                    _pollingTimer.Tick -= PollTriggerDetect;
                    _pollingTimer.Dispose();
                    _pollingTimer = null;
                }

                _display?.Dispose(); // Dispose PictureBox if needed
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cleanup error: " + ex.Message);
            }

            base.OnFormClosing(e);
        }


        private void panelCamera_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
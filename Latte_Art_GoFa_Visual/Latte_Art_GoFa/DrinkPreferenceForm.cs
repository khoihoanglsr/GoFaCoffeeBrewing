using System;
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.RapidDomain;
using static RobotStudio.Services.RobApi.Transport.RWS.RobFileTransferLegacyRWS;

namespace Latte_Art_GoFa
{
    public partial class DrinkPreferenceForm : Form
    {
        private string selectedDrink;
        private Controller robotController;

        public DrinkPreferenceForm(string drinkName, Controller controller)
        {
            InitializeComponent();
            selectedDrink = drinkName;
            robotController = controller;

            label_DrinkName.Text = $"Bạn đã chọn: {selectedDrink}";
        }

        private void DrinkPreferenceForm_Load(object sender, EventArgs e)
        {
        }

        private void button_BACK_Click(object sender, EventArgs e)
        {
            DrinkSelectionForm_User orderWindow = new DrinkSelectionForm_User(robotController);
            orderWindow.Show();
            this.Hide();
        }

        private void checkBox_YES_ICE_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_NO_ICE.Checked)
            {
                checkBox_YES_ICE.Checked = false;
            }
        }

        private void checkBox_NO_ICE_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_YES_ICE.Checked)
            {
                checkBox_NO_ICE.Checked = false;
            }
        }

        private void button_Order_Click(object sender, EventArgs e)
        {
            if (robotController == null || !robotController.Connected)
            {
                MessageBox.Show("Chưa kết nối robot!");
                return; // Dừng nếu chưa kết nối
            }

            // Ánh xạ tên đồ uống sang tên routine RAPID tương ứng
            string routineName = selectedDrink switch
            {
                "Coffee" => "Put_In_Cup",
                "Latte Art" => "PickTable_Cup2",
                "Milk Coffee" => "Put_Out_Cup",
                "Coffee with Milk" => "Water_Supply",
                _ => null
            };

            // Tất cả các routine nằm trong module này
            string moduleName = "Kcare_Latte_art";

            if (string.IsNullOrEmpty(routineName))
            {
                MessageBox.Show("Loại đồ uống không hợp lệ.");
                return;
            }

            try
            {
                using (Mastership m = Mastership.Request(robotController)) // Xin quyền điều khiển
                {
                    var task = robotController.Rapid.GetTask("T_ROB1"); // Lấy task RAPID
                    var module = task.GetModule(moduleName); // Lấy module tương ứng
                                                             // Ghi biến hasIce từ checkbox sang RAPID
                    bool Ice = checkBox_YES_ICE.Checked;
                    RapidData iceVar = task.GetRapidData(moduleName, "Ice");
                    iceVar.Value = new Bool(Ice);

                    // Kiểm tra routine có tồn tại không
                    bool routineExists = module.GetRoutines().Any(r => r.Name == routineName);

                    if (!routineExists)
                    {
                        MessageBox.Show($" Không tìm thấy routine '{routineName}' trong module '{moduleName}'");
                        return;
                    }
                    task.ResetProgramPointer();
                    // Gán con trỏ chương trình vào routine được chọn
                    task.SetProgramPointer(moduleName, routineName);

                    // Gửi lệnh bắt đầu RAPID
                    StartResult result = robotController.Rapid.Start();

                    if (result == StartResult.Ok)
                        MessageBox.Show($" Robot đang pha: {selectedDrink} {(Ice ? "(Có đá)" : "(Không đá)")}");
                    else
                        MessageBox.Show($" Không thể khởi động RAPID: {result}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chạy routine: " + ex.Message);
            }
        }
    }
}


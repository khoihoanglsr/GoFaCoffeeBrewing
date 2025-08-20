using System;
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.RapidDomain;
using DetectCoffeeCup;
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

            labelDrinkName.Text = $"Bạn đã chọn: {selectedDrink}";
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
            if (checkBox_YES_ICE.Checked)
            {
                checkBox_NO_ICE.Checked = false;
            }
        }

        private void checkBox_NO_ICE_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_NO_ICE.Checked)
            {
                checkBox_YES_ICE.Checked = false;
            }
        }

        private bool ValidateIceSelection()
        {
            if (!checkBox_YES_ICE.Checked && !checkBox_NO_ICE.Checked)
            {
                MessageBox.Show("Làm ơn chọn CÓ hoặc KHÔNG cho mục THÊM ĐÁ!", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void button_Order_Click(object sender, EventArgs e)
        {
            if (!ValidateIceSelection())
            {
                return; // Stop here if nothing is selected
            }

            if (robotController == null || !robotController.Connected)
            {
                MessageBox.Show("Chưa kết nối robot!");
                return; // Stop here if the robot isn't connected
            }

            // Ánh xạ tên đồ uống sang tên routine RAPID tương ứng
            string routineName = selectedDrink switch
            {
                "Black Coffee" => "BlackCoffee", // Call out the process making coffee
                //"Latte Art" => "", // 
                "Milk Coffee" => "MilkCoffee", // Call out the process making milk coffee
                //"Coffee with Milk" => "", //
                _ => null
            };

            // Main routines are in this module
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
                                                             // Ghi biến Ice từ checkbox sang RAPID
                    bool Ice = checkBox_YES_ICE.Checked;
                    RapidData iceVar = task.GetRapidData("CalibData", "ice_checkbox");
                    iceVar.Value = new Bool(Ice);

                    // Check if the routine exists
                    bool routineExists = module.GetRoutines().Any(r => r.Name == routineName);

                    if (!routineExists)
                    {
                        MessageBox.Show($" Không tìm thấy routine '{routineName}' trong module '{moduleName}'");
                        return;
                    }
                    task.ResetProgramPointer();

                    // Set the program pointer to the selected routine
                    task.SetProgramPointer(moduleName, routineName);

                    // Send command to start RAPID 
                    StartResult result = robotController.Rapid.Start();

                    if (result == StartResult.Ok)
                    {
                        // Start to open camera to observe the process
                        CameraProcess preferenceForm = new CameraProcess(robotController);
                        preferenceForm.Show();
                        this.Hide();
                    }
                    else
                        MessageBox.Show($" Không thể khởi động RAPID: {result}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi chạy routine: " + ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}


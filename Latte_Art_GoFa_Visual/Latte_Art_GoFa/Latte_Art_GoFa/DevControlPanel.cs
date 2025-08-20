//
using System;
using System.Globalization;
using System.Text;
using System.Text.Json;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Input;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Configuration;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.MotionDomain;
using ABB.Robotics.Controllers.RapidDomain;
using RobotStudio.Services.RobApi.RobApi1;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Latte_Art_GoFa
{
    public partial class Latte_Art_GoFa : Form
    {
        public ControllerInfoCollection Controllers;  //Info about the scanned controllers will be saved here
        public Controller? SelectedController;    //this variable is used to connect with the chosen controller
        ABB.Robotics.Controllers.RapidDomain.Task currentSelectedTask;   //this variable is used to choose a task in Rapid
        ABB.Robotics.Controllers.RapidDomain.Task[] allTasks;

        private System.Timers.Timer rapidStatusTimer;
        private System.Timers.Timer dataMonitoringTimer;
        RapidSymbol[] allRapidDatas;
        RapidSymbol[] allwritableRapidDatas;
        private bool isMonitoring = false;
        private bool isDataMonitoring = false;
        private bool isRobotControlButtonEnabled = false;

        //public static object Latte_Art_GoFa { get; internal set; }

        public Latte_Art_GoFa()
        {
            InitializeComponent();
            loginWindow Login_Window = (loginWindow)Application.OpenForms["loginWindow"];
            Controllers = Login_Window.Controllers;
            SelectedController = Login_Window.SelectedController;
        }

        private void Latte_Art_GoFa_Load(object sender, EventArgs e)
        {
            // You can place initial setup code here
            button_StartRAPID.Enabled = true;
            button_StopRAPID.Enabled = false;
            rapidStatusTimer = new System.Timers.Timer(100); // 100ms
            rapidStatusTimer.Elapsed += RapidStatusTimer_Elapsed;
            rapidStatusTimer.AutoReset = true;

            dataMonitoringTimer = new System.Timers.Timer(500); // 500ms
            dataMonitoringTimer.Elapsed += DataMonitoringTimer_Elapsed;
            dataMonitoringTimer.AutoReset = true;


            InitializeTCPLabels();
            LoadTasksIntoComboBox();
            InitializeJointLabels();
        }
        private void InitializeTCPLabels()
        {
            tableLayoutPanel_TCPPos.Controls.Clear(); // Clear previous labels if any

            string[] axisNames = { "X", "Y", "Z" };

            for (int i = 0; i < 3; i++)
            {
                Label tcpLabel = new Label();
                tcpLabel.Name = $"labelTCP{axisNames[i]}";
                tcpLabel.Text = $"{axisNames[i]}: 0.0 mm"; // Initial placeholder
                tcpLabel.Dock = DockStyle.Fill;
                tcpLabel.TextAlign = ContentAlignment.MiddleLeft;
                tcpLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

                tableLayoutPanel_TCPPos.Controls.Add(tcpLabel, 0, i);
            }
        }
        private void DataMonitoringTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (SelectedController == null || !SelectedController.Connected) return;

            List<string> selectedVars = new();
            this.Invoke(new Action(() =>
            {

            }));

            this.Invoke(new Action(() =>
            {
                DisplaySelectedRapidVariables(selectedVars, currentSelectedTask);
            }));
        }
        private void DisplaySelectedRapidVariables(List<string> selectedVars, ABB.Robotics.Controllers.RapidDomain.Task selectedTask)
        {


            HashSet<string> selectedSet = new HashSet<string>(selectedVars);

            foreach (RapidSymbol rs in allRapidDatas)
            {
                if (selectedSet.Contains(rs.Name))
                {
                    try
                    {
                        ListViewItem item = new ListViewItem(rs.Name);
                        RapidDataType theDataType;
                        theDataType = RapidDataType.GetDataType(rs);
                        item.SubItems.Add(theDataType.Name);

                        string module = rs.Scope[1];
                        RapidData data = selectedTask.GetRapidData(module, rs.Name);
                        string valueStr = "";
                        try
                        {
                            //valueStr = data.Value.ToString();
                            valueStr = data.StringValue;
                        }
                        catch
                        {
                            valueStr = "[Not Readable]";
                        }
                        item.SubItems.Add(valueStr);

                    }
                    catch (Exception ex)
                    {
                        // Skip if variable doesn't exist or cannot be read
                        ListViewItem item = new ListViewItem(rs.Name);
                        item.SubItems.Add("[Not Found]");
                        item.SubItems.Add($"Error: {ex.Message}");

                    }
                }
            }
        }

        private void InitializeJointLabels()
        {
            tableLayoutPanel_RobotJoints.Controls.Clear(); // Clear any existing content

            for (int i = 0; i < 7; i++)
            {
                Label jointLabel = new Label();
                jointLabel.Name = $"labelJoint{i + 1}";
                jointLabel.Text = $"Joint {i + 1}: 0.0°"; // Placeholder
                jointLabel.Dock = DockStyle.Fill;
                jointLabel.TextAlign = ContentAlignment.MiddleLeft;
                jointLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);

                tableLayoutPanel_RobotJoints.Controls.Add(jointLabel, 0, i);
            }
        }
        private void buttonStartDataMonitor_Click(object sender, EventArgs e)
        {
            if (!isDataMonitoring)
            {

                if (allRapidDatas == null || allRapidDatas.Length == 0)
                {
                    // The array is either null or contains no elements
                    MessageBox.Show("No RAPID symbols found. Please capture the variables first!");
                    return;
                }

                dataMonitoringTimer.Start();
                isDataMonitoring = true;

            }
            else
            {
                dataMonitoringTimer.Stop();
                isDataMonitoring = false;

            }
        }


        private void RapidStatusTimer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            if (SelectedController == null || !SelectedController.Connected)
            {
                this.Invoke(new Action(() =>
                {
                    label_Notconnected1.Text = "Not Connected!";
                    panel_RobotState.BackColor = Color.Gray;
                    label_Notconnected2.Text = "Not Connected!";
                    panel_ControllerState.BackColor = Color.Gray;
                }));
                return;
            }

            this.Invoke(new Action(() =>
            {
                if (SelectedController.State == ControllerState.MotorsOn)
                {
                    panel_ControllerState.BackColor = Color.Green;
                    label_Notconnected2.Text = "Motor is ON";
                }
                else if (SelectedController.State == ControllerState.MotorsOff)
                {
                    panel_ControllerState.BackColor = Color.Yellow;
                    label_Notconnected2.Text = "Motor is OFF";
                }
                else
                {
                    panel_ControllerState.BackColor = Color.Red;
                    label_Notconnected2.Text = "Unknown state";
                }
            }));

            try
            {
                var status = SelectedController.Rapid.ExecutionStatus;

                this.Invoke(new Action(() =>
                {
                    label_Notconnected1.Text = $"RAPID: {status}";
                    panel_RobotState.BackColor = status == ExecutionStatus.Running ? Color.Green :
                                                status == ExecutionStatus.Stopped ? Color.Yellow :
                                                Color.Gray;

                }));
            }
            catch
            {
                this.Invoke(new Action(() =>
                {
                    label_Notconnected1.Text = "RAPID: Unknown";
                    panel_RobotState.BackColor = Color.Gray;
                }));
            }
            //string taskName = comboBoxSelectedTask.SelectedItem.ToString();
            //ABB.Robotics.Controllers.RapidDomain.Task selectedTask = SelectedController.Rapid.GetTask(taskName);
            JointTarget jointTarget = currentSelectedTask.GetJointTarget();
            RobTarget tcpTarget = currentSelectedTask.GetRobTarget();
            //JointTarget jointTarget = SelectedController.MotionSystem.ActiveMechanicalUnit.GetPosition();
            //RobTarget tcpTarget = SelectedController.MotionSystem.ActiveMechanicalUnit.GetPosition(
            //    ABB.Robotics.Controllers.MotionDomain.CoordinateSystemType.World);

            double[] jointAngles = new double[]
            {
                jointTarget.RobAx.Rax_1,
                jointTarget.RobAx.Rax_2,
                jointTarget.RobAx.Rax_3,
                jointTarget.RobAx.Rax_4,
                jointTarget.RobAx.Rax_5,
                jointTarget.RobAx.Rax_6,
                jointTarget.ExtAx.Eax_a
            };

            double[] tcpPos = new double[]
            {
                tcpTarget.Trans.X,
                tcpTarget.Trans.Y,
                tcpTarget.Trans.Z
            };

            this.Invoke(new Action(() =>
            {
                UpdateJointLabels(jointAngles);
                UpdateTCPLabels(tcpPos);
            }));
        }
        private void UpdateTCPLabels(double[] tcpValues)
        {
            // tcpValues = new double[] { x, y, z }
            for (int i = 0; i < 3 && i < tcpValues.Length; i++)
            {
                Control label = tableLayoutPanel_TCPPos.GetControlFromPosition(0, i);
                if (label is Label tcpLabel)
                {
                    tcpLabel.Text = $"{(char)('X' + i)}: {tcpValues[i]:F2} mm";
                }
            }
        }

        private void UpdateJointLabels(double[] jointValues)
        {
            for (int i = 0; i < 7 && i < jointValues.Length; i++)
            {
                Control label = tableLayoutPanel_RobotJoints.GetControlFromPosition(0, i);
                if (label is Label jointLabel)
                {
                    jointLabel.Text = $"Joint {i + 1}: {jointValues[i]:F2}°";
                }
            }
        }
        private void GoFaController_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop and dispose rapidStatusTimer
            if (rapidStatusTimer != null)
            {
                rapidStatusTimer.Stop();
                rapidStatusTimer.Dispose();
                rapidStatusTimer = null;
            }

            // Stop and dispose dataMonitoringTimer
            if (dataMonitoringTimer != null)
            {
                dataMonitoringTimer.Stop();
                dataMonitoringTimer.Dispose();
                dataMonitoringTimer = null;
            }

            // Optional: safely disconnect the controller if needed
            if (SelectedController != null)
            {
                SelectedController.Logoff();
                SelectedController.Dispose();
                SelectedController = null;
            }

        }
        private void buttonMonitorTrigger_Click(object sender, EventArgs e)
        {
            if (!isMonitoring)
            {
                rapidStatusTimer.Start();
                isMonitoring = true;
                button_MonitorTrigger.Text = "Stop Monitor";
                button_MonitorTrigger.BackColor = Color.LightGreen;
            }
            else
            {
                rapidStatusTimer.Stop();
                isMonitoring = false;
                button_MonitorTrigger.Text = "Start Monitor";
                button_MonitorTrigger.BackColor = Color.LightGray;
            }
        }
        private void LoadTasksIntoComboBox()    //this function is used to load the tasks into the comboBox
        {
            ComboBox_SelectedROBOT.Items.Clear(); // Clear previous items

            if (SelectedController != null && SelectedController.Connected)
            {
                try
                {
                    allTasks = SelectedController.Rapid.GetTasks();
                    foreach (var task in allTasks)
                    {
                        ComboBox_SelectedROBOT.Items.Add(task.Name); // You could also store the task object if needed
                    }

                    if (ComboBox_SelectedROBOT.Items.Count > 0)
                        ComboBox_SelectedROBOT.SelectedIndex = 0; // Select first task by default
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load tasks: " + ex.Message);
                }
            }
        }



        private void startRAPID()   //this function used to run the RAPID program
        {
            if (SelectedController == null) return;

            try
            {
                //here we only start the program if the robot is in Auto Mode and the Motor is on
                if (SelectedController.OperatingMode == ControllerOperatingMode.Auto && SelectedController.State == ControllerState.MotorsOn)
                {
                    using (Mastership m = Mastership.Request(SelectedController))  //need to request the mastership first
                    {
                        foreach (var task in allTasks)
                        {
                            task.ResetProgramPointer();
                        }
                        StartResult result = SelectedController.Rapid.Start(true);
                        button_StartRAPID.Enabled = false;
                        button_StopRAPID.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client." + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }
        private void ControlRobotFromOffsetJoints(string dataName, string flagName, bool isIncrease)
        {
            if (SelectedController == null || !SelectedController.Connected)
            {
                MessageBox.Show("Please connect to the controller!");
                return;
            }



            string moduleName = "ControlFromSDK";

            try
            {
                RapidData rapidData = currentSelectedTask.GetRapidData(moduleName, dataName);
                RapidData rapidflag = currentSelectedTask.GetRapidData(moduleName, flagName);

                using (Mastership m = Mastership.Request(SelectedController))
                {
                    //string cleaned = textBoxOffsetMoveJoints.Text.Trim();

                    // Remove square brackets if present
                    //if (cleaned.StartsWith("[") && cleaned.EndsWith("]"))
                    //{
                    //    cleaned = cleaned.Substring(1, cleaned.Length - 2);
                    //  }

                    // Validate and write the single Num value
                    //  if (double.TryParse(cleaned, out double numValue))
                    {
                        // Apply sign based on direction
                        if (!isIncrease)
                            //   numValue *= -1;

                            // Num rt = new Num();
                            // rt.FillFromString2(numValue.ToString(CultureInfo.InvariantCulture)); // Ensure correct decimal separator
                            // rapidData.Value = rt;

                            // Set flag to trigger action
                            rapidflag.Value = new Bool(true);
                        //     }
                        else
                        {
                            MessageBox.Show("Please enter a valid numeric value (e.g., 12.34 or [12.34])", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error writing to '{dataName}': {ex.Message}");
            }
        }

        // Joint 1 Control Event
        private void buttonMoveJ1Increase_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint1", "flagMoveOffsetJoint", true);
        }

        private void buttonMoveJ1Decrease_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint1", "flagMoveOffsetJoint", false);
        }


        // Joint 2 Control Event
        private void buttonMoveJ2Increase_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint2", "flagMoveOffsetJoint", true);
        }

        private void buttonMoveJ2Decrease_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint2", "flagMoveOffsetJoint", false);
        }

        // Joint 3 Control Event
        private void buttonMoveJ3Increase_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint3", "flagMoveOffsetJoint", true);
        }

        private void buttonMoveJ3Decrease_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint3", "flagMoveOffsetJoint", false);
        }


        // Joint 4 Control Event
        private void buttonMoveJ4Increase_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint4", "flagMoveOffsetJoint", true);
        }

        private void buttonMoveJ4Decrease_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint4", "flagMoveOffsetJoint", false);
        }


        // Joint 5 Control Event
        private void buttonMoveJ5Increase_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint5", "flagMoveOffsetJoint", true);
        }

        private void buttonMoveJ5Decrease_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint5", "flagMoveOffsetJoint", false);
        }


        // Joint 6 Control Event
        private void buttonMoveJ6Increase_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint6", "flagMoveOffsetJoint", true);
        }

        private void buttonMoveJ6Decrease_Click(object sender, EventArgs e)
        {
            ControlRobotFromOffsetJoints("OffsetJoint6", "flagMoveOffsetJoint", false);
        }


        // 
        private void stopRAPID()
        {
            if (SelectedController == null) return;

            try
            {
                if (SelectedController.OperatingMode == ControllerOperatingMode.Auto && SelectedController.State == ControllerState.MotorsOn)
                {
                    using (Mastership m = Mastership.Request(SelectedController))
                    {
                        SelectedController.Rapid.Stop(StopMode.Immediate);
                        button_StopRAPID.Enabled = false;
                        button_StartRAPID.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client." + ex.Message);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
            }
        }
        private void work_StopRAPID_Click(object sender, EventArgs e)
        {
            stopRAPID();   //call the function to stop rapidf stopRAPID
        }
        private void work_StartRAPID_Click(object sender, EventArgs e)
        {
            startRAPID();   //call the function to start rapid
        }
        private void work_ToResetPPToMain_Click(object sender, EventArgs e)
        {
            if (SelectedController == null || !SelectedController.Connected)
            {
                MessageBox.Show("Please select a controller first!");
                return;
            }
            using (Mastership m = Mastership.Request(SelectedController))
            {
                foreach (var task in allTasks)
                {
                    task.Stop();
                    task.ResetProgramPointer();
                }
            }
        }

        private void comboBoxSelectedTask_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_ModuleFromTASK.Items.Clear();  // Clear previous modules
            if (SelectedController == null || !SelectedController.Connected) return;

            try
            {
                string selectedTaskName = ComboBox_SelectedROBOT.SelectedItem?.ToString();

                if (!string.IsNullOrEmpty(selectedTaskName))
                {
                    currentSelectedTask = SelectedController.Rapid.GetTask(selectedTaskName);

                    foreach (Module module in currentSelectedTask.GetModules())
                    {
                        list_ModuleFromTASK.Items.Add(module.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading modules: " + ex.Message);
            }
        }





        private void comboBox_Routines_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_StartRoutine.Enabled = true;
            button_StopRoutine.Enabled = false;
        }

        private void work_Start_Routine_Click(object sender, EventArgs e)
        {
            if (SelectedController == null || !SelectedController.Connected)
            {
                MessageBox.Show("Controller not connected.");
                return;
            }

            string moduleName = list_ModuleFromTASK.SelectedItem?.ToString();
            string routineName = comboBox_Routines.SelectedItem?.ToString();

            if ((currentSelectedTask == null) || string.IsNullOrEmpty(moduleName) || string.IsNullOrEmpty(routineName))
            {
                MessageBox.Show("Please select a task, module, and routine.");
                return;
            }

            try
            {

                using (Mastership m = Mastership.Request(SelectedController))
                {
                    currentSelectedTask.SetProgramPointer(moduleName, routineName);

                    // Start the entire RAPID execution (not individual task)
                    StartResult result = SelectedController.Rapid.Start();
                    if (result != StartResult.Ok)
                    {
                        MessageBox.Show($"Fail to start the current selected rountine. {result.ToString()}");
                    }
                    button_StartRoutine.Enabled = false;
                    button_StopRoutine.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to start routine: " + ex.Message);
            }
        }



        private void work_Stop_Routine_Click(object sender, EventArgs e)
        {
            if (SelectedController == null || !SelectedController.Connected) return;

            if (currentSelectedTask == null) return;

            try
            {
                using (Mastership m = Mastership.Request(SelectedController))
                {
                    currentSelectedTask.Stop(StopMode.Immediate);
                    button_StopRoutine.Enabled = false;
                    button_StartRoutine.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to stop execution: " + ex.Message);
            }
        }

        private void work_LoadModuleToRobot(object sender, EventArgs e)
        {
            if ((SelectedController == null) || (!SelectedController.Connected))
            {
                MessageBox.Show("Please connect to a controller!");
                return;
            }
            if (currentSelectedTask == null)
            {
                MessageBox.Show("Please select a specific task!");
                return;
            }

            // Prepare file dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a RAPID module to load";
            openFileDialog.Filter = "RAPID Module Files (*.mod;*.modx)|*.mod;*.modx";
            openFileDialog.InitialDirectory = Application.StartupPath;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string localPath = openFileDialog.FileName;
                string fileNameOnly = Path.GetFileName(localPath); // modulename.modx
                string remotePath = $"{fileNameOnly}";

                try
                {
                    using (Mastership m = Mastership.Request(SelectedController))
                    {
                        // Step 1: Upload file to controller file system (HOME:)
                        SelectedController.FileSystem.PutFile(localPath, remotePath);

                        // Step 2: Load module from the uploaded file path
                        currentSelectedTask.LoadModuleFromFile(remotePath, RapidLoadMode.Replace);
                    }

                    MessageBox.Show("Module uploaded and loaded successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    string fullMessage = $"Message: {ex.Message}\n\n";
                    if (ex.InnerException != null)
                    {
                        fullMessage += $"Inner Exception: {ex.InnerException.Message}\n\n";
                    }
                    fullMessage += $"Stack Trace:\n{ex.StackTrace}";

                    MessageBox.Show("Error uploading or loading module:\n\n" + fullMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void list_Module_From_TASK_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_Routines.Items.Clear();
            comboBox_Routines.Text = "";

            if (SelectedController == null || !SelectedController.Connected) return;

            string moduleName = list_ModuleFromTASK.SelectedItem?.ToString();

            if (currentSelectedTask == null || string.IsNullOrEmpty(moduleName)) return;
            try
            {
                Module module = currentSelectedTask.GetModule(moduleName);

                foreach (Routine proc in module.GetRoutines())
                {
                    comboBox_Routines.Items.Add(proc.Name);
                }

                if (comboBox_Routines.Items.Count > 0)
                {
                    comboBox_Routines.SelectedIndex = 0; // Optional: select the first routine
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading routines: " + ex.Message);
            }
        }


        private void button_MonitorTrigger_Click(object sender, EventArgs e)
        {
            if (!isMonitoring)
            {
                rapidStatusTimer.Start();
                isMonitoring = true;
                button_MonitorTrigger.Text = "Stop Monitor";
                button_MonitorTrigger.BackColor = Color.LightGreen;
            }
            else
            {
                rapidStatusTimer.Stop();
                isMonitoring = false;
                button_MonitorTrigger.Text = "Start Monitor";
                button_MonitorTrigger.BackColor = Color.LightGray;
            }
        }


        private void work_MonitorTrigger_Click(object sender, EventArgs e)
        {
            if (!isMonitoring)
            {
                rapidStatusTimer.Start();
                isMonitoring = true;
                button_MonitorTrigger.Text = "Stop Monitor";
                button_MonitorTrigger.BackColor = Color.LightGreen;
            }
            else
            {
                rapidStatusTimer.Stop();
                isMonitoring = false;
                button_MonitorTrigger.Text = "Start Monitor";
                button_MonitorTrigger.BackColor = Color.LightGray;
            }
        }

        private void buttonBackToLoginForm_Click(object sender, EventArgs e)
        {
            loginWindow orderWindow = new loginWindow();
            orderWindow.Show();
            this.Hide();
        }
    }
}

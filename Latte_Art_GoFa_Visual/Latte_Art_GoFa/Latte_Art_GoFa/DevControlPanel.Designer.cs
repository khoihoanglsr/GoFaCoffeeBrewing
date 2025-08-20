
namespace Latte_Art_GoFa
{
    partial class Latte_Art_GoFa
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Latte_Art_GoFa));
            panel2 = new Panel();
            label_Notconnected2 = new Label();
            label_Notconnected1 = new Label();
            panel_ControllerState = new Panel();
            panel6 = new Panel();
            panel_RobotState = new Panel();
            panel4 = new Panel();
            label_RobotStatus = new Label();
            tableLayoutPanel_TCPPos = new TableLayoutPanel();
            labelXYZCoordinates = new Label();
            tableLayoutPanel_RobotJoints = new TableLayoutPanel();
            labelJointAngles = new Label();
            button_MonitorTrigger = new Button();
            button_StopRAPID = new Button();
            ComboBox_SelectedROBOT = new ComboBox();
            button_StartRAPID = new Button();
            button_To_ResetPPToMain = new Button();
            list_ModuleFromTASK = new ListBox();
            comboBox_Routines = new ComboBox();
            button_StartRoutine = new Button();
            button_StopRoutine = new Button();
            button_LoadModuleToRobot = new Button();
            button_AutoMode = new Button();
            button_ManualMode = new Button();
            button_MotorOFF = new Button();
            button_MotorON = new Button();
            buttonBackToLoginForm = new Button();
            panel2.SuspendLayout();
            panel_ControllerState.SuspendLayout();
            panel_RobotState.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.Controls.Add(label_Notconnected2);
            panel2.Controls.Add(label_Notconnected1);
            panel2.Controls.Add(panel_ControllerState);
            panel2.Controls.Add(panel_RobotState);
            panel2.Controls.Add(label_RobotStatus);
            panel2.Controls.Add(tableLayoutPanel_TCPPos);
            panel2.Controls.Add(labelXYZCoordinates);
            panel2.Controls.Add(tableLayoutPanel_RobotJoints);
            panel2.Controls.Add(labelJointAngles);
            panel2.Location = new Point(884, 12);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(286, 467);
            panel2.TabIndex = 9;
            // 
            // label_Notconnected2
            // 
            label_Notconnected2.AutoSize = true;
            label_Notconnected2.Location = new Point(70, 63);
            label_Notconnected2.Name = "label_Notconnected2";
            label_Notconnected2.Size = new Size(107, 20);
            label_Notconnected2.TabIndex = 18;
            label_Notconnected2.Text = "Not connected";
            // 
            // label_Notconnected1
            // 
            label_Notconnected1.AutoSize = true;
            label_Notconnected1.Location = new Point(70, 33);
            label_Notconnected1.Name = "label_Notconnected1";
            label_Notconnected1.Size = new Size(107, 20);
            label_Notconnected1.TabIndex = 17;
            label_Notconnected1.Text = "Not connected";
            // 
            // panel_ControllerState
            // 
            panel_ControllerState.BackColor = SystemColors.Highlight;
            panel_ControllerState.Controls.Add(panel6);
            panel_ControllerState.Location = new Point(15, 63);
            panel_ControllerState.Margin = new Padding(3, 4, 3, 4);
            panel_ControllerState.Name = "panel_ControllerState";
            panel_ControllerState.Size = new Size(34, 20);
            panel_ControllerState.TabIndex = 16;
            // 
            // panel6
            // 
            panel6.Location = new Point(3, 36);
            panel6.Margin = new Padding(3, 4, 3, 4);
            panel6.Name = "panel6";
            panel6.Size = new Size(22, 28);
            panel6.TabIndex = 15;
            // 
            // panel_RobotState
            // 
            panel_RobotState.BackColor = SystemColors.Highlight;
            panel_RobotState.Controls.Add(panel4);
            panel_RobotState.Location = new Point(15, 33);
            panel_RobotState.Margin = new Padding(3, 4, 3, 4);
            panel_RobotState.Name = "panel_RobotState";
            panel_RobotState.Size = new Size(34, 22);
            panel_RobotState.TabIndex = 14;
            // 
            // panel4
            // 
            panel4.Location = new Point(3, 36);
            panel4.Margin = new Padding(3, 4, 3, 4);
            panel4.Name = "panel4";
            panel4.Size = new Size(22, 28);
            panel4.TabIndex = 15;
            // 
            // label_RobotStatus
            // 
            label_RobotStatus.AutoSize = true;
            label_RobotStatus.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label_RobotStatus.Location = new Point(96, 4);
            label_RobotStatus.Name = "label_RobotStatus";
            label_RobotStatus.Size = new Size(114, 23);
            label_RobotStatus.TabIndex = 10;
            label_RobotStatus.Text = "Robot Status";
            // 
            // tableLayoutPanel_TCPPos
            // 
            tableLayoutPanel_TCPPos.ColumnCount = 2;
            tableLayoutPanel_TCPPos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel_TCPPos.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel_TCPPos.Location = new Point(0, 356);
            tableLayoutPanel_TCPPos.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel_TCPPos.Name = "tableLayoutPanel_TCPPos";
            tableLayoutPanel_TCPPos.RowCount = 3;
            tableLayoutPanel_TCPPos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel_TCPPos.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel_TCPPos.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel_TCPPos.Size = new Size(282, 81);
            tableLayoutPanel_TCPPos.TabIndex = 12;
            // 
            // labelXYZCoordinates
            // 
            labelXYZCoordinates.AutoSize = true;
            labelXYZCoordinates.Location = new Point(83, 327);
            labelXYZCoordinates.Name = "labelXYZCoordinates";
            labelXYZCoordinates.Size = new Size(119, 20);
            labelXYZCoordinates.TabIndex = 1;
            labelXYZCoordinates.Text = "XYZ Coordinates";
            // 
            // tableLayoutPanel_RobotJoints
            // 
            tableLayoutPanel_RobotJoints.ColumnCount = 2;
            tableLayoutPanel_RobotJoints.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel_RobotJoints.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel_RobotJoints.Location = new Point(0, 113);
            tableLayoutPanel_RobotJoints.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel_RobotJoints.Name = "tableLayoutPanel_RobotJoints";
            tableLayoutPanel_RobotJoints.RowCount = 8;
            tableLayoutPanel_RobotJoints.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel_RobotJoints.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel_RobotJoints.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel_RobotJoints.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel_RobotJoints.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel_RobotJoints.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel_RobotJoints.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel_RobotJoints.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tableLayoutPanel_RobotJoints.Size = new Size(286, 207);
            tableLayoutPanel_RobotJoints.TabIndex = 4;
            // 
            // labelJointAngles
            // 
            labelJointAngles.AutoSize = true;
            labelJointAngles.BackColor = SystemColors.ScrollBar;
            labelJointAngles.Location = new Point(102, 89);
            labelJointAngles.Name = "labelJointAngles";
            labelJointAngles.Size = new Size(89, 20);
            labelJointAngles.TabIndex = 0;
            labelJointAngles.Text = "Joint Angles";
            // 
            // button_MonitorTrigger
            // 
            button_MonitorTrigger.Location = new Point(12, 441);
            button_MonitorTrigger.Name = "button_MonitorTrigger";
            button_MonitorTrigger.Size = new Size(214, 38);
            button_MonitorTrigger.TabIndex = 13;
            button_MonitorTrigger.Text = "Start Monitor";
            button_MonitorTrigger.UseVisualStyleBackColor = true;
            button_MonitorTrigger.Click += work_MonitorTrigger_Click;
            // 
            // button_StopRAPID
            // 
            button_StopRAPID.ForeColor = SystemColors.Highlight;
            button_StopRAPID.Location = new Point(121, 12);
            button_StopRAPID.Margin = new Padding(1);
            button_StopRAPID.Name = "button_StopRAPID";
            button_StopRAPID.Size = new Size(105, 65);
            button_StopRAPID.TabIndex = 3;
            button_StopRAPID.Text = "Stop RAPID";
            button_StopRAPID.UseVisualStyleBackColor = true;
            button_StopRAPID.Click += work_StopRAPID_Click;
            // 
            // ComboBox_SelectedROBOT
            // 
            ComboBox_SelectedROBOT.FormattingEnabled = true;
            ComboBox_SelectedROBOT.Location = new Point(12, 79);
            ComboBox_SelectedROBOT.Margin = new Padding(1);
            ComboBox_SelectedROBOT.Name = "ComboBox_SelectedROBOT";
            ComboBox_SelectedROBOT.Size = new Size(214, 28);
            ComboBox_SelectedROBOT.TabIndex = 6;
            ComboBox_SelectedROBOT.SelectedIndexChanged += comboBoxSelectedTask_SelectedIndexChanged;
            // 
            // button_StartRAPID
            // 
            button_StartRAPID.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            button_StartRAPID.ForeColor = SystemColors.Highlight;
            button_StartRAPID.Location = new Point(12, 12);
            button_StartRAPID.Margin = new Padding(1);
            button_StartRAPID.Name = "button_StartRAPID";
            button_StartRAPID.Size = new Size(107, 65);
            button_StartRAPID.TabIndex = 2;
            button_StartRAPID.Text = "Start RAPID";
            button_StartRAPID.UseVisualStyleBackColor = true;
            button_StartRAPID.Click += work_StartRAPID_Click;
            // 
            // button_To_ResetPPToMain
            // 
            button_To_ResetPPToMain.ForeColor = SystemColors.Highlight;
            button_To_ResetPPToMain.Location = new Point(12, 279);
            button_To_ResetPPToMain.Margin = new Padding(1);
            button_To_ResetPPToMain.Name = "button_To_ResetPPToMain";
            button_To_ResetPPToMain.Size = new Size(214, 40);
            button_To_ResetPPToMain.TabIndex = 5;
            button_To_ResetPPToMain.Text = "Reset PP to Main";
            button_To_ResetPPToMain.UseVisualStyleBackColor = true;
            button_To_ResetPPToMain.Click += work_ToResetPPToMain_Click;
            // 
            // list_ModuleFromTASK
            // 
            list_ModuleFromTASK.ForeColor = SystemColors.InfoText;
            list_ModuleFromTASK.FormattingEnabled = true;
            list_ModuleFromTASK.Location = new Point(12, 109);
            list_ModuleFromTASK.Margin = new Padding(1);
            list_ModuleFromTASK.Name = "list_ModuleFromTASK";
            list_ModuleFromTASK.Size = new Size(214, 84);
            list_ModuleFromTASK.TabIndex = 7;
            list_ModuleFromTASK.SelectedIndexChanged += list_Module_From_TASK_SelectedIndexChanged;
            // 
            // comboBox_Routines
            // 
            comboBox_Routines.FormattingEnabled = true;
            comboBox_Routines.Location = new Point(12, 321);
            comboBox_Routines.Margin = new Padding(1);
            comboBox_Routines.Name = "comboBox_Routines";
            comboBox_Routines.Size = new Size(214, 28);
            comboBox_Routines.TabIndex = 8;
            comboBox_Routines.SelectedIndexChanged += comboBox_Routines_SelectedIndexChanged;
            // 
            // button_StartRoutine
            // 
            button_StartRoutine.Location = new Point(12, 353);
            button_StartRoutine.Name = "button_StartRoutine";
            button_StartRoutine.Size = new Size(107, 45);
            button_StartRoutine.TabIndex = 9;
            button_StartRoutine.Text = "StartRoutine";
            button_StartRoutine.UseVisualStyleBackColor = true;
            button_StartRoutine.Click += work_Start_Routine_Click;
            // 
            // button_StopRoutine
            // 
            button_StopRoutine.Location = new Point(119, 353);
            button_StopRoutine.Name = "button_StopRoutine";
            button_StopRoutine.Size = new Size(107, 45);
            button_StopRoutine.TabIndex = 10;
            button_StopRoutine.Text = "StopRoutine";
            button_StopRoutine.UseVisualStyleBackColor = true;
            button_StopRoutine.Click += work_Stop_Routine_Click;
            // 
            // button_LoadModuleToRobot
            // 
            button_LoadModuleToRobot.Location = new Point(12, 400);
            button_LoadModuleToRobot.Name = "button_LoadModuleToRobot";
            button_LoadModuleToRobot.Size = new Size(214, 40);
            button_LoadModuleToRobot.TabIndex = 12;
            button_LoadModuleToRobot.Text = "Load Module";
            button_LoadModuleToRobot.UseVisualStyleBackColor = true;
            button_LoadModuleToRobot.Click += work_LoadModuleToRobot;
            // 
            // button_AutoMode
            // 
            button_AutoMode.BackColor = SystemColors.InactiveBorder;
            button_AutoMode.Location = new Point(12, 197);
            button_AutoMode.Name = "button_AutoMode";
            button_AutoMode.Size = new Size(107, 38);
            button_AutoMode.TabIndex = 13;
            button_AutoMode.Text = "AutoMode";
            button_AutoMode.UseVisualStyleBackColor = false;
            // 
            // button_ManualMode
            // 
            button_ManualMode.Location = new Point(121, 197);
            button_ManualMode.Name = "button_ManualMode";
            button_ManualMode.Size = new Size(105, 38);
            button_ManualMode.TabIndex = 14;
            button_ManualMode.Text = "ManualMode";
            button_ManualMode.UseVisualStyleBackColor = true;
            // 
            // button_MotorOFF
            // 
            button_MotorOFF.Location = new Point(12, 238);
            button_MotorOFF.Name = "button_MotorOFF";
            button_MotorOFF.Size = new Size(107, 38);
            button_MotorOFF.TabIndex = 16;
            button_MotorOFF.Text = "MotorOFF";
            button_MotorOFF.UseVisualStyleBackColor = true;
            // 
            // button_MotorON
            // 
            button_MotorON.Location = new Point(121, 238);
            button_MotorON.Name = "button_MotorON";
            button_MotorON.Size = new Size(105, 38);
            button_MotorON.TabIndex = 15;
            button_MotorON.Text = "MotorON";
            button_MotorON.UseVisualStyleBackColor = true;
            // 
            // buttonBackToLoginForm
            // 
            buttonBackToLoginForm.Location = new Point(474, 465);
            buttonBackToLoginForm.Name = "buttonBackToLoginForm";
            buttonBackToLoginForm.Size = new Size(94, 29);
            buttonBackToLoginForm.TabIndex = 17;
            buttonBackToLoginForm.Text = "Back";
            buttonBackToLoginForm.UseVisualStyleBackColor = true;
            buttonBackToLoginForm.Click += buttonBackToLoginForm_Click;
            // 
            // Latte_Art_GoFa
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.MenuBar;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1182, 506);
            Controls.Add(buttonBackToLoginForm);
            Controls.Add(button_LoadModuleToRobot);
            Controls.Add(button_MotorON);
            Controls.Add(button_StopRoutine);
            Controls.Add(button_StartRoutine);
            Controls.Add(panel2);
            Controls.Add(button_MotorOFF);
            Controls.Add(button_MonitorTrigger);
            Controls.Add(comboBox_Routines);
            Controls.Add(button_To_ResetPPToMain);
            Controls.Add(button_ManualMode);
            Controls.Add(button_AutoMode);
            Controls.Add(button_StartRAPID);
            Controls.Add(button_StopRAPID);
            Controls.Add(ComboBox_SelectedROBOT);
            Controls.Add(list_ModuleFromTASK);
            Margin = new Padding(1);
            Name = "Latte_Art_GoFa";
            Text = "Latte Art GoFa";
            Load += Latte_Art_GoFa_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel_ControllerState.ResumeLayout(false);
            panel_RobotState.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Label labelJointAngles;
        private Panel panel2;
        private TableLayoutPanel tableLayoutPanel_RobotJoints;
        private Label labelXYZCoordinates;
        private Button button_MonitorTrigger;
        private TableLayoutPanel tableLayoutPanel_TCPPos;
        private Label label_Notconnected1;
        private Panel panel_ControllerState;
        private Panel panel6;
        private Panel panel_RobotState;
        private Panel panel4;
        private Label label_RobotStatus;
        private Label label_Notconnected2;
        private Button button_StopRAPID;
        private ComboBox ComboBox_SelectedROBOT;
        private Button button_StartRAPID;
        private Button button_To_ResetPPToMain;
        private ListBox list_ModuleFromTASK;
        private ComboBox comboBox_Routines;
        private Button button_StartRoutine;
        private Button button_StopRoutine;
        private Button button_LoadModuleToRobot;
        private Button button_AutoMode;
        private Button button_ManualMode;
        private Button button_MotorOFF;
        private Button button_MotorON;
        private Button buttonBackToLoginForm;
    }
}

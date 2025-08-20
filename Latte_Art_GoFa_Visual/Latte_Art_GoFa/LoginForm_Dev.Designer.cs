namespace Latte_Art_GoFa
{
    partial class Login_Window
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login_Window));
            labelStatus = new Label();
            combo_Box_Controllers = new ComboBox();
            button_Login_Default_User = new Button();
            label1 = new Label();
            text_BoxUsername = new TextBox();
            label2 = new Label();
            text_BoxtextPassword = new TextBox();
            button_Login_Admin = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker3 = new System.ComponentModel.BackgroundWorker();
            backgroundWorker4 = new System.ComponentModel.BackgroundWorker();
            SuspendLayout();
            // 
            // labelStatus
            // 
            labelStatus.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            labelStatus.AutoSize = true;
            labelStatus.BackColor = SystemColors.ControlDark;
            labelStatus.Location = new Point(360, 180);
            labelStatus.Margin = new Padding(1, 0, 1, 0);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(173, 20);
            labelStatus.TabIndex = 1;
            labelStatus.Text = "Please select a controller";
            // 
            // combo_Box_Controllers
            // 
            combo_Box_Controllers.BackColor = SystemColors.ControlLight;
            combo_Box_Controllers.FormattingEnabled = true;
            combo_Box_Controllers.Location = new Point(360, 203);
            combo_Box_Controllers.Margin = new Padding(1);
            combo_Box_Controllers.Name = "combo_Box_Controllers";
            combo_Box_Controllers.Size = new Size(194, 28);
            combo_Box_Controllers.TabIndex = 2;
            combo_Box_Controllers.SelectedIndexChanged += comboBoxControllers_SelectedIndexChanged;
            // 
            // button_Login_Default_User
            // 
            button_Login_Default_User.BackColor = Color.Silver;
            button_Login_Default_User.Location = new Point(683, 325);
            button_Login_Default_User.Name = "button_Login_Default_User";
            button_Login_Default_User.Size = new Size(200, 29);
            button_Login_Default_User.TabIndex = 3;
            button_Login_Default_User.Text = "Login as default user";
            button_Login_Default_User.UseVisualStyleBackColor = false;
            button_Login_Default_User.Click += buttonLoginDefaultUser_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = SystemColors.ControlDark;
            label1.Location = new Point(683, 180);
            label1.Margin = new Padding(1, 0, 1, 0);
            label1.Name = "label1";
            label1.Size = new Size(75, 20);
            label1.TabIndex = 4;
            label1.Text = "Username";
            // 
            // text_BoxUsername
            // 
            text_BoxUsername.BackColor = SystemColors.ControlLight;
            text_BoxUsername.Location = new Point(683, 204);
            text_BoxUsername.Name = "text_BoxUsername";
            text_BoxUsername.Size = new Size(201, 27);
            text_BoxUsername.TabIndex = 5;
            text_BoxUsername.TextChanged += text_BoxUsername_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = SystemColors.ControlDark;
            label2.Location = new Point(683, 235);
            label2.Margin = new Padding(1, 0, 1, 0);
            label2.Name = "label2";
            label2.Size = new Size(70, 20);
            label2.TabIndex = 6;
            label2.Text = "Password";
            // 
            // text_BoxtextPassword
            // 
            text_BoxtextPassword.BackColor = SystemColors.ControlLight;
            text_BoxtextPassword.Location = new Point(683, 257);
            text_BoxtextPassword.Name = "text_BoxtextPassword";
            text_BoxtextPassword.Size = new Size(201, 27);
            text_BoxtextPassword.TabIndex = 7;
            text_BoxtextPassword.TextChanged += text_BoxtextPassword_TextChanged;
            // 
            // button_Login_Admin
            // 
            button_Login_Admin.BackColor = Color.Silver;
            button_Login_Admin.Location = new Point(683, 291);
            button_Login_Admin.Name = "button_Login_Admin";
            button_Login_Admin.Size = new Size(200, 29);
            button_Login_Admin.TabIndex = 8;
            button_Login_Admin.Text = "Login Admin";
            button_Login_Admin.UseVisualStyleBackColor = false;
            button_Login_Admin.Click += ButtonLoginAdmin_Click;
            // 
            // Login_Window
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1902, 1033);
            Controls.Add(button_Login_Admin);
            Controls.Add(text_BoxtextPassword);
            Controls.Add(label2);
            Controls.Add(text_BoxUsername);
            Controls.Add(label1);
            Controls.Add(button_Login_Default_User);
            Controls.Add(combo_Box_Controllers);
            Controls.Add(labelStatus);
            DoubleBuffered = true;
            Name = "Login_Window";
            StartPosition = FormStartPosition.Manual;
            Text = "Login_Window";
            Load += Form2_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelStatus;
        private ComboBox combo_Box_Controllers;
        private Button button_Login_Default_User;
        private Label label1;
        private TextBox text_BoxUsername;
        private Label label2;
        private TextBox text_BoxtextPassword;
        private Button button_Login_Admin;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
    }
}
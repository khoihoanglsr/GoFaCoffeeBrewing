namespace Latte_Art_GoFa
{
    partial class loginWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginWindow));
            labelStatus = new Label();
            combo_Box_Controllers = new ComboBox();
            buttonLoginDefaultUser = new Button();
            labelUsername = new Label();
            textBoxUsername = new TextBox();
            labelPassword = new Label();
            textBoxPassword = new TextBox();
            buttonLoginAdmin = new Button();
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
            labelStatus.BackColor = SystemColors.ButtonFace;
            labelStatus.Location = new Point(10, 9);
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
            combo_Box_Controllers.Location = new Point(10, 32);
            combo_Box_Controllers.Margin = new Padding(1);
            combo_Box_Controllers.Name = "combo_Box_Controllers";
            combo_Box_Controllers.Size = new Size(201, 28);
            combo_Box_Controllers.TabIndex = 2;
            combo_Box_Controllers.SelectedIndexChanged += comboBoxControllers_SelectedIndexChanged;
            // 
            // buttonLoginDefaultUser
            // 
            buttonLoginDefaultUser.BackColor = Color.Silver;
            buttonLoginDefaultUser.Location = new Point(10, 253);
            buttonLoginDefaultUser.Name = "buttonLoginDefaultUser";
            buttonLoginDefaultUser.Size = new Size(200, 29);
            buttonLoginDefaultUser.TabIndex = 3;
            buttonLoginDefaultUser.Text = "Customer Ordering Screen";
            buttonLoginDefaultUser.UseVisualStyleBackColor = false;
            buttonLoginDefaultUser.Click += buttonLoginDefaultUser_Click;
            // 
            // labelUsername
            // 
            labelUsername.AutoSize = true;
            labelUsername.BackColor = SystemColors.ButtonFace;
            labelUsername.Location = new Point(10, 108);
            labelUsername.Margin = new Padding(1, 0, 1, 0);
            labelUsername.Name = "labelUsername";
            labelUsername.Size = new Size(75, 20);
            labelUsername.TabIndex = 4;
            labelUsername.Text = "Username";
            // 
            // textBoxUsername
            // 
            textBoxUsername.BackColor = SystemColors.ControlLight;
            textBoxUsername.Location = new Point(10, 132);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(201, 27);
            textBoxUsername.TabIndex = 5;
            textBoxUsername.TextChanged += text_BoxUsername_TextChanged;
            // 
            // labelPassword
            // 
            labelPassword.AutoSize = true;
            labelPassword.BackColor = SystemColors.ButtonFace;
            labelPassword.Location = new Point(10, 163);
            labelPassword.Margin = new Padding(1, 0, 1, 0);
            labelPassword.Name = "labelPassword";
            labelPassword.Size = new Size(70, 20);
            labelPassword.TabIndex = 6;
            labelPassword.Text = "Password";
            // 
            // textBoxPassword
            // 
            textBoxPassword.BackColor = SystemColors.ControlLight;
            textBoxPassword.Location = new Point(10, 185);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(201, 27);
            textBoxPassword.TabIndex = 7;
            textBoxPassword.TextChanged += text_BoxtextPassword_TextChanged;
            // 
            // buttonLoginAdmin
            // 
            buttonLoginAdmin.BackColor = Color.Silver;
            buttonLoginAdmin.Location = new Point(10, 219);
            buttonLoginAdmin.Name = "buttonLoginAdmin";
            buttonLoginAdmin.Size = new Size(200, 29);
            buttonLoginAdmin.TabIndex = 8;
            buttonLoginAdmin.Text = "Login as admin";
            buttonLoginAdmin.UseVisualStyleBackColor = false;
            buttonLoginAdmin.Click += ButtonLoginAdmin_Click;
            // 
            // loginWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonFace;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(477, 339);
            Controls.Add(buttonLoginAdmin);
            Controls.Add(textBoxPassword);
            Controls.Add(labelPassword);
            Controls.Add(textBoxUsername);
            Controls.Add(labelUsername);
            Controls.Add(buttonLoginDefaultUser);
            Controls.Add(combo_Box_Controllers);
            Controls.Add(labelStatus);
            DoubleBuffered = true;
            Name = "loginWindow";
            StartPosition = FormStartPosition.Manual;
            Text = "Login Window";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelStatus;
        private ComboBox combo_Box_Controllers;
        private Button buttonLoginDefaultUser;
        private Label labelUsername;
        private TextBox textBoxUsername;
        private Label labelPassword;
        private TextBox textBoxPassword;
        private Button buttonLoginAdmin;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.ComponentModel.BackgroundWorker backgroundWorker3;
        private System.ComponentModel.BackgroundWorker backgroundWorker4;
    }
}
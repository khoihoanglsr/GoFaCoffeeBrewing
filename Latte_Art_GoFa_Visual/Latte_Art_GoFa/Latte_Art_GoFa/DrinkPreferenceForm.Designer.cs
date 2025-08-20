namespace Latte_Art_GoFa
{
    partial class DrinkPreferenceForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrinkPreferenceForm));
            checkBox_YES_ICE = new CheckBox();
            checkBox_NO_ICE = new CheckBox();
            button_ORDER = new Button();
            label1 = new Label();
            button_BACK = new Button();
            labelDrinkName = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // checkBox_YES_ICE
            // 
            checkBox_YES_ICE.BackColor = Color.PeachPuff;
            checkBox_YES_ICE.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkBox_YES_ICE.Location = new Point(128, 112);
            checkBox_YES_ICE.Name = "checkBox_YES_ICE";
            checkBox_YES_ICE.Size = new Size(125, 35);
            checkBox_YES_ICE.TabIndex = 0;
            checkBox_YES_ICE.Text = "CÓ";
            checkBox_YES_ICE.UseVisualStyleBackColor = false;
            checkBox_YES_ICE.CheckedChanged += checkBox_YES_ICE_CheckedChanged;
            // 
            // checkBox_NO_ICE
            // 
            checkBox_NO_ICE.BackColor = Color.Bisque;
            checkBox_NO_ICE.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            checkBox_NO_ICE.Location = new Point(128, 153);
            checkBox_NO_ICE.Name = "checkBox_NO_ICE";
            checkBox_NO_ICE.Size = new Size(125, 35);
            checkBox_NO_ICE.TabIndex = 1;
            checkBox_NO_ICE.Text = "KHÔNG";
            checkBox_NO_ICE.UseVisualStyleBackColor = false;
            checkBox_NO_ICE.CheckedChanged += checkBox_NO_ICE_CheckedChanged;
            // 
            // button_ORDER
            // 
            button_ORDER.Location = new Point(326, 316);
            button_ORDER.Name = "button_ORDER";
            button_ORDER.Size = new Size(116, 47);
            button_ORDER.TabIndex = 3;
            button_ORDER.Text = "ORDER";
            button_ORDER.UseVisualStyleBackColor = true;
            button_ORDER.Click += button_Order_Click;
            // 
            // label1
            // 
            label1.BackColor = SystemColors.GradientInactiveCaption;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(128, 73);
            label1.Name = "label1";
            label1.Size = new Size(125, 35);
            label1.TabIndex = 4;
            label1.Text = "THÊM ĐÁ";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // button_BACK
            // 
            button_BACK.BackColor = SystemColors.Info;
            button_BACK.Location = new Point(8, 12);
            button_BACK.Name = "button_BACK";
            button_BACK.Size = new Size(94, 29);
            button_BACK.TabIndex = 5;
            button_BACK.Text = "BACK";
            button_BACK.UseVisualStyleBackColor = false;
            button_BACK.Click += button_BACK_Click;
            // 
            // labelDrinkName
            // 
            labelDrinkName.AutoSize = true;
            labelDrinkName.BackColor = Color.Transparent;
            labelDrinkName.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelDrinkName.Location = new Point(128, 9);
            labelDrinkName.Name = "labelDrinkName";
            labelDrinkName.Size = new Size(378, 46);
            labelDrinkName.TabIndex = 6;
            labelDrinkName.Text = "[Hiển thị nước đã order]";
            // 
            // pictureBox1
            // 
            pictureBox1.AccessibleRole = AccessibleRole.None;
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-5, -3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(811, 458);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.BackColor = SystemColors.GradientInactiveCaption;
            label2.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label2.Location = new Point(280, 72);
            label2.Name = "label2";
            label2.Size = new Size(143, 36);
            label2.TabIndex = 8;
            label2.Text = "THÊM SỮA";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // checkBox1
            // 
            checkBox1.BackColor = Color.PeachPuff;
            checkBox1.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold);
            checkBox1.Location = new Point(280, 112);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(148, 35);
            checkBox1.TabIndex = 9;
            checkBox1.Text = "CÓ";
            checkBox1.UseVisualStyleBackColor = false;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.BackColor = Color.Bisque;
            checkBox2.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold);
            checkBox2.Location = new Point(280, 153);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(144, 35);
            checkBox2.TabIndex = 10;
            checkBox2.Text = "KHÔNG";
            checkBox2.UseVisualStyleBackColor = false;
            // 
            // DrinkPreferenceForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(label2);
            Controls.Add(button_BACK);
            Controls.Add(label1);
            Controls.Add(button_ORDER);
            Controls.Add(checkBox_NO_ICE);
            Controls.Add(checkBox_YES_ICE);
            Controls.Add(labelDrinkName);
            Controls.Add(pictureBox1);
            Name = "DrinkPreferenceForm";
            Text = "Control Panel";
            Load += DrinkPreferenceForm_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox_YES_ICE;
        private CheckBox checkBox_NO_ICE;
        private ListBox listBox1;
        private Button button1;
        private Label label1;
        private Button button_ORDER;
        private Button button_BACK;
        private Label labelDrinkName;
        private PictureBox pictureBox1;
        private Label label2;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
    }
}
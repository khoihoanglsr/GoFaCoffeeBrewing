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
            checkBox_YES_ICE = new CheckBox();
            checkBox_NO_ICE = new CheckBox();
            listBox_NOTE = new ListBox();
            button_ORDER = new Button();
            label1 = new Label();
            button_BACK = new Button();
            label_DrinkName = new Label();
            SuspendLayout();
            // 
            // checkBox_YES_ICE
            // 
            checkBox_YES_ICE.AutoSize = true;
            checkBox_YES_ICE.Location = new Point(382, 35);
            checkBox_YES_ICE.Margin = new Padding(3, 2, 3, 2);
            checkBox_YES_ICE.Name = "checkBox_YES_ICE";
            checkBox_YES_ICE.Size = new Size(43, 19);
            checkBox_YES_ICE.TabIndex = 0;
            checkBox_YES_ICE.Text = "CÓ";
            checkBox_YES_ICE.UseVisualStyleBackColor = true;
            checkBox_YES_ICE.CheckedChanged += checkBox_YES_ICE_CheckedChanged;
            // 
            // checkBox_NO_ICE
            // 
            checkBox_NO_ICE.AutoSize = true;
            checkBox_NO_ICE.Location = new Point(382, 58);
            checkBox_NO_ICE.Margin = new Padding(3, 2, 3, 2);
            checkBox_NO_ICE.Name = "checkBox_NO_ICE";
            checkBox_NO_ICE.Size = new Size(68, 19);
            checkBox_NO_ICE.TabIndex = 1;
            checkBox_NO_ICE.Text = "KHÔNG";
            checkBox_NO_ICE.UseVisualStyleBackColor = true;
            checkBox_NO_ICE.CheckedChanged += checkBox_NO_ICE_CheckedChanged;
            // 
            // listBox_NOTE
            // 
            listBox_NOTE.FormattingEnabled = true;
            listBox_NOTE.ItemHeight = 15;
            listBox_NOTE.Location = new Point(462, 9);
            listBox_NOTE.Margin = new Padding(3, 2, 3, 2);
            listBox_NOTE.Name = "listBox_NOTE";
            listBox_NOTE.Size = new Size(228, 139);
            listBox_NOTE.TabIndex = 2;
            // 
            // button_ORDER
            // 
            button_ORDER.Location = new Point(345, 262);
            button_ORDER.Margin = new Padding(3, 2, 3, 2);
            button_ORDER.Name = "button_ORDER";
            button_ORDER.Size = new Size(82, 22);
            button_ORDER.TabIndex = 3;
            button_ORDER.Text = "ORDER";
            button_ORDER.UseVisualStyleBackColor = true;
            button_ORDER.Click += button_Order_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(382, 9);
            label1.Name = "label1";
            label1.Size = new Size(59, 15);
            label1.TabIndex = 4;
            label1.Text = "THÊM ĐÁ";
            // 
            // button_BACK
            // 
            button_BACK.Location = new Point(7, 9);
            button_BACK.Margin = new Padding(3, 2, 3, 2);
            button_BACK.Name = "button_BACK";
            button_BACK.Size = new Size(82, 22);
            button_BACK.TabIndex = 5;
            button_BACK.Text = "BACK";
            button_BACK.UseVisualStyleBackColor = true;
            button_BACK.Click += button_BACK_Click;
            // 
            // label_DrinkName
            // 
            label_DrinkName.AutoSize = true;
            label_DrinkName.Location = new Point(234, 172);
            label_DrinkName.Name = "label_DrinkName";
            label_DrinkName.Size = new Size(68, 15);
            label_DrinkName.TabIndex = 6;
            label_DrinkName.Text = "Thong bao ";
            // 
            // DrinkPreferenceForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 338);
            Controls.Add(label_DrinkName);
            Controls.Add(button_BACK);
            Controls.Add(label1);
            Controls.Add(button_ORDER);
            Controls.Add(listBox_NOTE);
            Controls.Add(checkBox_NO_ICE);
            Controls.Add(checkBox_YES_ICE);
            Margin = new Padding(3, 2, 3, 2);
            Name = "DrinkPreferenceForm";
            Text = "Form1";
            Load += DrinkPreferenceForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBox_YES_ICE;
        private CheckBox checkBox_NO_ICE;
        private ListBox listBox1;
        private Button button1;
        private Label label1;
        private ListBox listBox_NOTE;
        private Button button_ORDER;
        private Button button_BACK;
        private Label label_DrinkName;
    }
}
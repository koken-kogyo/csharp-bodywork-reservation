namespace BodyWorkReservation
{
    partial class FormBodyworkPopup
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
            panel1 = new Panel();
            buttonSaveCulture = new Button();
            comboBoxCulture = new ComboBox();
            labelOtherSymptoms = new Label();
            labelTreatment = new Label();
            textBoxNote = new TextBox();
            comboBoxTreatment = new ComboBox();
            labelTimeSlot = new Label();
            labelReservDt = new Label();
            labelEmployee = new Label();
            panel2 = new Panel();
            buttonCancelAppoint = new Button();
            buttonAppoint = new Button();
            buttonClose = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonSaveCulture);
            panel1.Controls.Add(comboBoxCulture);
            panel1.Controls.Add(labelOtherSymptoms);
            panel1.Controls.Add(labelTreatment);
            panel1.Controls.Add(textBoxNote);
            panel1.Controls.Add(comboBoxTreatment);
            panel1.Controls.Add(labelTimeSlot);
            panel1.Controls.Add(labelReservDt);
            panel1.Controls.Add(labelEmployee);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(5);
            panel1.Name = "panel1";
            panel1.Size = new Size(598, 462);
            panel1.TabIndex = 0;
            // 
            // buttonSaveCulture
            // 
            buttonSaveCulture.Font = new Font("Arial Narrow", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonSaveCulture.ForeColor = SystemColors.ControlDarkDark;
            buttonSaveCulture.Location = new Point(172, 12);
            buttonSaveCulture.Name = "buttonSaveCulture";
            buttonSaveCulture.Size = new Size(215, 28);
            buttonSaveCulture.TabIndex = 8;
            buttonSaveCulture.Text = "言語設定を保存";
            buttonSaveCulture.UseVisualStyleBackColor = true;
            buttonSaveCulture.Click += ButtonSaveCulture_Click;
            // 
            // comboBoxCulture
            // 
            comboBoxCulture.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCulture.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxCulture.ForeColor = SystemColors.ControlDarkDark;
            comboBoxCulture.FormattingEnabled = true;
            comboBoxCulture.Items.AddRange(new object[] { "Japanese (ja-JP)", "English (en-US)", "Português (pt-BR)" });
            comboBoxCulture.Location = new Point(12, 12);
            comboBoxCulture.Name = "comboBoxCulture";
            comboBoxCulture.Size = new Size(154, 28);
            comboBoxCulture.TabIndex = 7;
            // 
            // labelOtherSymptoms
            // 
            labelOtherSymptoms.Font = new Font("HGPｺﾞｼｯｸM", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            labelOtherSymptoms.Location = new Point(12, 233);
            labelOtherSymptoms.Name = "labelOtherSymptoms";
            labelOtherSymptoms.Size = new Size(443, 32);
            labelOtherSymptoms.TabIndex = 6;
            labelOtherSymptoms.Text = "その他お悩み事：";
            labelOtherSymptoms.TextAlign = ContentAlignment.BottomLeft;
            // 
            // labelTreatment
            // 
            labelTreatment.Font = new Font("HGPｺﾞｼｯｸE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            labelTreatment.Location = new Point(12, 182);
            labelTreatment.Name = "labelTreatment";
            labelTreatment.Size = new Size(174, 47);
            labelTreatment.TabIndex = 5;
            labelTreatment.Text = "施術内容：";
            labelTreatment.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBoxNote
            // 
            textBoxNote.Font = new Font("HGPｺﾞｼｯｸE", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBoxNote.ImeMode = ImeMode.On;
            textBoxNote.Location = new Point(12, 268);
            textBoxNote.Multiline = true;
            textBoxNote.Name = "textBoxNote";
            textBoxNote.Size = new Size(574, 182);
            textBoxNote.TabIndex = 4;
            // 
            // comboBoxTreatment
            // 
            comboBoxTreatment.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTreatment.Font = new Font("HGPｺﾞｼｯｸE", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            comboBoxTreatment.FormattingEnabled = true;
            comboBoxTreatment.Location = new Point(192, 182);
            comboBoxTreatment.Name = "comboBoxTreatment";
            comboBoxTreatment.Size = new Size(394, 43);
            comboBoxTreatment.TabIndex = 3;
            // 
            // labelTimeSlot
            // 
            labelTimeSlot.BorderStyle = BorderStyle.FixedSingle;
            labelTimeSlot.Font = new Font("HGPｺﾞｼｯｸE", 26.25F);
            labelTimeSlot.Location = new Point(192, 125);
            labelTimeSlot.Name = "labelTimeSlot";
            labelTimeSlot.Size = new Size(394, 47);
            labelTimeSlot.TabIndex = 2;
            labelTimeSlot.Text = "16:20 ～ 16:50";
            labelTimeSlot.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelReservDt
            // 
            labelReservDt.BorderStyle = BorderStyle.FixedSingle;
            labelReservDt.Font = new Font("HGPｺﾞｼｯｸE", 26.25F);
            labelReservDt.Location = new Point(12, 125);
            labelReservDt.Name = "labelReservDt";
            labelReservDt.Size = new Size(174, 47);
            labelReservDt.TabIndex = 1;
            labelReservDt.Text = "12/31";
            labelReservDt.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelEmployee
            // 
            labelEmployee.BorderStyle = BorderStyle.FixedSingle;
            labelEmployee.Font = new Font("HGPｺﾞｼｯｸE", 36F, FontStyle.Regular, GraphicsUnit.Point, 128);
            labelEmployee.Location = new Point(12, 50);
            labelEmployee.Name = "labelEmployee";
            labelEmployee.Size = new Size(574, 69);
            labelEmployee.TabIndex = 0;
            labelEmployee.Text = "コーケン太郎";
            labelEmployee.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(buttonCancelAppoint);
            panel2.Controls.Add(buttonAppoint);
            panel2.Controls.Add(buttonClose);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 462);
            panel2.Name = "panel2";
            panel2.Size = new Size(598, 101);
            panel2.TabIndex = 1;
            // 
            // buttonCancelAppoint
            // 
            buttonCancelAppoint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCancelAppoint.Font = new Font("HGPｺﾞｼｯｸE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            buttonCancelAppoint.ForeColor = Color.LightCoral;
            buttonCancelAppoint.Location = new Point(298, 8);
            buttonCancelAppoint.Name = "buttonCancelAppoint";
            buttonCancelAppoint.Size = new Size(137, 83);
            buttonCancelAppoint.TabIndex = 2;
            buttonCancelAppoint.Text = "予約取消";
            buttonCancelAppoint.UseVisualStyleBackColor = true;
            buttonCancelAppoint.Click += ButtonCancelAppoint_Click;
            // 
            // buttonAppoint
            // 
            buttonAppoint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAppoint.Font = new Font("HGPｺﾞｼｯｸE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            buttonAppoint.ForeColor = Color.LightCoral;
            buttonAppoint.Location = new Point(441, 8);
            buttonAppoint.Name = "buttonAppoint";
            buttonAppoint.Size = new Size(145, 83);
            buttonAppoint.TabIndex = 1;
            buttonAppoint.Text = "予約";
            buttonAppoint.UseVisualStyleBackColor = true;
            buttonAppoint.Click += ButtonAppoint_Click;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonClose.Font = new Font("HGPｺﾞｼｯｸE", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            buttonClose.ForeColor = SystemColors.ControlDarkDark;
            buttonClose.Location = new Point(155, 8);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(137, 83);
            buttonClose.TabIndex = 0;
            buttonClose.Text = "閉じる";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += ButtonClose_Click;
            // 
            // FormBodyworkPopup
            // 
            AutoScaleDimensions = new SizeF(11F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(598, 563);
            ControlBox = false;
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("HGPｺﾞｼｯｸE", 15.75F);
            KeyPreview = true;
            Margin = new Padding(5, 4, 5, 4);
            Name = "FormBodyworkPopup";
            Text = "予約画面";
            KeyDown += FormBodyworkPopup_KeyDown;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label labelTimeSlot;
        private Label labelReservDt;
        private Label labelEmployee;
        private Panel panel2;
        private TextBox textBoxNote;
        private ComboBox comboBoxTreatment;
        private Button buttonAppoint;
        private Button buttonClose;
        private Button buttonCancelAppoint;
        private Label labelOtherSymptoms;
        private Label labelTreatment;
        private Button buttonSaveCulture;
        private ComboBox comboBoxCulture;
    }
}
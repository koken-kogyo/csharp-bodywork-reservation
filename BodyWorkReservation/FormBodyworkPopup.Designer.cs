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
            label2 = new Label();
            label1 = new Label();
            textBoxNote = new TextBox();
            comboBoxTreatment = new ComboBox();
            labelTimeSlot = new Label();
            labelReservDt = new Label();
            labelEmployee = new Label();
            panel2 = new Panel();
            buttonCancelAppoint = new Button();
            buttonAppoint = new Button();
            buttonCancel = new Button();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBoxNote);
            panel1.Controls.Add(comboBoxTreatment);
            panel1.Controls.Add(labelTimeSlot);
            panel1.Controls.Add(labelReservDt);
            panel1.Controls.Add(labelEmployee);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(5);
            panel1.Name = "panel1";
            panel1.Size = new Size(542, 421);
            panel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.Font = new Font("HGPｺﾞｼｯｸM", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label2.Location = new Point(12, 198);
            label2.Name = "label2";
            label2.Size = new Size(443, 32);
            label2.TabIndex = 6;
            label2.Text = "その他お悩み事：";
            label2.TextAlign = ContentAlignment.BottomLeft;
            // 
            // label1
            // 
            label1.Font = new Font("HGPｺﾞｼｯｸE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.Location = new Point(12, 147);
            label1.Name = "label1";
            label1.Size = new Size(138, 47);
            label1.TabIndex = 5;
            label1.Text = "施術内容：";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // textBoxNote
            // 
            textBoxNote.Font = new Font("HGPｺﾞｼｯｸE", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            textBoxNote.ImeMode = ImeMode.On;
            textBoxNote.Location = new Point(12, 233);
            textBoxNote.Multiline = true;
            textBoxNote.Name = "textBoxNote";
            textBoxNote.Size = new Size(518, 182);
            textBoxNote.TabIndex = 4;
            // 
            // comboBoxTreatment
            // 
            comboBoxTreatment.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTreatment.Font = new Font("HGPｺﾞｼｯｸE", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            comboBoxTreatment.FormattingEnabled = true;
            comboBoxTreatment.Location = new Point(192, 147);
            comboBoxTreatment.Name = "comboBoxTreatment";
            comboBoxTreatment.Size = new Size(338, 43);
            comboBoxTreatment.TabIndex = 3;
            // 
            // labelTimeSlot
            // 
            labelTimeSlot.BorderStyle = BorderStyle.FixedSingle;
            labelTimeSlot.Font = new Font("HGPｺﾞｼｯｸE", 26.25F);
            labelTimeSlot.Location = new Point(192, 90);
            labelTimeSlot.Name = "labelTimeSlot";
            labelTimeSlot.Size = new Size(338, 47);
            labelTimeSlot.TabIndex = 2;
            labelTimeSlot.Text = "16:20 ～ 16:50";
            labelTimeSlot.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelReservDt
            // 
            labelReservDt.BorderStyle = BorderStyle.FixedSingle;
            labelReservDt.Font = new Font("HGPｺﾞｼｯｸE", 26.25F);
            labelReservDt.Location = new Point(12, 90);
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
            labelEmployee.Location = new Point(12, 15);
            labelEmployee.Name = "labelEmployee";
            labelEmployee.Size = new Size(518, 69);
            labelEmployee.TabIndex = 0;
            labelEmployee.Text = "コーケン太郎";
            labelEmployee.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(buttonCancelAppoint);
            panel2.Controls.Add(buttonAppoint);
            panel2.Controls.Add(buttonCancel);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 421);
            panel2.Name = "panel2";
            panel2.Size = new Size(542, 108);
            panel2.TabIndex = 1;
            // 
            // buttonCancelAppoint
            // 
            buttonCancelAppoint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCancelAppoint.Font = new Font("HGPｺﾞｼｯｸE", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            buttonCancelAppoint.Location = new Point(242, 13);
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
            buttonAppoint.Location = new Point(385, 13);
            buttonAppoint.Name = "buttonAppoint";
            buttonAppoint.Size = new Size(145, 83);
            buttonAppoint.TabIndex = 1;
            buttonAppoint.Text = "予約";
            buttonAppoint.UseVisualStyleBackColor = true;
            buttonAppoint.Click += ButtonAppoint_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonCancel.Font = new Font("HGPｺﾞｼｯｸE", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            buttonCancel.Location = new Point(99, 13);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(137, 83);
            buttonCancel.TabIndex = 0;
            buttonCancel.Text = "閉じる";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += ButtonCancel_Click;
            // 
            // FormBodyworkPopup
            // 
            AutoScaleDimensions = new SizeF(11F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 529);
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
        private Button buttonCancel;
        private Button buttonCancelAppoint;
        private Label label2;
        private Label label1;
    }
}
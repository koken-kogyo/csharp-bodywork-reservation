namespace BodyWorkReservation
{
    partial class FormAnnouncement
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
            buttonUnderstand = new Button();
            labelAnnounce1 = new Label();
            labelAnnounce3 = new Label();
            labelAnnounce2 = new Label();
            comboBoxCulture = new ComboBox();
            buttonClose = new Button();
            SuspendLayout();
            // 
            // buttonUnderstand
            // 
            buttonUnderstand.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonUnderstand.ForeColor = Color.LightCoral;
            buttonUnderstand.Location = new Point(674, 272);
            buttonUnderstand.Margin = new Padding(5, 4, 5, 4);
            buttonUnderstand.Name = "buttonUnderstand";
            buttonUnderstand.Size = new Size(367, 85);
            buttonUnderstand.TabIndex = 0;
            buttonUnderstand.Text = "了解";
            buttonUnderstand.UseVisualStyleBackColor = true;
            buttonUnderstand.Click += ButtonUnderstand_Click;
            // 
            // labelAnnounce1
            // 
            labelAnnounce1.AutoSize = true;
            labelAnnounce1.Font = new Font("HGPｺﾞｼｯｸE", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            labelAnnounce1.Location = new Point(34, 33);
            labelAnnounce1.Margin = new Padding(5, 0, 5, 0);
            labelAnnounce1.Name = "labelAnnounce1";
            labelAnnounce1.Size = new Size(168, 37);
            labelAnnounce1.TabIndex = 1;
            labelAnnounce1.Text = "施術30分";
            // 
            // labelAnnounce3
            // 
            labelAnnounce3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelAnnounce3.Font = new Font("HGPｺﾞｼｯｸE", 18F, FontStyle.Regular, GraphicsUnit.Point, 128);
            labelAnnounce3.ForeColor = SystemColors.ControlDarkDark;
            labelAnnounce3.Location = new Point(84, 206);
            labelAnnounce3.Margin = new Padding(5, 0, 5, 0);
            labelAnnounce3.Name = "labelAnnounce3";
            labelAnnounce3.Size = new Size(957, 62);
            labelAnnounce3.TabIndex = 2;
            labelAnnounce3.Text = "会社補助額1,400円・給与天引き";
            labelAnnounce3.TextAlign = ContentAlignment.TopRight;
            // 
            // labelAnnounce2
            // 
            labelAnnounce2.Font = new Font("HGPｺﾞｼｯｸE", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            labelAnnounce2.Location = new Point(34, 99);
            labelAnnounce2.Margin = new Padding(5, 0, 5, 0);
            labelAnnounce2.Name = "labelAnnounce2";
            labelAnnounce2.Size = new Size(995, 97);
            labelAnnounce2.TabIndex = 3;
            labelAnnounce2.Text = "2,900円のところ 自己負担額1,500円でご利用いただけます";
            // 
            // comboBoxCulture
            // 
            comboBoxCulture.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxCulture.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCulture.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxCulture.ForeColor = SystemColors.ControlDarkDark;
            comboBoxCulture.FormattingEnabled = true;
            comboBoxCulture.Items.AddRange(new object[] { "Japanese (ja-JP)", "English (en-US)", "Português (pt-BR)" });
            comboBoxCulture.Location = new Point(889, 12);
            comboBoxCulture.Name = "comboBoxCulture";
            comboBoxCulture.Size = new Size(154, 28);
            comboBoxCulture.TabIndex = 8;
            comboBoxCulture.SelectedIndexChanged += comboBoxCulture_SelectedIndexChanged;
            // 
            // buttonClose
            // 
            buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonClose.ForeColor = SystemColors.ControlDarkDark;
            buttonClose.Location = new Point(34, 272);
            buttonClose.Margin = new Padding(5, 4, 5, 4);
            buttonClose.Name = "buttonClose";
            buttonClose.Size = new Size(367, 85);
            buttonClose.TabIndex = 9;
            buttonClose.Text = "閉じる";
            buttonClose.UseVisualStyleBackColor = true;
            buttonClose.Click += buttonClose_Click;
            // 
            // FormAnnouncement
            // 
            AutoScaleDimensions = new SizeF(11F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1055, 370);
            ControlBox = false;
            Controls.Add(buttonClose);
            Controls.Add(comboBoxCulture);
            Controls.Add(labelAnnounce2);
            Controls.Add(labelAnnounce3);
            Controls.Add(labelAnnounce1);
            Controls.Add(buttonUnderstand);
            Font = new Font("HGPｺﾞｼｯｸE", 15.75F);
            KeyPreview = true;
            Margin = new Padding(5, 4, 5, 4);
            Name = "FormAnnouncement";
            Text = "FormAnnouncement";
            KeyDown += FormAnnouncement_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonUnderstand;
        private Label labelAnnounce1;
        private Label labelAnnounce3;
        private Label labelAnnounce2;
        private ComboBox comboBoxCulture;
        private Button buttonClose;
    }
}
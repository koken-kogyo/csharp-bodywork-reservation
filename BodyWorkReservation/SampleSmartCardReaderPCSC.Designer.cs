namespace BodyWorkReservation
{
    partial class SampleBodyWorkReservationPCSC
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
            buttonCheckReader = new Button();
            buttonReadID = new Button();
            buttonReadCardType = new Button();
            comboBoxCardReader = new ComboBox();
            textBox_Log = new TextBox();
            buttonSuica090F = new Button();
            SuspendLayout();
            // 
            // buttonCheckReader
            // 
            buttonCheckReader.Location = new Point(12, 10);
            buttonCheckReader.Name = "buttonCheckReader";
            buttonCheckReader.Size = new Size(196, 54);
            buttonCheckReader.TabIndex = 0;
            buttonCheckReader.Text = "Check Reader Connection";
            buttonCheckReader.UseVisualStyleBackColor = true;
            buttonCheckReader.Click += ButtonCheckReader_Click;
            // 
            // buttonReadID
            // 
            buttonReadID.Location = new Point(12, 99);
            buttonReadID.Name = "buttonReadID";
            buttonReadID.Size = new Size(196, 54);
            buttonReadID.TabIndex = 1;
            buttonReadID.Text = "Read ID";
            buttonReadID.UseVisualStyleBackColor = true;
            buttonReadID.Click += ButtonReadID_Click;
            // 
            // buttonReadCardType
            // 
            buttonReadCardType.Location = new Point(12, 159);
            buttonReadCardType.Name = "buttonReadCardType";
            buttonReadCardType.Size = new Size(196, 54);
            buttonReadCardType.TabIndex = 2;
            buttonReadCardType.Text = "Read Card Type";
            buttonReadCardType.UseVisualStyleBackColor = true;
            buttonReadCardType.Click += ButtonReadCardType_Click;
            // 
            // comboBoxCardReader
            // 
            comboBoxCardReader.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxCardReader.FormattingEnabled = true;
            comboBoxCardReader.Location = new Point(12, 70);
            comboBoxCardReader.Name = "comboBoxCardReader";
            comboBoxCardReader.Size = new Size(196, 23);
            comboBoxCardReader.TabIndex = 3;
            comboBoxCardReader.SelectedIndexChanged += ComboBoxCardReader_SelectedIndexChanged;
            // 
            // textBox_Log
            // 
            textBox_Log.Location = new Point(214, 10);
            textBox_Log.Multiline = true;
            textBox_Log.Name = "textBox_Log";
            textBox_Log.Size = new Size(574, 428);
            textBox_Log.TabIndex = 4;
            // 
            // buttonSuica090F
            // 
            buttonSuica090F.Location = new Point(11, 230);
            buttonSuica090F.Name = "buttonSuica090F";
            buttonSuica090F.Size = new Size(197, 55);
            buttonSuica090F.TabIndex = 5;
            buttonSuica090F.Text = "Suica 090F";
            buttonSuica090F.UseVisualStyleBackColor = true;
            buttonSuica090F.Click += ButtonSuica090F_Click;
            // 
            // FormSample
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonSuica090F);
            Controls.Add(textBox_Log);
            Controls.Add(comboBoxCardReader);
            Controls.Add(buttonReadCardType);
            Controls.Add(buttonReadID);
            Controls.Add(buttonCheckReader);
            Name = "FormSample";
            Text = "FormSample";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonCheckReader;
        private Button buttonReadID;
        private Button buttonReadCardType;
        private ComboBox comboBoxCardReader;
        private TextBox textBox_Log;
        private Button buttonSuica090F;
    }
}

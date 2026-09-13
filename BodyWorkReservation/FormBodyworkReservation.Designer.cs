namespace BodyWorkReservation
{
    partial class FormBodyworkReservation
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
            buttonSearchEmpNo = new Button();
            label1 = new Label();
            textBoxEmpNo = new TextBox();
            labelEmpName = new Label();
            labelStatus = new Label();
            labelFelicaIDm = new Label();
            panel2 = new Panel();
            dataGridView1 = new DataGridView();
            Column1FirstThursday = new DataGridViewTextBoxColumn();
            Column2SecondThursday = new DataGridViewTextBoxColumn();
            Column3SeardThursday = new DataGridViewTextBoxColumn();
            Column4FourceThursday = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonSearchEmpNo);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBoxEmpNo);
            panel1.Controls.Add(labelEmpName);
            panel1.Controls.Add(labelStatus);
            panel1.Controls.Add(labelFelicaIDm);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(5, 4, 5, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(934, 128);
            panel1.TabIndex = 2;
            // 
            // buttonSearchEmpNo
            // 
            buttonSearchEmpNo.Font = new Font("HGPｺﾞｼｯｸE", 12F, FontStyle.Regular, GraphicsUnit.Point, 128);
            buttonSearchEmpNo.ForeColor = SystemColors.ControlDarkDark;
            buttonSearchEmpNo.Location = new Point(263, 83);
            buttonSearchEmpNo.Name = "buttonSearchEmpNo";
            buttonSearchEmpNo.Size = new Size(75, 29);
            buttonSearchEmpNo.TabIndex = 8;
            buttonSearchEmpNo.Text = "検索";
            buttonSearchEmpNo.UseVisualStyleBackColor = true;
            buttonSearchEmpNo.Click += ButtonSearchEmpNo_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Location = new Point(30, 88);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(126, 21);
            label1.TabIndex = 7;
            label1.Text = "従業員番号：";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBoxEmpNo
            // 
            textBoxEmpNo.ForeColor = SystemColors.ControlDarkDark;
            textBoxEmpNo.Location = new Point(153, 84);
            textBoxEmpNo.Name = "textBoxEmpNo";
            textBoxEmpNo.Size = new Size(100, 28);
            textBoxEmpNo.TabIndex = 6;
            textBoxEmpNo.TextAlign = HorizontalAlignment.Center;
            textBoxEmpNo.KeyDown += TextBoxEmpNo_KeyDown;
            // 
            // labelEmpName
            // 
            labelEmpName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelEmpName.AutoSize = true;
            labelEmpName.Font = new Font("HGPｺﾞｼｯｸE", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            labelEmpName.Location = new Point(469, 32);
            labelEmpName.Margin = new Padding(5, 0, 5, 0);
            labelEmpName.Name = "labelEmpName";
            labelEmpName.Size = new Size(445, 37);
            labelEmpName.TabIndex = 4;
            labelEmpName.Text = "こんにちは 「コーケン」 さん";
            labelEmpName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Font = new Font("HGPｺﾞｼｯｸE", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            labelStatus.Location = new Point(30, 34);
            labelStatus.Margin = new Padding(5, 0, 5, 0);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(102, 35);
            labelStatus.TabIndex = 2;
            labelStatus.Text = "label1";
            labelStatus.TextAlign = ContentAlignment.MiddleLeft;
            labelStatus.Click += LabelStatus_Click;
            // 
            // labelFelicaIDm
            // 
            labelFelicaIDm.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelFelicaIDm.ForeColor = SystemColors.ControlDarkDark;
            labelFelicaIDm.Location = new Point(636, 87);
            labelFelicaIDm.Margin = new Padding(5, 0, 5, 0);
            labelFelicaIDm.Name = "labelFelicaIDm";
            labelFelicaIDm.Size = new Size(260, 24);
            labelFelicaIDm.TabIndex = 5;
            labelFelicaIDm.Text = "00-00-00-00-00-00-00-00";
            labelFelicaIDm.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            panel2.Controls.Add(dataGridView1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 128);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10);
            panel2.Size = new Size(934, 350);
            panel2.TabIndex = 4;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridView1.ColumnHeadersHeight = 62;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1FirstThursday, Column2SecondThursday, Column3SeardThursday, Column4FourceThursday });
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(10, 10);
            dataGridView1.Margin = new Padding(0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 200;
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.Size = new Size(914, 330);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellClick += DataGridView1_CellClick;
            // 
            // Column1FirstThursday
            // 
            Column1FirstThursday.HeaderText = "Column1";
            Column1FirstThursday.Name = "Column1FirstThursday";
            Column1FirstThursday.ReadOnly = true;
            // 
            // Column2SecondThursday
            // 
            Column2SecondThursday.HeaderText = "Column2";
            Column2SecondThursday.Name = "Column2SecondThursday";
            Column2SecondThursday.ReadOnly = true;
            // 
            // Column3SeardThursday
            // 
            Column3SeardThursday.HeaderText = "Column3";
            Column3SeardThursday.Name = "Column3SeardThursday";
            Column3SeardThursday.ReadOnly = true;
            // 
            // Column4FourceThursday
            // 
            Column4FourceThursday.HeaderText = "Column4";
            Column4FourceThursday.Name = "Column4FourceThursday";
            Column4FourceThursday.ReadOnly = true;
            // 
            // FormBodyworkReservation
            // 
            AutoScaleDimensions = new SizeF(11F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(934, 478);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Font = new Font("HGPｺﾞｼｯｸE", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            KeyPreview = true;
            Margin = new Padding(5, 4, 5, 4);
            Name = "FormBodyworkReservation";
            Text = "FormBodyWorkReservation";
            Activated += DataGridViewSelectionClear;
            Load += FormBodyWorkReservation_Load;
            Shown += DataGridViewSelectionClear;
            KeyDown += FormBodyWorkReservation_KeyDown;
            Resize += FormBodyWorkReservation_Resize;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label labelEmpName;
        private Label labelStatus;
        private Label labelFelicaIDm;
        private Panel panel2;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1FirstThursday;
        private DataGridViewTextBoxColumn Column2SecondThursday;
        private DataGridViewTextBoxColumn Column3SeardThursday;
        private DataGridViewTextBoxColumn Column4FourceThursday;
        private TextBox textBoxEmpNo;
        private Button buttonSearchEmpNo;
        private Label label1;
    }
}
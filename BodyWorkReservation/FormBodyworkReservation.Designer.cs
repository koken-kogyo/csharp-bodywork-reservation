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
            buttonNextMonth = new Button();
            buttonPrevMonth = new Button();
            labelMonthly = new Label();
            buttonExportExcel = new Button();
            pictureBox1 = new PictureBox();
            buttonSearchEmpNo = new Button();
            label1 = new Label();
            textBoxEmpNo = new TextBox();
            labelEmpName = new Label();
            comboMonth = new ComboBox();
            panel2 = new Panel();
            dataGridView1 = new DataGridView();
            Column1FirstThursday = new DataGridViewTextBoxColumn();
            Column2SecondThursday = new DataGridViewTextBoxColumn();
            Column3SeardThursday = new DataGridViewTextBoxColumn();
            Column4FourceThursday = new DataGridViewTextBoxColumn();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            labelFelicaIDm2 = new ToolStripStatusLabel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(buttonNextMonth);
            panel1.Controls.Add(buttonPrevMonth);
            panel1.Controls.Add(labelMonthly);
            panel1.Controls.Add(buttonExportExcel);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(buttonSearchEmpNo);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(textBoxEmpNo);
            panel1.Controls.Add(labelEmpName);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(5, 4, 5, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(934, 120);
            panel1.TabIndex = 2;
            panel1.Click += Panel1_Click;
            // 
            // buttonNextMonth
            // 
            buttonNextMonth.Anchor = AnchorStyles.None;
            buttonNextMonth.Font = new Font("HGPｺﾞｼｯｸE", 9.75F);
            buttonNextMonth.ForeColor = SystemColors.ControlDarkDark;
            buttonNextMonth.Location = new Point(595, 74);
            buttonNextMonth.Name = "buttonNextMonth";
            buttonNextMonth.Size = new Size(71, 35);
            buttonNextMonth.TabIndex = 12;
            buttonNextMonth.Text = "翌月";
            buttonNextMonth.UseVisualStyleBackColor = true;
            buttonNextMonth.Click += ButtonNextMonth_Click;
            // 
            // buttonPrevMonth
            // 
            buttonPrevMonth.Anchor = AnchorStyles.Left;
            buttonPrevMonth.Font = new Font("HGPｺﾞｼｯｸE", 9.75F);
            buttonPrevMonth.ForeColor = SystemColors.ControlDarkDark;
            buttonPrevMonth.Location = new Point(333, 74);
            buttonPrevMonth.Name = "buttonPrevMonth";
            buttonPrevMonth.Size = new Size(72, 35);
            buttonPrevMonth.TabIndex = 11;
            buttonPrevMonth.Text = "前月";
            buttonPrevMonth.UseVisualStyleBackColor = true;
            buttonPrevMonth.Click += ButtonPrevMonth_Click;
            // 
            // labelMonthly
            // 
            labelMonthly.Anchor = AnchorStyles.Left;
            labelMonthly.AutoSize = true;
            labelMonthly.Font = new Font("HGPｺﾞｼｯｸE", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            labelMonthly.Location = new Point(413, 74);
            labelMonthly.Margin = new Padding(5, 0, 5, 0);
            labelMonthly.Name = "labelMonthly";
            labelMonthly.Size = new Size(174, 35);
            labelMonthly.TabIndex = 2;
            labelMonthly.Text = "9月度開催";
            labelMonthly.TextAlign = ContentAlignment.BottomLeft;
            labelMonthly.Click += LabelMonthly_Click;
            // 
            // buttonExportExcel
            // 
            buttonExportExcel.Anchor = AnchorStyles.None;
            buttonExportExcel.Font = new Font("HGPｺﾞｼｯｸE", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            buttonExportExcel.ForeColor = SystemColors.ControlDarkDark;
            buttonExportExcel.Location = new Point(667, 80);
            buttonExportExcel.Name = "buttonExportExcel";
            buttonExportExcel.Size = new Size(111, 29);
            buttonExportExcel.TabIndex = 10;
            buttonExportExcel.Text = "集計データ出力";
            buttonExportExcel.UseVisualStyleBackColor = true;
            buttonExportExcel.Click += ButtonExportExcel_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.掛川整体からだや;
            pictureBox1.Location = new Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(312, 114);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // buttonSearchEmpNo
            // 
            buttonSearchEmpNo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonSearchEmpNo.Font = new Font("HGPｺﾞｼｯｸE", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 128);
            buttonSearchEmpNo.ForeColor = SystemColors.ControlDarkDark;
            buttonSearchEmpNo.Location = new Point(848, 84);
            buttonSearchEmpNo.Name = "buttonSearchEmpNo";
            buttonSearchEmpNo.Size = new Size(75, 29);
            buttonSearchEmpNo.TabIndex = 8;
            buttonSearchEmpNo.Text = "検索";
            buttonSearchEmpNo.UseVisualStyleBackColor = true;
            buttonSearchEmpNo.Click += ButtonSearchEmpNo_Click;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Font = new Font("HGPｺﾞｼｯｸE", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 128);
            label1.ForeColor = SystemColors.ControlDarkDark;
            label1.Location = new Point(616, 90);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(114, 19);
            label1.TabIndex = 7;
            label1.Text = "従業員番号：";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBoxEmpNo
            // 
            textBoxEmpNo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxEmpNo.ForeColor = SystemColors.ControlDarkDark;
            textBoxEmpNo.Location = new Point(738, 85);
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
            labelEmpName.Location = new Point(475, 30);
            labelEmpName.Margin = new Padding(5, 0, 5, 0);
            labelEmpName.Name = "labelEmpName";
            labelEmpName.Size = new Size(445, 37);
            labelEmpName.TabIndex = 4;
            labelEmpName.Text = "こんにちは 「コーケン」 さん";
            labelEmpName.TextAlign = ContentAlignment.MiddleRight;
            // 
            // comboMonth
            // 
            comboMonth.FormattingEnabled = true;
            comboMonth.Location = new Point(408, -8);
            comboMonth.Name = "comboMonth";
            comboMonth.Size = new Size(174, 29);
            comboMonth.TabIndex = 13;
            comboMonth.Visible = false;
            comboMonth.SelectedIndexChanged += ComboMonth_SelectedIndexChanged;
            comboMonth.DropDownClosed += comboMonth_DropDownClosed;
            // 
            // panel2
            // 
            panel2.Controls.Add(comboMonth);
            panel2.Controls.Add(dataGridView1);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 120);
            panel2.Margin = new Padding(0);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10, 5, 10, 5);
            panel2.Size = new Size(934, 336);
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
            dataGridView1.Location = new Point(10, 5);
            dataGridView1.Margin = new Padding(0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 200;
            dataGridView1.RowTemplate.Height = 40;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.Size = new Size(914, 326);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellClick += DataGridView1_CellClick;
            dataGridView1.RowHeaderMouseDoubleClick += DataGridView1_RowHeaderMouseDoubleClick;
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
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, labelFelicaIDm2 });
            statusStrip1.Location = new Point(0, 456);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(934, 22);
            statusStrip1.TabIndex = 5;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // labelFelicaIDm2
            // 
            labelFelicaIDm2.ForeColor = SystemColors.ControlDarkDark;
            labelFelicaIDm2.Name = "labelFelicaIDm2";
            labelFelicaIDm2.Size = new Size(801, 17);
            labelFelicaIDm2.Spring = true;
            labelFelicaIDm2.Text = "00-00-00-00-00-00-00-00";
            labelFelicaIDm2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // FormBodyworkReservation
            // 
            AutoScaleDimensions = new SizeF(11F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(934, 478);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(statusStrip1);
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
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label labelEmpName;
        private Label labelMonthly;
        private Panel panel2;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1FirstThursday;
        private DataGridViewTextBoxColumn Column2SecondThursday;
        private DataGridViewTextBoxColumn Column3SeardThursday;
        private DataGridViewTextBoxColumn Column4FourceThursday;
        private TextBox textBoxEmpNo;
        private Button buttonSearchEmpNo;
        private Label label1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private PictureBox pictureBox1;
        private ToolStripStatusLabel labelFelicaIDm2;
        private Button buttonExportExcel;
        private Button buttonNextMonth;
        private Button buttonPrevMonth;
        private ComboBox comboMonth;
    }
}
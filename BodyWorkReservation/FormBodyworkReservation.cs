using PCSC;
using PCSC.Exceptions;
using PCSC.Monitoring;
using Timer = System.Windows.Forms.Timer;

namespace BodyWorkReservation
{
    public partial class FormBodyworkReservation : Form
    {
        private DateTime _currentDate = DateTime.Today;
        private readonly List<DateTime> _thursdays = [];
        private bool _isAdministrator = false;
        private string _empno = string.Empty;
        private string _empname = string.Empty;
        private string _culturecd = string.Empty;

        private ISCardMonitor? _monitor = null;
        private string _readerName = "Sony FeliCa Port/PaSoRi 3.0 0";

        // 自動ログアウト機能
        private readonly IdleMessageFilter _filter = new();
        private readonly Timer idleTimer = new();

        // コンストラクタ
        public FormBodyworkReservation()
        {
            InitializeComponent();

            // タイトルバー非表示
            this.FormBorderStyle = FormBorderStyle.None;

            // 最大化表示
            this.WindowState = FormWindowState.Maximized;

            // Logo の読み込み
            pictureBox1.Image = Properties.Resources.Logo;
        }

        // フォームロードで各種初期設定を行う
        private void FormBodyWorkReservation_Load(object sender, EventArgs e)
        {
            labelEmpName.Text = "";
            labelFelicaIDm2.Text = "";
            buttonExportExcel.Visible = false;

            // 年月選択コンボボックスの初期化
            SetupMonthCombo();
            comboMonth.Visible = false;

            // 初期日付の設定と初期データの読み込み
            var d = DateTime.Today;
            _currentDate = (d.Day > 20)
                ? new DateTime(d.Year, d.Month, 1).AddMonths(1)
                : new DateTime(d.Year, d.Month, 1);
            ReadMonthlyData();
            
            // 最終木曜日の翌日から２０日迄の場合は翌月の予約を表示する
            var lastThursday = _thursdays.LastOrDefault();
            if (lastThursday.Day < d.Day && d.Day <= 20)
            {
                _currentDate = _currentDate.AddMonths(1);
                ReadMonthlyData();
            }

            // カードリーダー初期化
            InitializePcscMonitor();
        }

        private void ReadMonthlyData()
        {
            // 月度表示
            labelMonthly.Text = _currentDate.ToString("M月度開催");

            // データグリッドビューの列を生成
            GenerateThursdayColumns();
            for (int i = 0; i < 6; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].HeaderCell.Value = Common.TIMESLOT_NAME[i];
            }
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.RowHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;

            // データベース読み込み
            予約データ取得();
            自分の予約一覧に色を付ける();

            // セル幅とセル高さを等分に分ける
            AdjustColumnWidth();
            AdjustRowHeight();
            AdjustFontSize();
        }
        // 「前月」ボタン
        private void ButtonPrevMonth_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Today;
            if (now.Day > 20) now = now.AddMonths(1);
            if (_currentDate.AddMonths(-1) < now.AddMonths(-7)) return;
            _currentDate = _currentDate.AddMonths(-1);
            ReadMonthlyData();
            DataGridViewSelectionClear(sender, e);
        }
        // 「翌月」ボタン
        private void ButtonNextMonth_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Today;
            if (now.Day > 20) now = now.AddMonths(1);
            if (_currentDate.AddMonths(1) > now.AddMonths(3)) return;
            _currentDate = _currentDate.AddMonths(1);
            ReadMonthlyData();
            DataGridViewSelectionClear(sender, e);
        }
        // データグリッドの選択状態をクリアするにはActivatedかShownしかない！
        private void DataGridViewSelectionClear(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }
        private void FormBodyWorkReservation_Resize(object sender, EventArgs e)
        {
            AdjustColumnWidth();
            AdjustRowHeight();
            AdjustFontSize();
        }

        // （見た目の初期設定）データグリッドビューの列幅を再計算（行ヘッダーは列幅の1.5倍に設定）
        private void AdjustColumnWidth()
        {
            if (this.WindowState == FormWindowState.Minimized) return;
            if (_thursdays.Count == 0) return;
            int totalWidth = panel2.Width - panel2.Padding.Left - panel2.Padding.Right;
            int colWidth = (int)(totalWidth / (_thursdays.Count + 1.5));
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.Width = colWidth;
                totalWidth -= colWidth;
            }
            dataGridView1.RowHeadersWidth = totalWidth;
        }

        // （見た目の初期設定）データグリッドビューの行高さを7等分で再計算
        private void AdjustRowHeight()
        {
            if (this.WindowState == FormWindowState.Minimized) return;
            int rows = 7;
            int availableHeight = panel2.Height - panel2.Padding.Top - panel2.Padding.Bottom;
            if (availableHeight <= 0) return;
            int rowHeight = availableHeight / rows;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Height = rowHeight;
                availableHeight -= rowHeight;
            }
            dataGridView1.ColumnHeadersHeight = availableHeight;
        }

        // （見た目の初期設定）データグリッドビューのフォントサイズを再計算
        private void AdjustFontSize()
        {
            if (this.WindowState == FormWindowState.Minimized) return;
            int formWidth = this.ClientSize.Width;

            // 横幅に応じてフォントサイズを決める（レスポンシブ）
            float fontSize = formWidth * 0.014f;   // 実務で最も自然に見える係数

            // 最小・最大フォントサイズ（崩れ防止）
            if (fontSize < 10f) fontSize = 10f;
            if (fontSize > 32f) fontSize = 32f;

            // ★フォント名を HGPｺﾞｼｯｸE に固定
            var font = new Font("HGPｺﾞｼｯｸE", fontSize);

            dataGridView1.DefaultCellStyle.Font = font;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = font;
            dataGridView1.RowHeadersDefaultCellStyle.Font = font;

            labelEmpName.AutoSize = false;
            labelEmpName.TextAlign = ContentAlignment.MiddleRight;
            labelEmpName.Font = font;

            float fontSizeM = formWidth * 0.018f;
            var fontM = new Font("HGPｺﾞｼｯｸE", fontSizeM);
            labelMonthly.Font = fontM;
            comboMonth.Font = fontM;
            comboMonth.Width = (int)(labelMonthly.Width * 1.3f);

            var h = labelMonthly.Height;
            var base_y = panel1.Height - 5;
            labelMonthly.Top = base_y - h - 1;
            buttonPrevMonth.Height = h;
            buttonPrevMonth.Top = base_y - h;
            buttonNextMonth.Height = h;
            buttonNextMonth.Top = base_y - h;
            buttonNextMonth.Left = labelMonthly.Right + 12;
            buttonExportExcel.Height = h;
            buttonExportExcel.Top = base_y - h;
            buttonExportExcel.Left = buttonNextMonth.Right + 12;

        }

        // キーボードショートカット「Esc」でプログラム終了
        private void FormBodyWorkReservation_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _monitor = null;
                this.Close();
            }
        }
        // （隠れコマンド）④ダブルクリックでプログラム終了
        private void DataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == 3 && e.ColumnIndex == -1)
            {
                _monitor = null;
                this.Close();
            }
        }



        /*
         * 年月選択コンボボックス関連の処理ここから
         */
        private void SetupMonthCombo()
        {
            comboMonth.DropDownStyle = ComboBoxStyle.DropDownList;

            // 今日を基準に過去6か月〜未来3か月を作成
            DateTime now = DateTime.Today;
            if (now.Day > 20) now = now.AddMonths(1);

            for (int i = -6; i <= 3; i++)
            {
                DateTime target = new DateTime(now.Year, now.Month, 1).AddMonths(i);
                comboMonth.Items.Add(target.ToString("yyyy年 MM月"));
            }

            // 初期選択（今月）
            comboMonth.SelectedItem = new DateTime(now.Year, now.Month, 1).ToString("yyyy年 MM月");
        }
        private void LabelMonthly_Click(object sender, EventArgs e)
        {
            if (comboMonth.Visible)
            {
                comboMonth.Visible = false;
            }
            else
            {
                comboMonth.Left = labelMonthly.Left;
                comboMonth.Top = 0;
                comboMonth.SelectedItem = _currentDate.ToString("yyyy年 MM月");

                comboMonth.Visible = true;
                comboMonth.BringToFront();

                comboMonth.DroppedDown = true;   // ★これで即展開
            }
        }
        private void ComboMonth_DropDownClosed(object sender, EventArgs e)
        {
            DataGridViewSelectionClear(sender, e);
            comboMonth.Visible = false;
        }

        private void ComboMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboMonth.SelectedItem == null) return;
            string s = comboMonth.SelectedItem.ToString() ?? "1900年 01月";
            int y = Convert.ToInt32(s.Split("年")[0]);
            int m = Convert.ToInt32(s.Split("年")[1].Split("月")[0]);
            _currentDate = new DateTime(y, m, 1);
            ReadMonthlyData();
        }
        private void Panel1_Click(object sender, EventArgs e)
        {
            comboMonth.Visible = false;
        }

        /*
         * 年月選択コンボボックス関連の処理ここまで
         */




        /*
         * Excel出力関連の処理ここから
         */
        private void ButtonExportExcel_Click(object sender, EventArgs e)
        {
            if (!Common.IsExcelInstalled())
            {
                MessageBox.Show("Microsoft Excel がインストールされていません．", "Excel出力",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            int m = _currentDate.Month;
            string saveFullPath = @$"{desktop}\からだや予約集計_{m}月度.xlsx";
            if (Path.Exists(saveFullPath))
            {
                if (MessageBox.Show($"既にファイルが存在しています．\n上書きしてもよろしいですか？", "上書き確認",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    File.Delete(saveFullPath);
                }
                else
                {
                    return;
                }
            }
            // 実績一覧Excelを出力し別プロセスで開く
            Common.ExportExcel(dataGridView1, saveFullPath);
        }
        /*
         * Excel出力関連の処理ここまで
         */





        /*
         * データグリッド関連の処理ここから
         */

        /// <summary>
        /// 20日締めの木曜日を1か月分抽出して列タイトルにする
        /// </summary>
        private void GenerateThursdayColumns()
        {
            dataGridView1.Columns.Clear();
            _thursdays.Clear();

            // 締め日が過ぎているかどうかで期間を決める
            DateTime periodEnd = new(_currentDate.Year, _currentDate.Month, 20);
            DateTime periodStart = periodEnd.AddMonths(-1).AddDays(1); // 前月21日

            // 木曜日だけ抽出
            for (DateTime d = periodStart; d <= periodEnd; d = d.AddDays(1))
            {
                if (d.DayOfWeek == DayOfWeek.Thursday)
                {
                    _thursdays.Add(d);
                }
            }

            // 列追加
            foreach (var th in _thursdays)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    HeaderText = th.ToString("M/d (木)"),
                    Name = th.ToString("yyyyMMdd")
                };
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.Columns.Add(col);
            }
        }

        // 「セル」クリックイベント
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                DataGridViewSelectionClear(sender, e);
                return;
            }
            var cell = dataGridView1[e.ColumnIndex, e.RowIndex];
            if (_empno == string.Empty)
            {
                if (cell.Style.BackColor != Color.LightSlateGray)
                {
                    var msg = (_monitor != null) ? "社員証を読み取るか、" : "";
                    msg += "従業員番号を入力してください．";
                    MessageBox.Show(msg, "予約登録", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxEmpNo.Focus();
                }
                DataGridViewSelectionClear(sender, e);
                return;
            }
#pragma warning disable CS8622
            idleTimer.Stop();
            _filter.UserActivity -= ResetIdleTimer;
            //Debug.Print(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " StopCellClick");
#pragma warning restore CS8622
            var order = (cell.Value is Common.Order cellOrder)
                ? cellOrder
                : null;
            if (order == null)
            {
                if (cell.Style.BackColor == Color.LightSlateGray) // ①②の重複予約不可
                {
                    DataGridViewSelectionClear(sender, e);
                    return;
                }
                order = new Common.Order
                {
                    ReservDt = _thursdays[e.ColumnIndex],
                    TimeSlot = e.RowIndex,
                    EmpNo = _empno,
                    EmpName = _empname,
                };
                // お知らせの表示
                Form frmAnn = new FormAnnouncement()
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                var resultAnn = frmAnn.ShowDialog();
                if (resultAnn != DialogResult.Cancel)
                {
                    // 予約画面表示
                    Form frm = new FormBodyworkPopup(true, order)
                    {
                        StartPosition = FormStartPosition.CenterParent
                        // セルの横にポップアップさせる処理は廃止↓
                        // StartPosition = FormStartPosition.Manual,
                        // Location = PopupPoint(sender, e) 
                    };
                    var resultPop = frm.ShowDialog();
                    if (resultPop == DialogResult.OK)
                    {
                        cell.Value = order;
                    }
                }
            }
            else
            {
                if (order.EmpNo != _empno && _isAdministrator == false)
                {
                    DataGridViewSelectionClear(sender, e);
                    return;
                }
                Form frm = new FormBodyworkPopup(false, order)
                {
                    StartPosition = FormStartPosition.CenterParent,
                    // セルの横にポップアップさせる処理は廃止↓
                    // StartPosition = FormStartPosition.Manual,
                    // Location = PopupPoint(sender, e)
                };
                var resultPop = frm.ShowDialog();
                if (resultPop == DialogResult.No) // 予約取消
                {
                    cell.Value = null;
                }
            }
            自分の予約一覧に色を付ける();
#pragma warning disable CS8622
            idleTimer.Start();
            _filter.UserActivity += ResetIdleTimer;
            //Debug.Print(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " StartCellClick");
#pragma warning restore CS8622
        }
        // ポップアップウィンドウを表示させる位置を計算
        private Point PopupPoint(object sender, DataGridViewCellEventArgs e)
        {
            // セルの画面座標を取得（0:列ヘッダー行の下側に固定表示させる）
            Rectangle cellRect = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, 0, false);
            Point cellScreenPos = dataGridView1.PointToScreen(cellRect.Location);

            int cellRight = cellScreenPos.X + cellRect.Width;
            int cellLeft = cellScreenPos.X;
            int subWidth = 483; // サブフォームの幅

            // スクリーンがnullの場合、画面の右端
            if (Screen.PrimaryScreen == null)
            {
                return new Point(cellRight, cellLeft);
            }
            int screenRight = Screen.PrimaryScreen.WorkingArea.Right;

            // 配置位置（縦はセルのYそのまま）
            int x;
            // 右側に置くと画面からはみ出す？
            if (cellRight + subWidth > screenRight)
            {
                x = cellLeft - subWidth;// → 左側に置く
            }
            else
            {
                x = cellRight;          // → 通常は右側に置く
            }
            int y = cellScreenPos.Y;    // 縦位置は固定

            return new Point(x, y);
        }

        private void 自分の予約一覧に色を付ける()
        {
            // データグリッドビューのセルを初期化
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    int rowIdx = cell.RowIndex;
                    int colIdx = cell.ColumnIndex;
                    if (cell.Value is Common.Order order)
                    {
                        // 自分の予約に色を付ける
                        cell.Style.BackColor = (order.EmpNo == _empno || _isAdministrator)
                            ? dataGridView1.DefaultCellStyle.SelectionBackColor
                            : Color.LightSlateGray;

                        // ②に予約が入っている時は①の予約を出来ないようグレーアウトする
                        if (rowIdx == 1)
                        {
                            dataGridView1[colIdx, 0].Style.BackColor = Color.LightSlateGray;
                            dataGridView1[colIdx, 0].Value = "－";
                        }
                        // ③当然過去日付はグレーアウトする
                        if (_thursdays[colIdx] < DateTime.Today)
                        {
                            cell.Style.BackColor = Color.LightSlateGray;
                        }
                    }
                    else
                    {
                        // ②に予約が入っていなくても①に予約が入っている時はグレーアウトする
                        if (rowIdx == 1 && dataGridView1[colIdx, 0].Value is Common.Order)
                        {
                            dataGridView1[colIdx, 1].Style.BackColor = Color.LightSlateGray;
                            dataGridView1[colIdx, 1].Value = "－";
                        }
                        else
                        {
                            dataGridView1[colIdx, rowIdx].Style.BackColor =
                                dataGridView1.DefaultCellStyle.BackColor;
                        }
                        // ③当然過去日付はグレーアウトする
                        if (_thursdays[colIdx] < DateTime.Today)
                        {
                            cell.Style.BackColor = Color.LightSlateGray;
                            cell.Value = "－";
                        }
                    }
                    cell.Style.ForeColor = Color.White;
                }
            }
        }
        /*
         * データグリッド関連の処理ここまで
         */






        /*
         * データベース関連ここから
         */

        // 予約データの取得とデータグリッドビューへの反映
        private void 予約データ取得()
        {
            if (_thursdays == null || _thursdays.Count == 0) return;

            //// データグリッドビューのセルを初期化
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        cell.Value = null;
            //        cell.Style.ForeColor = dataGridView1.DefaultCellStyle.ForeColor;
            //        cell.Style.BackColor = dataGridView1.DefaultCellStyle.BackColor;
            //    }
            //}

            var dt = DBManager_MySQL.予約データ取得(_thursdays);
            buttonExportExcel.Enabled = (dt.Rows.Count > 0);
            foreach (System.Data.DataRow dr in dt.Rows)
            {
                DateTime reservdt = Convert.ToDateTime(dr["RESERVDT"]);
                int timeslot = Convert.ToInt32(dr["TIMESLOT"]);
                string empno = dr["EMPNO"].ToString() ?? "";
                string empname = dr["NAME"].ToString() ?? "";
                string treatment = dr["TREATMENT"].ToString() ?? "";
                string note = dr["NOTE"].ToString() ?? "";
                var order = new Common.Order
                {
                    ReservDt = reservdt,
                    TimeSlot = timeslot,
                    EmpNo = empno,
                    EmpName = empname,
                    Treatment = treatment,
                    Note = note
                };
                int colIndex = _thursdays.FindIndex(d => d.Date == reservdt.Date);
                if (colIndex != -1 && timeslot >= 0 && timeslot < dataGridView1.Rows.Count)
                {
                    var cell = dataGridView1[colIndex, timeslot];
                    cell.Value = order;
                }
            }
        }

        // 従業員マスタ検索
        private void TextBoxEmpNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ButtonSearchEmpNo_Click(sender, e);
        }
        private void ButtonSearchEmpNo_Click(object sender, EventArgs e)
        {
            if (textBoxEmpNo.Text == string.Empty) return;
            var dt = DBManager_MySQL.従業員番号検索(textBoxEmpNo.Text);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("従業員番号は見つかりませんでした．", "従業員マスタ検索",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                _empno = dt.Rows[0]["EMPNO"].ToString() ?? "";
                _empname = dt.Rows[0]["NAME"].ToString() ?? "";
                _culturecd = dt.Rows[0]["CULTURECD"].ToString() ?? "ja-JP";
                _isAdministrator = Common.管理者判定(_empno);
                Common.言語設定(_culturecd);
                labelEmpName.Text = 挨拶() + $" {_empname} さん";
                labelFelicaIDm2.Text = dt.Rows[0]["FELICAID"].ToString() ?? "";
                buttonExportExcel.Visible = _isAdministrator;
                自分の予約一覧に色を付ける();
#pragma warning disable CS8622
                labelEmpName.Click += ログアウト処理;
#pragma warning restore CS8622
                TimerStart();
            }
        }
        private static string 挨拶()
        {
            return DateTime.Now.Hour switch
            {
                < 11 => "おはよう",
                < 17 => "こんにちは",
                _ => "こんばんは"
            };
        }

        /*
         * データベース関連ここまで
         */













        /*
         * FeliCa 関連の処理ここから
         */

        // カードリーダー初期化
        private void InitializePcscMonitor()
        {
            try
            {
                if (Common.IsNfcPortDriverInstalled() == false)
                {
                    toolStripStatusLabel1.Text = "Smart Card Reader を接続してください．";
                    /*
                    MessageBox.Show("PaSoRi [RC-S380] が見つかりません．\n Smart Card Reader を接続してください．"
                        , Common.PROGRAM_TITLE
                        , MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    */
                    return;
                }

                var context = ContextFactory.Instance.Establish(SCardScope.System);
                var readers = context.GetReaders();

                if (readers == null || readers.Length == 0)
                {
                    toolStripStatusLabel1.Text = "カードリーダーが見つかりません";
                    return;
                }

                // PaSoRi が存在するか確認
                bool exists = false;
                foreach (var r in readers)
                {
                    if (r.Contains("PaSoRi"))
                    {
                        exists = true;
                        _readerName = r;
                        break;
                    }
                }

                if (!exists)
                {
                    toolStripStatusLabel1.Text = "PaSoRi が見つかりません";
                    return;
                }

                toolStripStatusLabel1.Text = $"使用リーダー：{_readerName}";

                _monitor = MonitorFactory.Instance.Create(SCardScope.System);

                _monitor.CardInserted += Monitor_CardInserted;
                _monitor.CardRemoved += Monitor_CardRemoved;
                _monitor.Initialized += Monitor_Initialized;
                _monitor.MonitorException += Monitor_MonitorException;

                _monitor.Start(_readerName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("カードリーダー初期化処理で異常が発生しました．\n"
                    + ex.Message
                    , Common.PROGRAM_TITLE
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // 「モニター」の初期化
        private void Monitor_Initialized(object sender, CardStatusEventArgs e)
        {
            Invoke(new Action(() =>
            {
                labelEmpName.Text = "社員証を読み取ってください";
            }));
        }

        // 「カード検知」イベント
        private void Monitor_CardInserted(object sender, CardStatusEventArgs e)
        {
            Invoke(new Action(() =>
            {
                toolStripStatusLabel1.Text = "カード検知";

                // FeliCa Polling → IDm取得
                var idm = GetFelicaIDm();
                if (idm != null)
                {
                    string felicaid = BitConverter.ToString(idm);
                    var dt = DBManager_MySQL.FeliCaID検索(felicaid);
                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("FeliCaIDは見つかりませんでした．", "従業員マスタ検索",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        _empno = dt.Rows[0]["EMPNO"].ToString() ?? "";
                        _empname = dt.Rows[0]["NAME"].ToString() ?? "";
                        _culturecd = dt.Rows[0]["CULTURECD"].ToString() ?? "ja-JP";
                        _isAdministrator = Common.管理者判定(_empno);
                        Common.言語設定(_culturecd);
                        labelEmpName.Text = 挨拶() + $" {_empname} さん";
                        labelFelicaIDm2.Text = dt.Rows[0]["FELICAID"].ToString() ?? "";
                        buttonExportExcel.Visible = _isAdministrator;
                        自分の予約一覧に色を付ける();
#pragma warning disable CS8622
                        labelEmpName.Click += ログアウト処理;
#pragma warning restore CS8622
                        TimerStart();
                    }
                }
                else
                {
                    labelFelicaIDm2.Text = "IDm取得失敗";
                }
            }));
        }

        // 「カード抜去」イベント
        private void Monitor_CardRemoved(object sender, CardStatusEventArgs e)
        {
            Invoke(new Action(() =>
            {
                /*
                 * 何もしない
                labelStatus.Text = "カード抜去";
                labelEmpName.Text = "";
                labelFelicaIDm2.Text = "";
                */
            }));
        }

        // モニター中異常検知
        private void Monitor_MonitorException(object sender, PCSCException ex)
        {
            Invoke(new Action(() =>
            {
                _monitor = null;
                toolStripStatusLabel1.ForeColor = Color.Yellow;
                toolStripStatusLabel1.BackColor = Color.LightCoral;
                toolStripStatusLabel1.Text = $"FeliCaモニター異常: {ex.Message}";
            }));
        }

        /// <summary>
        /// FeliCa の IDm を取得する（PaSoRi専用）
        /// </summary>
        private byte[]? GetFelicaIDm()
        {
            var context = ContextFactory.Instance.Establish(SCardScope.System);
            using var reader = new SCardReader(context);

            var rc = reader.Connect(_readerName, SCardShareMode.Shared, SCardProtocol.Any);
            if (rc != SCardError.Success)
            {
                return null;
            }

            // FeliCa Polling コマンド（IDm取得）
            var apdu = new byte[] { 0xFF, 0xCA, 0x00, 0x00, 0x00 };

            var receivePci = new SCardPCI();
            var sendPci = SCardPCI.GetPci(reader.ActiveProtocol);

            var receiveBuffer = new byte[256];
            var receiveLength = receiveBuffer.Length;

            rc = reader.Transmit(
                sendPci,
                apdu,
                apdu.Length,
                receivePci,
                receiveBuffer,
                ref receiveLength);

            if (rc != SCardError.Success)
            {
                return null;
            }

            // 末尾2バイトは SW1 SW2（90 00）
            var idm = new byte[receiveLength - 2];
            Array.Copy(receiveBuffer, 0, idm, 0, idm.Length);

            return idm;
        }

        /*
         * FeliCa 関連の処理ここまで
         */






        /*
         * 自動ログアウト処理関連ここから
         */

        public class IdleMessageFilter : IMessageFilter
        {
            public event EventHandler? UserActivity;

            public bool PreFilterMessage(ref Message m)
            {
                // キーボード・マウスの全ての操作を拾う
                if (m.Msg == 0x200 ||  // WM_MOUSEMOVE
                    m.Msg == 0x201 ||  // WM_LBUTTONDOWN
                    m.Msg == 0x100)    // WM_KEYDOWN
                {
                    UserActivity?.Invoke(this, EventArgs.Empty);
                }
                return false;
            }
        }

        private void TimerStart()
        {
            // 自動ログアウト機能
#pragma warning disable CS8622
            idleTimer.Interval = 30 * 1000; // 30秒を設定
            idleTimer.Tick += IdleTimer_Tick;
            idleTimer.Start();                  // 開始
            //Debug.Print(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " TimerStart ");
            Application.AddMessageFilter(_filter);
            _filter.UserActivity += ResetIdleTimer;
#pragma warning restore CS8622
        }
        private void ResetIdleTimer(object sender, EventArgs e)
        {
            idleTimer.Stop();
            idleTimer.Start();
            //Debug.Print(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " Reset");
        }
        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            ログアウト処理(sender, e);
        }

        private void ログアウト処理(object sender, EventArgs e)
        {
            idleTimer.Stop();
            //Debug.Print(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " Stop");
#pragma warning disable CS8622
            _filter.UserActivity -= ResetIdleTimer;
            labelEmpName.Click -= ログアウト処理;
#pragma warning restore CS8622
            _empno = string.Empty;
            _empname = string.Empty;
            Common.IsAdmin = false;
            _isAdministrator = false;
            textBoxEmpNo.Text = string.Empty;
            labelEmpName.Text = string.Empty;
            labelFelicaIDm2.Text = string.Empty;
            if (_monitor != null) labelEmpName.Text = "社員証を読み取ってください";
            buttonExportExcel.Visible = false;
            自分の予約一覧に色を付ける();
        }

        /*
         * 自動ログアウト処理関連ここまで
         */



    }

}



using PCSC;
using PCSC.Exceptions;
using PCSC.Monitoring;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace BodyWorkReservation
{
    public partial class FormBodyworkReservation : Form
    {
        private readonly List<DateTime> _thursdays = [];
        public string _empno = string.Empty;
        public string _empname = string.Empty;
        public bool _isAdministrator = false;

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
        }

        // フォームロードで各種初期設定を行う
        private void FormBodyWorkReservation_Load(object sender, EventArgs e)
        {
            labelEmpName.Text = "";
            labelFelicaIDm.Text = "";

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

            // リサイズイベントでセル幅とセル高さを等分に分ける
            FormBodyWorkReservation_Resize(sender, e);

            // カードリーダー初期化
            InitializePcscMonitor();
        }

        // データグリッドの選択状態をクリアするにはActivatedかShownしかない！
        private void DataGridViewSelectionClear(object sender, EventArgs e)
        {
            //labelStatus.Focus(); // 画面からフォーカスを外したい
            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }
        private void FormBodyWorkReservation_Resize(object sender, EventArgs e)
        {
            if (_monitor == null)
            {
                labelStatus.Width = this.Width - 60;
            }
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
        // 異常時はLabelStatusをクリックまたはタップでプログラム終了
        private void LabelStatus_Click(object sender, EventArgs e)
        {
            if (_monitor == null)
            {
                this.Close();
            }
        }






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

            // 今日を基準に「20日締めの期間」を計算
            DateTime today = DateTime.Today;

            // 締め日が過ぎているかどうかで期間を決める
            DateTime periodEnd =
                today.Day >= 20
                ? new DateTime(today.Year, today.Month, 20).AddMonths(1)
                : new DateTime(today.Year, today.Month, 20);

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
                    HeaderText = th.ToString("MM/dd (木)"),
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
            if (_empno == string.Empty || e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                DataGridViewSelectionClear(sender, e);
                return;
            }
            idleTimer.Stop();
            int row = e.RowIndex;
            DateTime reservdt = _thursdays[e.ColumnIndex];
            var cell = dataGridView1[e.ColumnIndex, e.RowIndex];
            var order = (Common.Order)cell.Value;
            if (order == null)
            {
                if (cell.Style.BackColor == Color.LightSlateGray) // ①②の重複予約不可
                {
                    DataGridViewSelectionClear(sender, e);
                    return;
                }
                order = new Common.Order
                {
                    ReservDt = reservdt,
                    TimeSlot = row,
                    EmpNo = _empno,
                    EmpName = _empname,
                };
                Form frm = new FormBodyworkPopup(true, order, this)
                {
                    StartPosition = FormStartPosition.CenterParent
                    // セルの横にポップアップさせる処理は廃止↓
                    // StartPosition = FormStartPosition.Manual,
                    // Location = PopupPoint(sender, e) 
                };
                var result = frm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    cell.Value = order;
                }
            }
            else
            {
                if (order.EmpNo != _empno && _isAdministrator == false)
                {
                    DataGridViewSelectionClear(sender, e);
                    return;
                }
                Form frm = new FormBodyworkPopup(false, order, this)
                {
                    StartPosition = FormStartPosition.CenterParent,
                    // セルの横にポップアップさせる処理は廃止↓
                    // StartPosition = FormStartPosition.Manual,
                    // Location = PopupPoint(sender, e)
                };
                var result = frm.ShowDialog();
                if (result == DialogResult.No) // 予約取消
                {
                    cell.Value = null;
                }
            }
            自分の予約一覧に色を付ける();
            idleTimer.Start();
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
                        if (rowIdx == 1) dataGridView1[colIdx, 0].Style.BackColor = Color.LightSlateGray;
                    }
                    else
                    {
                        // ②に予約が入っていなくても①に予約が入っている時はグレーアウトする
                        if (rowIdx == 1 && dataGridView1[colIdx, 0].Value is Common.Order)
                        {
                            dataGridView1[colIdx, 1].Style.BackColor = Color.LightSlateGray;
                        }
                        else
                        {
                            dataGridView1[colIdx, rowIdx].Style.BackColor =
                                dataGridView1.DefaultCellStyle.BackColor;
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
                _isAdministrator = 管理者判定(_empno);
                labelEmpName.Text = 挨拶() + $" {_empname} さん";
                labelFelicaIDm.Text = dt.Rows[0]["FELICAID"].ToString() ?? "";
                自分の予約一覧に色を付ける();
#pragma warning disable CS8622
                labelEmpName.Click += ログアウト処理;
#pragma warning restore CS8622
                TimerStart();
            }
        }
        // 管理者判定【森下政宏モリシタマサヒロ、藤田彩華フジタアヤカ】
        private static bool 管理者判定(string empno)
        {
            return (empno == "10794" || empno == "21292");
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
                    labelStatus.Text = "Smart Card Reader を接続してください．";
                    MessageBox.Show("PaSoRi [RC-S380] が見つかりません．\n Smart Card Reader を接続してください．"
                        , Common.PROGRAM_TITLE
                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var context = ContextFactory.Instance.Establish(SCardScope.System);
                var readers = context.GetReaders();

                if (readers == null || readers.Length == 0)
                {
                    labelStatus.Text = "カードリーダーが見つかりません";
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
                    labelStatus.Text = "PaSoRi が見つかりません";
                    return;
                }

                labelStatus.Text = $"使用リーダー：{_readerName}";

                _monitor = MonitorFactory.Instance.Create(SCardScope.System);

                _monitor.CardInserted += Monitor_CardInserted;
                _monitor.CardRemoved += Monitor_CardRemoved;
                _monitor.Initialized += Monitor_Initialized;
                _monitor.MonitorException += Monitor_MonitorException;

                _monitor.Start(_readerName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ドライバが見つかりません．\nインストールしてから実行してください"
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
                labelStatus.Text = "社員証を読み取ってください";
            }));
        }

        // 「カード検知」イベント
        private void Monitor_CardInserted(object sender, CardStatusEventArgs e)
        {
            Invoke(new Action(() =>
            {
                labelStatus.Text = "カード検知";

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
                        _isAdministrator = 管理者判定(_empno);
                        labelEmpName.Text = 挨拶() + $" {_empname} さん";
                        labelFelicaIDm.Text = dt.Rows[0]["FELICAID"].ToString() ?? "";
                        自分の予約一覧に色を付ける();
#pragma warning disable CS8622
                        labelEmpName.Click += ログアウト処理;
#pragma warning restore CS8622
                        TimerStart();
                    }
                }
                else
                {
                    labelFelicaIDm.Text = "IDm取得失敗";
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
                labelFelicaIDm.Text = "";
                */
            }));
        }

        // モニター中異常検知
        private void Monitor_MonitorException(object sender, PCSCException ex)
        {
            Invoke(new Action(() =>
            {
                _monitor = null;
                labelStatus.Left = 20;
                labelStatus.Width = this.Width - 60;
                labelStatus.TextAlign = ContentAlignment.MiddleCenter;
                labelStatus.ForeColor = Color.Yellow;
                labelStatus.BackColor = Color.LightCoral;
                labelStatus.Font = new Font(labelStatus.Font, FontStyle.Bold);
                labelStatus.Text = $"モニター例外: {ex.Message}";
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
            idleTimer.Interval = 3 * 60 * 1000; // 3分を設定
            idleTimer.Tick += IdleTimer_Tick;
            idleTimer.Start();                  // 開始
            //this.KeyDown += ResetIdleTimer;     // ユーザー操作を監視
            //this.MouseMove += ResetIdleTimer;   // ユーザー操作を監視
            Application.AddMessageFilter(_filter);
            _filter.UserActivity += ResetIdleTimer;
#pragma warning restore CS8622
        }
        private void ResetIdleTimer(object sender, EventArgs e)
        {
            idleTimer.Stop();
            idleTimer.Start();
        }
        private void IdleTimer_Tick(object sender, EventArgs e)
        {
            idleTimer.Stop();
            ログアウト処理(sender, e);
#pragma warning disable CS8622
            _filter.UserActivity -= ResetIdleTimer;
#pragma warning restore CS8622
        }

        private void ログアウト処理(object sender, EventArgs e)
        {
            _empno = string.Empty;
            _empname = string.Empty;
            _isAdministrator = false;
            textBoxEmpNo.Text = string.Empty;
            labelEmpName.Text = string.Empty;
            labelFelicaIDm.Text = string.Empty;
            if (_monitor != null) labelStatus.Text = "社員証を読み取ってください";
            自分の予約一覧に色を付ける();
#pragma warning disable CS8622
            labelEmpName.Click -= ログアウト処理;
#pragma warning restore CS8622
        }

        /*
         * 自動ログアウト処理関連ここまで
         */



    }

}



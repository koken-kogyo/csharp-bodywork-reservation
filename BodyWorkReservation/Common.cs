using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System.Diagnostics;                   // Process.Start
using System.Management;
using System.Runtime.InteropServices;       // Marshal.ReleaseComObject
using Excel = Microsoft.Office.Interop.Excel;

namespace BodyWorkReservation
{
    public class Common
    {
        /*
         * Constants
         */
        // プログラムタイトル
        public static readonly string PROGRAM_TITLE = "[KGA003SF] からだや予約システム";
        public static readonly string PROGRAM_NAME = "BodyWorkReservation";
        public static readonly string PROGRAM_VERSION = "260906.01";

        // 管理者【森下政宏：モリシタマサヒロ、藤田彩華：フジタアヤカ】
        public static readonly string[] ADMIN_CODES = [
            "10794",
            "21292"
        ];
        public static bool IsAdmin { get; set; }  // 管理者モード

        // 時間帯
        public static readonly string[] TIMESLOT_NAME = [
            "① 16:30 ～ 17:00",
            "② 16:50 ～ 17:20",
            "③ 17:30 ～ 18:00",
            "④ 18:10 ～ 18:40",
            "⑤ 18:50 ～ 19:20",
            "⑥ 19:30 ～ 20:00"
        ];

        // 施術内容
        public static readonly string[] TREATMENT_NAME = [
            "腰痛",
            "肩凝り",
            "頭痛",
            "股関節痛",
            "膝痛",
            "自律神経の不調",
            "その他"
        ];

        // メッセージ定義
        public static readonly int MSG_PAD = 6;
        public static readonly string MSG_SEPARATOR =
"-------------------------------------------------------------------------------";

        // エラーメッセージ定義
        public static readonly string ERR_NOT_NUMERIC = "数値を入力してください．";

        public static readonly string MSG_DATABESE_CONFIG_NOT_EXSIST = "データベース設定ファイルが存在しません\n設定ファイルを配置しアプリを再起動してください";
        public static readonly string MSG_FILE_CONFIG_NOT_EXSIST = "ファイル設定ファイルが存在しません\n設定ファイルを配置しアプリを再起動してください";
        public static readonly string MSG_DATABESE_CONNECTION_FAILURE = "データベースへの接続に失敗しました";
        public static readonly string MSG_DATABESE_CLOSE_FAILURE = "データベースへの切断に失敗しました";
        public static readonly string MSG_KM8420_REFRESH_FAILURE = "データベースの更新に失敗しました";

        public static readonly string MSG_PROGRAM_ERROR = "プログラムの想定エラーが発生しました";



        /*
         * 設定ファイル関連
         */
        public const string APP_SETTING_FILE = "appsettings.json";
        public class EmConfig
        {
            public string HOST { get; set; } = "";
            public string USER { get; set; } = "";
            public string PASS { get; set; } = "";
            public string SCHEMA { get; set; } = "";
        }
        public class MpConfig
        {
            public string SERVER { get; set; } = "";
            public string PORT { get; set; } = "";
            public string USER { get; set; } = "";
            public string PASS { get; set; } = "";
            public string SCHEMA { get; set; } = "";
        }
        public class AppConfig
        {
            public EmConfig EmConfig { get; set; } = new();
            public MpConfig MpConfig { get; set; } = new();
        }
        public static AppConfig LoadConfig()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, APP_SETTING_FILE);

            if (!File.Exists(filePath))
            {
                MessageBox.Show(
                    $"設定ファイル[{APP_SETTING_FILE}]が見つかりません．\n"
                    + "アプリケーションを終了します．"
                    , Common.PROGRAM_TITLE
                    , MessageBoxButtons.OK
                    , MessageBoxIcon.Stop
                );
                Environment.Exit(9);
            }

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(APP_SETTING_FILE, optional: false, reloadOnChange: true)
                .Build();

            var appConfig = new AppConfig();
            config.GetSection("EmConfig").Bind(appConfig.EmConfig);
            config.GetSection("MpConfig").Bind(appConfig.MpConfig);

            return appConfig;
        }



        /*
         * クラス関連
         */
        /// <summary>
        /// からだや予約データクラス
        /// KD7000 から取得したデータを格納する
        /// </summary>
        public class Order
        {
            public DateTime ReservDt { get; set; }  // 予約日
            public int TimeSlot { get; set; }       // 時間帯
            public string EmpNo { get; set; }       // 従業員番号
            public string EmpName { get; set; }     // 従業員名
            public string Treatment { get; set; }   // 施術内容
            public string Note { get; set; }        // その他のお悩み

            public Order()
            {
                ReservDt = DateTime.MinValue;
                TimeSlot = 0;
                EmpNo = "";
                EmpName = "";
                Treatment = "";
                Note = "";
            }
            public Order(Order order)
            {
                ReservDt = (order != null) ? order.ReservDt : DateTime.MinValue;
                TimeSlot = (order != null) ? order.TimeSlot : 0;
                EmpNo = (order != null) ? order.EmpNo : "";
                EmpName = (order != null) ? order.EmpName : "";
                Treatment = (order != null) ? order.Treatment : "";
                Note = (order != null) ? order.Note : "";
            }
            public Order(DateTime reservdt, int timeslot, string empno, string empname, string treatment, string note)
            {
                ReservDt = reservdt;
                TimeSlot = timeslot;
                EmpNo = empno;
                EmpName = empname;
                Treatment = treatment;
                Note = note;
            }
            // データグリッド上の表示はここで決まる【施術内容】
            public override string ToString()
            {                
                return (IsAdmin) ? EmpName : "済";
            }
        }



        /*
         * メソッド関連
         */

        // 管理者判定
        public static bool 管理者判定(string loginid)
        {
            IsAdmin = ADMIN_CODES.Contains(loginid);
            return IsAdmin;
        }

        // デバイス一覧に RC-S380 または PaSoRi が存在するかを確認する
        public static bool IsNfcPortDriverInstalled()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity");
                foreach (ManagementObject obj in searcher.Get().Cast<ManagementObject>())
                {
                    string name = obj["Name"]?.ToString() ?? "";

                    if (name.Contains("RC-S380", StringComparison.OrdinalIgnoreCase)
                    || name.Contains("PaSoRi", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        // Excel がインストールされているかレジストリを確認する
        public static bool IsExcelInstalled()
        {
            using var key = Registry.ClassesRoot.OpenSubKey("Excel.Application");
            return key != null;
        }

        // 「集計データ出力」
        public static void ExportExcel(DataGridView dgv, string savefullpath)
        {
            // ① DataGridView から Order を抽出
            var orders = new List<Order>();

            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value is Order o)
                    {
                        orders.Add(o);
                    }
                }
            }

            // ② 従業員番号でグループ化（EmpNo, EmpName, Count）
            var summary = orders
                .GroupBy(o => new { o.EmpNo, o.EmpName })
                .Select(g => new
                {
                    g.Key.EmpNo,
                    g.Key.EmpName,
                    Count = g.Count()
                })
                .OrderBy(x => x.EmpNo)
                .ToList();

            // ③ Excel Interop で出力
            Excel.Application? excelApp = null; // Excel オブジェクト
            Excel.Workbook? book = null;        // Workbook オブジェクト
            Excel.Worksheet? sheet = null;      // Worksheet オブジェクト
            try
            {
                excelApp = new()
                {
                    Visible = false
                };
                book = excelApp.Workbooks.Add();
                sheet = (Excel.Worksheet)book.ActiveSheet;

                // ヘッダ
                sheet.Cells[1, 1].Value = "社員番号";
                sheet.Cells[1, 2].Value = "氏名";
                sheet.Cells[1, 3].Value = "回数";

                int rowIndex = 2;

                foreach (var s in summary)
                {
                    sheet.Cells[rowIndex, 1].Value = s.EmpNo;
                    sheet.Cells[rowIndex, 2].Value = s.EmpName;
                    sheet.Cells[rowIndex, 3].Value = s.Count;
                    rowIndex++;
                }

                // 別名で保存（Desktopに作成）
                book.SaveAs(savefullpath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー: " + ex.Message, Common.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (book != null)
                {
                    book.Close(false);
                    Marshal.ReleaseComObject(book);
                    book = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                    excelApp = null;
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

                // 関連付けられたアプリでExcelを開く
                if (File.Exists(@savefullpath))
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = @savefullpath,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
            }


        }








    }


}

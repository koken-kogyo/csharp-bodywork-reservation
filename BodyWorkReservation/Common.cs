using DecryptPassword;
using Microsoft.Extensions.Configuration;   // JSON
using Microsoft.Win32;
using System.Diagnostics;                   // Process.Start
using System.Management;
using System.Runtime.InteropServices;       // Marshal.ReleaseComObject
using Excel = Microsoft.Office.Interop.Excel;
// Microsoft Graph API
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

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
        // サポート言語
        public static readonly string[] SUPPORTED_CULTURES = [
            "Japanese (ja-JP)",
            "English (en-US)",
            "Português (pt-BR)"
        ];

        // ログインユーザーに関する情報を保持するプロパティ
        public static string LoginID { get; set; } = string.Empty;  // ログインID
        public static bool IsAdmin { get; set; }                    // 管理者モード
        public static bool IsFullTimeEmployee { get; set; }         // 正社員
        public static string CultureCD { get; set; } = "ja-JP";     // 言語設定
        public static int CultureID { get; set; } = 0;              // サポート言語Index

        // タイムスロット時間帯
        public static readonly string[] TIMESLOT_NAME = [
            "① 16:20 ～ 16:50",
            "② 17:20 ～ 17:50",
            "③ 18:00 ～ 18:30",
            "④ 18:40 ～ 19:10",
            "⑤ 19:20 ～ 19:50"
        ];
        public static readonly string[] TIMESLOT202609 = [
            "① 16:30 ～ 17:00",
            "② 16:50 ～ 17:20",
            "③ 17:30 ～ 18:00",
            "④ 18:10 ～ 18:40",
            "⑤ 18:50 ～ 19:20",
            "⑥ 19:30 ～ 20:00"
        ];
        public static readonly string[] TIMESLOT202711 = [
            "① 16:20 ～ 16:50",
            "② 17:20 ～ 17:50",
            "③ 18:00 ～ 18:30",
            "④ 18:40 ～ 19:10",
            "⑤ 19:20 ～ 19:50"
        ];
        public static readonly string[] TIMESLOT209912 = [
            "① 17:00 ～ 17:30",
            "② 17:20 ～ 17:50",
            "③ 18:00 ～ 18:30",
            "④ 18:40 ～ 19:10",
            "⑤ 19:20 ～ 19:50",
            "⑥ 20:00 ～ 20:30"
        ];

        // （ 廃止 → Resourcesに移行 ）施術内容
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
        public static AppConfig LoadAppConfig()
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
        // メール送信設定ファイル (static)
        public const string APP_EMAILSETTING_FILE = "appsettings_sendmail.json";
        public static bool IsEmailConfigLoaded { get; set; } = false;
        public static class EmailConfig
        {
            public static string Subject { get; set; } = string.Empty;
            public static string FromEmailAddress { get; set; } = string.Empty;
            public static string[] ToRecipients { get; set; } = [];
            public static string[] CcRecipients { get; set; } = [];
            public static string ClientEnc { get; set; } = string.Empty;
            public static string ClientSecretEnc { get; set; } = string.Empty;
            public static string TenantEnc { get; set; } = string.Empty;
        }
        public static void LoadEmailConfig()
        {
            string filePath = Path.Combine(AppContext.BaseDirectory, APP_EMAILSETTING_FILE);

            if (!File.Exists(filePath))
            {
                IsEmailConfigLoaded = false;
                return;
            }

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(APP_EMAILSETTING_FILE, optional: false, reloadOnChange: true)
                .Build();

            EmailConfig.Subject = config.GetSection("Subject").Value ?? "";
            EmailConfig.FromEmailAddress = config.GetSection("FromEmailAddress").Value ?? "";
            EmailConfig.ToRecipients =
                config.GetSection("ToRecipients").Get<string[]>() ?? [];
            EmailConfig.CcRecipients =
                config.GetSection("CcRecipients").Get<string[]>() ?? [];
            EmailConfig.ClientEnc = config.GetSection("ClientEnc").Value ?? "";
            EmailConfig.ClientSecretEnc = config.GetSection("ClientSecretEnc").Value ?? "";
            EmailConfig.TenantEnc = config.GetSection("TenantEnc").Value ?? "";

            IsEmailConfigLoaded = true;
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
            public DateTime StDt { get; set; }      // 開始時刻
            public DateTime EdDt { get; set; }      // 終了時刻
            public string EmpNo { get; set; }       // 従業員番号
            public string EmpName { get; set; }     // 従業員名
            public string Treatment { get; set; }   // 施術内容
            public string Note { get; set; }        // その他のお悩み

            public Order()
            {
                ReservDt = DateTime.MinValue;
                TimeSlot = 0;
                StDt = DateTime.MinValue;
                EdDt = DateTime.MinValue;
                EmpNo = "";
                EmpName = "";
                Treatment = "";
                Note = "";
            }
            public Order(Order order)
            {
                ReservDt = (order != null) ? order.ReservDt : DateTime.MinValue;
                TimeSlot = (order != null) ? order.TimeSlot : 0;
                StDt = (order != null) ? order.StDt : DateTime.MinValue;
                EdDt = (order != null) ? order.EdDt : DateTime.MinValue;
                EmpNo = (order != null) ? order.EmpNo : "";
                EmpName = (order != null) ? order.EmpName : "";
                Treatment = (order != null) ? order.Treatment : "";
                Note = (order != null) ? order.Note : "";
            }
            public Order(DateTime reservdt
                , int timeslot
                , DateTime stdt
                , DateTime eddt
                , string empno
                , string empname
                , string treatment
                , string note)
            {
                ReservDt = reservdt;
                TimeSlot = timeslot;
                StDt = stdt;
                EdDt = eddt;
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

        // 従業員コード判定
        public static bool 管理者判定(string loginid)
        {
            LoginID = loginid;
            IsAdmin = ADMIN_CODES.Contains(loginid);
            // 正社員判定
            if (loginid.StartsWith('1') || loginid.StartsWith('2') ||
                loginid.StartsWith("01") || loginid.StartsWith("02"))
            {
                IsFullTimeEmployee = true;
            }
            else
            {
                IsFullTimeEmployee = false;
            }
            return IsAdmin;
        }

        // 言語設定
        public static void 言語設定(string culturecd)
        {
            // 初期値を設定
            CultureCD = (culturecd == string.Empty)
                ? (IsFullTimeEmployee == true) 
                ? "ja-JP" 
                : "en-US"
                : culturecd;
            CultureID = 0;
            for (int i = 0; i < SUPPORTED_CULTURES.Length; i++)
            {
                // コンボボックス選択からの設定
                if (SUPPORTED_CULTURES[i].ToString() == CultureCD)
                {
                    CultureCD = CultureCD.Split("(")[1].Split(")")[0];
                    CultureID = i;
                    break;
                }
                // 従業員マスタ言語コードからの設定
                else if (SUPPORTED_CULTURES[i].ToString().Contains(CultureCD))
                {
                    CultureID = i;
                    break;
                }
            }
        }

        // タイムスロットの取得（予約日によって変化する）
        public static string[] GetTimeSlotByDate(DateTime d)
        {
            // 2026/09 以降のタイムスロット時間帯
            if (d >= new DateTime(2026, 9, 1) && d < new DateTime(2027, 11, 1))
            {
                return TIMESLOT202609;
            }

            // 2027/11 以降のタイムスロット時間帯（テストで使用。202711までには消す事）
            if (d >= new DateTime(2027, 11, 1) && d < new DateTime(2099, 12, 1))
            {
                return TIMESLOT202711;
            }

            // 2099/12 以降のタイムスロット時間帯
            if (d >= new DateTime(2099, 12, 1))
            {
                return TIMESLOT209912;
            }

            // それ以外は基本タイムスロット時間帯
            return TIMESLOT_NAME;
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
                    System.Diagnostics.Process.Start(psi);
                }
            }
        }


        // Microsoft Graph API v6 メール送信
        public static async Task SendScreenshotMailAsync(Byte[] pngBytes, string messageBody)
        {
            try
            {
                // ① 認証（Client Credentials Flow）
                var dpc = new DecryptPasswordClass();
                dpc.DecryptPassword(EmailConfig.ClientEnc, out string clientId);
                string clientSecret = EmailConfig.ClientSecretEnc;
                dpc.DecryptPassword(EmailConfig.TenantEnc, out string tenantId);
                var app = ConfidentialClientApplicationBuilder.Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithTenantId(tenantId)
                    .Build();

                var scopes = new[] { "https://graph.microsoft.com/.default" };
                var token = await app.AcquireTokenForClient(scopes).ExecuteAsync();

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token.AccessToken);

                var graphClient = new GraphServiceClient(httpClient);

                // ② To 送信先メールアドレスのリストを作成 
                var toRecipientsList = new List<Recipient>();
                foreach (var to in EmailConfig.ToRecipients)
                {
                    toRecipientsList.Add(new Recipient { EmailAddress = new EmailAddress { Address = to } });
                }

                // ③ CC 送信先メールアドレスのリストを作成
                var ccRecipientsList = new List<Recipient>();
                foreach (var cc in EmailConfig.CcRecipients)
                {
                    ccRecipientsList.Add(new Recipient { EmailAddress = new EmailAddress { Address = cc } });
                }

                // ④ メール作成（Graph SDK v6の書き方）
                var mailBody = new SendMailPostRequestBody
                {
                    Message = new Microsoft.Graph.Models.Message
                    {
                        Subject = EmailConfig.Subject, //"[自動送信] からだや予約送信",
                        Body = new ItemBody
                        {
                            ContentType = BodyType.Text,
                            Content = messageBody
                        },

                        // 送信先メールアドレス（To）
                        ToRecipients = toRecipientsList,

                        // CC
                        CcRecipients = ccRecipientsList,

                        // 添付ファイル（今回はスクショ）
                        Attachments =
                        [
                            new FileAttachment
                            {
                                OdataType = "#microsoft.graph.fileAttachment",
                                Name = "screenshot.png",
                                ContentBytes = pngBytes
                            }
                        ]
                    },
                    SaveToSentItems = true
                };

                // ⑤ 送信（Graph SDK v6の書き方）
                await graphClient.Users[EmailConfig.FromEmailAddress] // 送信元メールアドレス
                    .SendMail
                    .PostAsync(mailBody);
            }
            catch (Exception ex)
            {
                MessageBox.Show("メール送信エラー: " + ex.Message, Common.PROGRAM_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /*
         * メソッド関連ここまで
         */






    }


}

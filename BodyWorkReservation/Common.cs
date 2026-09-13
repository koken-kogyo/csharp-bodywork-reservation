using System;
using System.Management;
using Microsoft.Extensions.Configuration;

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
                return Treatment;
            }
        }


        public static bool IsNfcPortDriverInstalled()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        string name = obj["Name"]?.ToString() ?? "";

                        if (name.IndexOf("RC-S380", StringComparison.OrdinalIgnoreCase) >= 0
                        || name.IndexOf("PaSoRi", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }




    }





    public static class ParseExtensions
    {
        public static double ToDoubleSafe(this object value, double defaultValue = 0)
        {
            if (value == null || value == DBNull.Value) return defaultValue;
            if (double.TryParse(value.ToString(), out double result))
                return result;
            return defaultValue;
        }
        public static int? ToIntNullable(this object value)
        {
            if (value == null || value == DBNull.Value)
                return null;

            if (int.TryParse(value.ToString(), out int result))
                return result;

            return null;
        }
    }

    public static class ConsoleExtensions
    {
        /// <summary>
        /// 文字列を PAD_SIZE で PadLeft して Console.WriteLine する。
        /// </summary>
        public static void ConsoleWriteLinePadded(this string value)
        {
            Console.WriteLine(" ".PadLeft(Common.MSG_PAD) + value);
        }
    }

    public static class CompareExtensions
    {
        private const double EPS = 0.0000001;
        public static bool NearlyEquals(this double a, double b)
        {
            return Math.Abs(a - b) < EPS;
        }
        public static bool IntEquals(this int? a, int? b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            return a.Value == b.Value;
        }
    }


    internal static class AssemblyState
    {
        public const bool IsDebug =
#if DEBUG
        true;
#else
        false;
#endif
    }

}

using DecryptPassword;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using Mysqlx.Expect;
using System.Data;
using System.Text;

namespace BodyWorkReservation
{
    public static class DBManager_MySQL
    {
        private static MySqlConnection? mpCnn = null;
        private static string mpSchema = string.Empty;

        /// <summary>
        /// Oracle データベース スキーマ接続成否
        /// </summary>
        /// <param name="mpCnn">MySQL データベースへの接続クラス</param>
        /// <returns>結果 (false: 失敗, true: 成功)</returns>
        public static bool Connect()
        {
            bool ret = true;

            var appConfig = Common.LoadConfig();
            var mpConfig = appConfig.MpConfig;
            mpSchema = mpConfig.SCHEMA;

            // appsettings.json からデータベース情報を読み込む
            string server = mpConfig.SERVER;
            string database = mpConfig.SCHEMA;
            int port = Convert.ToInt32(mpConfig.PORT);
            string uid = mpConfig.USER;
            string charset = "utf8mb4";

            // パスワード復号化
            var dpc = new DecryptPasswordClass();
            string encpassed = mpConfig.PASS;
            dpc.DecryptPassword(encpassed, out string decPasswd);
            string pwd = decPasswd;

            // MySQL 接続文字列
            string connectionString = string.Format(
                "Server={0};Database={1};Port={2};Uid={3};Pwd={4};Charset={5}"
                , server, database, port, uid, pwd, charset);

            try
            {
                // MySQL へのコネクションの確立
                mpCnn = new MySqlConnection(connectionString);
                mpCnn.Open();
            }
            catch
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// MySQL データベース スキーマからの切断
        /// </summary>
        /// <param name="mpCnn">MySQL データベースへの接続クラス</param>
        public static void Close()
        {
            mpCnn?.Close();
            if (mpCnn != null ) mpCnn = null;
        }

        /// <summary>
        /// からだや予約 新規登録 or 更新
        /// </summary>
        /// <returns>挿入 or 更新件数</returns>
        public static int 予約登録更新(Common.Order o)
        {
            int ret = -1;
            string sql = string.Empty;
            try
            {
                Connect();
                var note = (o.Note == string.Empty) ? "null" : "'" + o.Note + "'";
                sql = "insert into "
                    + mpSchema + ".kd7000 "
                    + "(RESERVDT,TIMESLOT,EMPNO,TREATMENT,NOTE,INSTDT) "
                    + "values ("
                    + $"'{o.ReservDt}',{o.TimeSlot},'{o.EmpNo}','{o.Treatment}',{note}, now()) "
                    + "on duplicate key update "
                    + "TREATMENT = values(TREATMENT), "
                    + "NOTE = values(NOTE)";
                using (MySqlCommand myCmd = new(sql, mpCnn))
                {
                    ret = myCmd.ExecuteNonQuery();
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("予約登録に失敗しました．\n" + sql + "\n" + ex.Message
                    , Common.PROGRAM_TITLE
                    , MessageBoxButtons.OK, MessageBoxIcon.Error );
            }
            return ret;
        }

        /// <summary>
        /// からだや予約取消
        /// </summary>
        /// <returns>削除件数</returns>
        public static int 予約取消(Common.Order o)
        {
            int ret = -1;
            string sql = string.Empty;
            try
            {
                Connect();
                sql = "delete from "
                    + mpSchema + ".kd7000 "
                    + "where "
                    + $"RESERVDT='{o.ReservDt}' and "
                    + $"TIMESLOT={o.TimeSlot}"
                ;
                using (MySqlCommand myCmd = new(sql, mpCnn))
                {
                    ret = myCmd.ExecuteNonQuery();
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("予約取消に失敗しました．\n" + sql + "\n" + ex.Message
                    , Common.PROGRAM_TITLE
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return ret;
        }


        // 従業員番号を検索しデータテーブルを返却
        public static DataTable 従業員番号検索(string empno)
        {
            var dt = new DataTable();
            string sql = string.Empty;
            try
            {
                Connect();
                sql = $"select * from {mpSchema}.km0010 where EMPNO='{empno}'";
                using (MySqlCommand myCmd = new(sql, mpCnn))
                {
                    using MySqlDataAdapter myDa = new(myCmd);
                    myDa.Fill(dt);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("従業員番号検索に失敗しました．\n" + sql + "\n" + ex.Message
                    , Common.PROGRAM_TITLE
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        // FeliCaIDを検索しデータテーブルを返却
        public static DataTable FeliCaID検索(string felicaid)
        {
            var dt = new DataTable();
            string sql = string.Empty;
            try
            {
                Connect();
                sql = $"select * from {mpSchema}.km0010 where FELICAID='{felicaid}'";
                using (MySqlCommand myCmd = new(sql, mpCnn))
                {
                    using MySqlDataAdapter myDa = new(myCmd);
                    myDa.Fill(dt);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("従業員番号検索に失敗しました．\n" + sql + "\n" + ex.Message
                    , Common.PROGRAM_TITLE
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }

        /// <summary>
        /// 予約データ取得
        /// </summary>
        public static DataTable 予約データ取得(List<DateTime> _thursdays)
        {
            var dt = new DataTable();
            string sql = string.Empty;
            try
            {
                Connect();
                int count = _thursdays.Count;
                var reservDtF = _thursdays[0].ToString("yyyy-MM-dd");
                var reservDtT = _thursdays[count - 1].ToString("yyyy-MM-dd");
                sql = "select a.*, b.NAME from "
                    + mpSchema + ".kd7000 a "
                    + "inner join km0010 b on b.EMPNO=a.EMPNO "
                    + "where "
                    + $"RESERVDT between '{reservDtF}' and '{reservDtT}'"
                ;
                using (MySqlCommand myCmd = new(sql, mpCnn))
                {
                    using MySqlDataAdapter myDa = new(myCmd);
                    myDa.Fill(dt);
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("予約データ取得に失敗しました．\n" + sql + "\n" + ex.Message
                    , Common.PROGRAM_TITLE
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }



    }
}

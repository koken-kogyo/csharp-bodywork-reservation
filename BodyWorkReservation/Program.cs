using BodyWorkReservation;
using System.Globalization;

namespace BodyWorkReservation
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // コンフィグファイルチェック（事前準備）
            Common.LoadAppConfig();
            Common.LoadEmailConfig();

            //Application.Run(new SampleBodyWorkReservationPCSC());
            Application.Run(new FormBodyworkReservation());

        }
    }
}
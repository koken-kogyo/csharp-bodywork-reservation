using BodyWorkReservation.Properties;
using System.Globalization;

namespace BodyWorkReservation
{
    public partial class FormAnnouncement : Form
    {
        public FormAnnouncement()
        {
            InitializeComponent();

            comboBoxCulture.DataSource = Common.SUPPORTED_CULTURES; // 言語設定の初期化
            comboBoxCulture.SelectedIndex = Common.CultureID;
        }

        // 多言語対応のテキスト設定
        private void SetCultureText()
        {
            // 他言語対応
            this.Text = Resources.TitleAnnouncement;
            labelAnnounce1.Text = Resources.Announce1;
            labelAnnounce2.Text = (Common.IsFullTimeEmployee)
                ? Resources.Announce2FullTimeEmployee
                : Resources.Announce2OtherEmployee;

            labelAnnounce3.Text = (Common.IsFullTimeEmployee)
                ? Resources.Announce3FullTimeEmployee
                : ""; // Resources.Announce3OtherEmployee;
            buttonClose.Text = Resources.BtnClose;
            buttonUnderstand.Text = Resources.BtnUnderstand;
        }

        // 「了解しました」ボタン
        private void ButtonUnderstand_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        // 「閉じる」ボタン
        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ショートカット
        private void FormAnnouncement_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        // 「言語」変更
        private void comboBoxCulture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCulture.SelectedIndex < 0) return;
            string culture = comboBoxCulture.Text.Split("(")[1].Split(")")[0];
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            SetCultureText();
        }

    }
}

using BodyWorkReservation.Properties;
using System.Globalization;

namespace BodyWorkReservation
{
    public partial class FormBodyworkPopup : Form
    {
        private readonly bool _isNewOrder = false;
        private readonly Common.Order _order;

        public FormBodyworkPopup(bool isNewOrder, Common.Order order)
        {
            InitializeComponent();

            this._isNewOrder = isNewOrder;
            this._order = order;

            // 言語設定
            comboBoxCulture.DataSource = Common.SUPPORTED_CULTURES; // 言語設定の初期化
            comboBoxCulture.SelectedIndex = Common.CultureID;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Common.CultureCD);
#pragma warning disable CS8622
            comboBoxCulture.SelectedIndexChanged += ComboBoxCulture_SelectedIndexChanged; // 言語設定時のイベント抑制
#pragma warning restore CS8622
            buttonSaveCulture.Visible = false;

            // 多言語対応のテキスト設定（コンボボックスの初期化も含む）
            SetCultureText();

            // テキスト関連の初期設定
            labelEmployee.Text = order.EmpName;
            labelReservDt.Text = order.ReservDt.ToString("MM/dd");
            labelTimeSlot.Text = Common.TIMESLOT_NAME[order.TimeSlot];
            textBoxNote.Text = order.Note;

            // 過去オーダーは参照に設定
            if (order.ReservDt < DateTime.Today)
            {
                buttonAppoint.Enabled = false;
                buttonCancelAppoint.Enabled = false;
                comboBoxTreatment.Enabled = false;
                textBoxNote.Enabled = false;
            }

            // ボタン関連の初期設定
            if (isNewOrder)
            {
                buttonCancelAppoint.Visible = false;
                buttonClose.Left = buttonCancelAppoint.Left;
            }
            else
            {
                comboBoxTreatment.Text = order.Treatment;
            }
        }

        // 多言語対応のテキスト設定
        private void SetCultureText()
        {
            // 他言語対応
            this.Text = Resources.TitlePopup;
            labelTreatment.Text = Resources.LabelTreatment;
            labelOtherSymptoms.Text = Resources.LabelOtherSymptoms;

            // コンボボックスの初期設定
            //comboBoxTreatment.DataSource = Common.TREATMENT_NAME; // 多言語対応の為廃止
            comboBoxTreatment.Items.Clear();
            comboBoxTreatment.Items.Add(Resources.Pain_1LowBack);
            comboBoxTreatment.Items.Add(Resources.Pain_2StiffShoulders);
            comboBoxTreatment.Items.Add(Resources.Pain_3Headache);
            comboBoxTreatment.Items.Add(Resources.Pain_4Hip);
            comboBoxTreatment.Items.Add(Resources.Pain_5Knee);
            comboBoxTreatment.Items.Add(Resources.Pain_6Autonomic);
            comboBoxTreatment.Items.Add(Resources.Pain_9Other);

            // ボタン関連の初期設定
            buttonSaveCulture.Text = Resources.BtnSaveCalture;          // "言語設定保存";
            if (_isNewOrder)
            {
                buttonAppoint.Text = Resources.BtnReservation;          // "予約";
            }
            else
            {
                buttonAppoint.Text = Resources.BtnChangeReservation;    // "予約変更";
            }
            buttonCancelAppoint.Text = Resources.BtnCancelReservation;  // "予約取消";
            buttonClose.Text = Resources.BtnClose;                      // "閉じる";
        }

        // 「予約」「予約変更」新規登録または更新
        private void ButtonAppoint_Click(object sender, EventArgs e)
        {
            _order.Treatment = comboBoxTreatment.Text;
            _order.Note = textBoxNote.Text;
            if (string.IsNullOrEmpty(_order.Treatment) &&
                string.IsNullOrEmpty(_order.Note))
            {
                MessageBox.Show(Resources.MsgTreatmentRequired, this.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DBManager_MySQL.予約登録更新(_order);
            DialogResult = DialogResult.OK;
            Close();
        }

        // 「予約取消」ボタン
        private void ButtonCancelAppoint_Click(object sender, EventArgs e)
        {
            DBManager_MySQL.予約取消(_order);
            DialogResult = DialogResult.No;
            Close();
        }

        // 「閉じる」ボタン
        private void ButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ショートカット
        private void FormBodyworkPopup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ButtonClose_Click(sender, e);
            }
        }

        // 「言語設定」の変更
        private void ComboBoxCulture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCulture.SelectedIndex < 0) return;
            string culture = comboBoxCulture.Text.Split("(")[1].Split(")")[0];
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            SetCultureText();
            buttonSaveCulture.Visible = true;
        }

        // 「言語設定の保存」ボタン
        private void ButtonSaveCulture_Click(object sender, EventArgs e)
        {
            Common.言語設定(comboBoxCulture.SelectedText);
            int ret = DBManager_MySQL.従業員マスタ言語設定変更();
            if (ret == -1) return;
            buttonSaveCulture.Visible = false;
            MessageBox.Show(Resources.MsgCultureSaved, this.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

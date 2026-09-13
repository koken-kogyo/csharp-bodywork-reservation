namespace BodyWorkReservation
{
    public partial class FormBodyworkPopup : Form
    {
        private readonly bool _isNewOrder = false;
        private readonly Common.Order _order;

        public FormBodyworkPopup(bool isNewOrder, Common.Order order, FormBodyworkReservation frm)
        {
            InitializeComponent();

            // テキスト関連の初期設定
            labelEmployee.Text = order.EmpName;
            labelReservDt.Text = order.ReservDt.ToString("MM/dd");
            labelTimeSlot.Text = Common.TIMESLOT_NAME[order.TimeSlot];
            textBoxNote.Text = order.Note;

            // コンボボックスの初期設定
            comboBoxTreatment.DataSource = Common.TREATMENT_NAME;

            // ボタン関連の初期設定
            if (isNewOrder)
            {
                buttonCancelAppoint.Visible = false;
                buttonCancel.Left = buttonCancelAppoint.Left;
                buttonAppoint.Text = "予約";
            }
            else
            {
                comboBoxTreatment.Text = order.Treatment;
                buttonAppoint.Text = "予約変更";
            }
            this._isNewOrder = isNewOrder;
            this._order = order;
        }

        // 「予約」「予約変更」新規登録または更新
        private void ButtonAppoint_Click(object sender, EventArgs e)
        {
            _order.Treatment = comboBoxTreatment.Text;
            _order.Note = textBoxNote.Text;
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

        // 「キャンセル」ボタン
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void FormBodyworkPopup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                ButtonCancel_Click(sender, e);
            }
        }
    }
}

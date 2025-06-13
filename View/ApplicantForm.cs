using API.Services;
using API.Models;
using Lib.Utils;

namespace View
{
    public partial class ApplicantForm : Form
    {
        private readonly PMBClient _client = new();

        public ApplicantForm()
        {
            InitializeComponent();
            Load += ApplicantForm_Load;
        }

        private async void ApplicantForm_Load(object sender, EventArgs e)
        {
            var rules = await _client.GetDepartmentRulesAsync();
            cmbDepartment.Items.AddRange(rules.Select(r => r.DepartmentId).ToArray());
            cmbPaymentMethod.Items.AddRange(new[] { "Transfer Bank", "Kartu Kredit" });
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var origin = txtOrigin.Text.Trim();
            var departmentId = cmbDepartment.SelectedItem?.ToString();
            var bankAccount = txtBank.Text.Trim();
            var paymentMethodText = cmbPaymentMethod.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(origin) ||
                string.IsNullOrWhiteSpace(departmentId) || string.IsNullOrWhiteSpace(bankAccount) ||
                string.IsNullOrWhiteSpace(paymentMethodText))
            {
                MessageBox.Show("Semua field wajib diisi.");
                return;
            }

            string paymentMethod = paymentMethodText == "Kartu Kredit" ? "credit" : "bank";

            if (!PaymentValidator.IsValidPaymentFormat(bankAccount))
            {
                MessageBox.Show("Nomor rekening atau kartu tidak valid (harus 16 digit angka).", "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var rules = await _client.GetDepartmentRulesAsync();
            var rule = rules.FirstOrDefault(r => r.DepartmentId == departmentId);
            if (rule == null)
            {
                MessageBox.Show("Jurusan tidak ditemukan.");
                return;
            }

            var scores = new Dictionary<string, int>();
            foreach (var subject in rule.RequiredSubjects.Keys)
            {
                var input = Microsoft.VisualBasic.Interaction.InputBox($"Nilai {subject}:", "Input Nilai", "0");
                if (!int.TryParse(input, out int val) || val < 0 || val > 100)
                {
                    MessageBox.Show("Nilai tidak valid. Harus angka antara 0 - 100.");
                    return;
                }
                scores[subject] = val;
            }

            var applicant = new Applicant
            {
                Name = name,
                SchoolOrigin = origin,
                DepartmentId = departmentId,
                BankAccount = bankAccount,
                SubjectScores = scores,
                PaymentMethod = paymentMethod
            };

            var result = await _client.RegisterApplicantAsync(applicant);
            MessageBox.Show(result ? "Pendaftaran berhasil!" : "Pendaftaran gagal.", result ? "Sukses" : "Gagal", MessageBoxButtons.OK, result ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
    }
}

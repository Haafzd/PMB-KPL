using System;
using System.Windows.Forms;
using API.Services;

namespace View
{
    public partial class RegisterForm : Form
    {
        private readonly PMBClient _client = new();

        public RegisterForm()
        {
            InitializeComponent();
        }

        private async void btnRegister_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;
            var confirm = txtConfirm.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirm))
            {
                MessageBox.Show("Semua field wajib diisi.");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Password dan konfirmasi tidak cocok.");
                return;
            }

            var result = await _client.RegisterAsync(email, password, confirm);
            if (result.IsSuccess)
            {
                MessageBox.Show("Registrasi berhasil.");
                Hide();
                new LoginForm().Show();
            }
            else
            {
                MessageBox.Show($"Gagal: {result.ErrorMessage}");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            new LoginForm().Show();
        }
    }

}

using API.Services;
using System;
using System.Windows.Forms;

namespace View
{
    public partial class LoginForm : Form
    {
        private readonly PMBClient _client = new();

        public LoginForm()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text.Trim();
            var password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Email dan password wajib diisi.");
                return;
            }

            var result = await _client.LoginAsync(email, password);
            if (result.IsSuccess)
            {
                MessageBox.Show("Login berhasil!");
                Hide();
                new DashboardForm(email).Show();
            }
            else
            {
                MessageBox.Show($"Login gagal: {result.ErrorMessage}");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            Hide();
            new RegisterForm().Show();
        }
    }

}

namespace View
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private TextBox txtEmail;
        private TextBox txtPassword;
        private TextBox txtConfirm;
        private Button btnRegister;
        private Button btnBack;

        private void InitializeComponent()
        {
            this.txtEmail = new TextBox { PlaceholderText = "Email", Width = 200, Location = new Point(50, 30) };
            this.txtPassword = new TextBox { PlaceholderText = "Password", UseSystemPasswordChar = true, Width = 200, Location = new Point(50, 70) };
            this.txtConfirm = new TextBox { PlaceholderText = "Konfirmasi Password", UseSystemPasswordChar = true, Width = 200, Location = new Point(50, 110) };
            this.btnRegister = new Button { Text = "Daftar", Location = new Point(50, 150), Width = 95 };
            this.btnBack = new Button { Text = "Kembali", Location = new Point(155, 150), Width = 95 };

            this.btnRegister.Click += btnRegister_Click;
            this.btnBack.Click += btnBack_Click;

            this.Controls.AddRange(new Control[] { txtEmail, txtPassword, txtConfirm, btnRegister, btnBack });
            this.Text = "Form Registrasi";
            this.ClientSize = new Size(300, 210);
        }
        #endregion
    }
}

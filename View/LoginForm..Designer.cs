namespace View
{
    partial class LoginForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private TextBox txtEmail;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnRegister;

        private void InitializeComponent()
        {
            this.txtEmail = new TextBox { PlaceholderText = "Email", Width = 200, Location = new Point(50, 30) };
            this.txtPassword = new TextBox { PlaceholderText = "Password", UseSystemPasswordChar = true, Width = 200, Location = new Point(50, 70) };
            this.btnLogin = new Button { Text = "Masuk", Location = new Point(50, 110), Width = 95 };
            this.btnRegister = new Button { Text = "Daftar", Location = new Point(155, 110), Width = 95 };

            this.btnLogin.Click += btnLogin_Click;
            this.btnRegister.Click += btnRegister_Click;

            this.Controls.AddRange(new Control[] { txtEmail, txtPassword, btnLogin, btnRegister });
            this.Text = "Login";
            this.ClientSize = new Size(300, 180);
        }

        #endregion
    }
}

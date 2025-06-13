namespace View
{
    partial class ApplicantForm
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
        private TextBox txtName;
        private TextBox txtOrigin;
        private TextBox txtBank;
        private ComboBox cmbDepartment;
        private ComboBox cmbPaymentMethod;
        private Button btnSubmit;

        private void InitializeComponent()
        {
            txtName = new TextBox { PlaceholderText = "Nama", Location = new Point(20, 20), Width = 250 };
            txtOrigin = new TextBox { PlaceholderText = "Asal Sekolah", Location = new Point(20, 60), Width = 250 };
            cmbDepartment = new ComboBox { Location = new Point(20, 100), Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };
            txtBank = new TextBox { PlaceholderText = "Rekening/Kartu (16 digit)", Location = new Point(20, 140), Width = 250 };
            cmbPaymentMethod = new ComboBox
            {
                Location = new Point(20, 180),
                Width = 250,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "Transfer Bank", "Kartu Kredit" }
            };
            btnSubmit = new Button { Text = "Daftar", Location = new Point(20, 220), Width = 250 };

            btnSubmit.Click += btnSubmit_Click;

            Controls.AddRange(new Control[]
            {
        txtName, txtOrigin, cmbDepartment, txtBank, cmbPaymentMethod, btnSubmit
            });

            this.Text = "Form Pendaftaran Mahasiswa";
            this.ClientSize = new Size(300, 280);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }
        #endregion
    }
}

namespace View
{
    partial class ApplicantForm
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

        private TextBox txtName;
        private TextBox txtOrigin;
        private TextBox txtBank;
        private ComboBox cmbDepartment;
        private ComboBox cmbPaymentMethod;
        private Button btnSubmit;

        private void InitializeComponent()
        {
            txtName = new TextBox();
            txtOrigin = new TextBox();
            cmbDepartment = new ComboBox();
            txtBank = new TextBox();
            cmbPaymentMethod = new ComboBox();
            btnSubmit = new Button();
            SuspendLayout();

            // txtName
            txtName.Location = new Point(0, 0);
            txtName.Name = "txtName";
            txtName.Size = new Size(100, 27);
            txtName.TabIndex = 0;

            // txtOrigin
            txtOrigin.Location = new Point(0, 0);
            txtOrigin.Name = "txtOrigin";
            txtOrigin.Size = new Size(100, 27);
            txtOrigin.TabIndex = 1;

            // cmbDepartment
            cmbDepartment.Location = new Point(0, 0);
            cmbDepartment.Name = "cmbDepartment";
            cmbDepartment.Size = new Size(121, 28);
            cmbDepartment.TabIndex = 2;

            // txtBank
            txtBank.Location = new Point(0, 0);
            txtBank.Name = "txtBank";
            txtBank.Size = new Size(100, 27);
            txtBank.TabIndex = 3;

            // cmbPaymentMethod
            cmbPaymentMethod.Location = new Point(0, 0);
            cmbPaymentMethod.Name = "cmbPaymentMethod";
            cmbPaymentMethod.Size = new Size(121, 28);
            cmbPaymentMethod.TabIndex = 4;

            // btnSubmit
            btnSubmit.Location = new Point(0, 0);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.Size = new Size(75, 23);
            btnSubmit.TabIndex = 5;
            btnSubmit.Click += btnSubmit_Click;

            // ApplicantForm
            ClientSize = new Size(503, 148);
            Controls.Add(txtName);
            Controls.Add(txtOrigin);
            Controls.Add(cmbDepartment);
            Controls.Add(txtBank);
            Controls.Add(cmbPaymentMethod);
            Controls.Add(btnSubmit);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "ApplicantForm";
            Text = "Form Pendaftaran Mahasiswa";
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}

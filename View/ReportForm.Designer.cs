namespace View
{
    partial class ReportForm
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

        private TextBox txtReport;

        private void InitializeComponent()
        {
            this.txtReport = new TextBox
            {
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 10),
                BackColor = Color.White
            };

            this.Controls.Add(txtReport);
            this.Text = "Laporan Pendaftar";
            this.ClientSize = new Size(600, 400);
        }
        #endregion
    }
}

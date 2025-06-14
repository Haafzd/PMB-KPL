﻿namespace View
{
    partial class DashboardForm
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

        private Label lblWelcome;
        private Button btnDaftar;
        private Button btnLaporan;
        private Button btnLogout;

        private void InitializeComponent()
        {
            lblWelcome = new Label
            {
                Text = "Selamat datang",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };

            btnDaftar = new Button
            {
                Text = "Pendaftaran Mahasiswa",
                Location = new Point(20, 60),
                Width = 200
            };

            btnLaporan = new Button
            {
                Text = "Lihat Laporan",
                Location = new Point(20, 100),
                Width = 200
            };

            btnLogout = new Button
            {
                Text = "Keluar",
                Location = new Point(20, 140),
                Width = 200
            };

            btnDaftar.Click += btnDaftar_Click;
            btnLaporan.Click += btnLaporan_Click;
            btnLogout.Click += btnLogout_Click;

            Controls.AddRange(new Control[] { lblWelcome, btnDaftar, btnLaporan, btnLogout });

            this.Text = "Dashboard";
            this.ClientSize = new Size(260, 200);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }
        #endregion
    }
}

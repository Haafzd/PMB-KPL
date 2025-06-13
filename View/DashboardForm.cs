using System;
using System.Windows.Forms;

namespace View
{
    public partial class DashboardForm : Form
{
    private readonly string _userEmail;

    public DashboardForm(string userEmail)
    {
        InitializeComponent();
        _userEmail = userEmail;
        lblWelcome.Text = $"Selamat datang, {_userEmail}";
    }

    private void btnDaftar_Click(object sender, EventArgs e)
    {
        new ApplicantForm().Show();
    }

    private void btnLaporan_Click(object sender, EventArgs e)
    {
        new ReportForm().Show();
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        Close();
        new LoginForm().Show();
    }
}

}

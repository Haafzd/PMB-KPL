using API.Models;
using API.Services;

namespace View
{
    public partial class ReportForm : Form
    {
        private readonly PMBClient _client = new();

        public ReportForm()
        {
            InitializeComponent();
            Load += ReportForm_Load;
        }

        private async void ReportForm_Load(object sender, EventArgs e)
        {
            try
            {
                var applicants = await _client.GetApplicantsAsync();
                if (applicants == null || !applicants.Any())
                {
                    txtReport.Text = "Tidak ada data pendaftar.";
                    return;
                }

                var data = applicants.Select(a => new StudentReportData
                {
                    Name = a.Name,
                    Major = a.DepartmentId,
                    Year = DateTime.Now.Year,
                    NIM = $"{a.DepartmentId}{a.Id:D4}"
                }).ToList();

                var report = await _client.GenerateReportAsync(data);
                txtReport.Text = report;

            }
            catch (Exception ex)
            {
                txtReport.Text = $"Gagal memuat laporan: {ex.Message}";
            }
        }
    }
}

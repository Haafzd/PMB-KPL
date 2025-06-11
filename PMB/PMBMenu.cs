using API.Models;
using API.Services;
using Lib.Utils;

namespace PMB
{
    public class PMBMenu
    {
        private static bool _isLoggedIn = false;
        private static string _currentUser = "";
        private static readonly PMBClient _client = new();
        private static async Task PrintReport()
        {
            Console.Clear();

            var applicants = await _client.GetApplicantsAsync();
            if (applicants.Count == 0)
            {
                Console.WriteLine("Tidak ada pendaftar untuk dilaporkan.");
                Console.ReadKey();
                return;
            }

            var data = applicants.Select(a => new StudentReportData
            {
                Name = a.Name,
                Major = a.DepartmentId,
                Year = DateTime.Now.Year,
                NIM = NimGenerator.GenerateNim()
            }).ToList();

            var report = await _client.GenerateReportAsync(data);

            Console.WriteLine("=== Laporan Pendaftar ===\n");
            Console.WriteLine(report);
            Console.ReadKey();
        } 
    }
}

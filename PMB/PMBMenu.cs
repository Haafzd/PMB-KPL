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
        public static async Task Run()
        {
            while (!_isLoggedIn)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEM PMB ===");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Register");
                Console.WriteLine("3. Keluar");

                switch (InputHelper.PromptRequired("Pilih menu: "))
                {
                    case "1": await HandleLogin(); break;
                    case "2": await HandleRegister(); break;
                    case "3": Environment.Exit(0); break;
                    default:
                        Console.WriteLine("Input tidak valid.");
                        Console.ReadKey();
                        break;
                }
            }

            await ShowMainMenu();
        }
        private static async Task HandleLogin()
        {
            var email = InputHelper.PromptRequired("Email: ");

            if (!InputHelper.ValidateEmail(email))
            {
                Console.WriteLine("Format email tidak valid.");
                Console.ReadKey();
                return;
            }

            var password = InputHelper.PromptPassword("Password: ");

            var result = await _client.LoginAsync(email, password);

            if (result.IsSuccess)
            {
                _isLoggedIn = true;
                _currentUser = email;
                Console.WriteLine("Login berhasil!");
            }
            else
            {
                Console.WriteLine($"Login gagal: {result.ErrorMessage}");
            }
            Console.ReadKey();
        }

        private static async Task HandleRegister()
        {
            var email = InputHelper.PromptRequired("Email: ");
            if (!InputHelper.ValidateEmail(email))
            {
                Console.WriteLine("Format email tidak valid.");
                Console.ReadKey();
                return;
            }

            var password = InputHelper.PromptRequired("Password: ");
            var confirmPassword = InputHelper.PromptRequired("Ulangi Password: ");

            var result = await _client.RegisterAsync(email, password, confirmPassword);

            Console.WriteLine(result.IsSuccess ? "Registrasi berhasil!" : $"Gagal: {result.ErrorMessage}");
            Console.ReadKey();
        }

        private static async Task ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== MENU PMB === (User: {_currentUser})");
                Console.WriteLine("1. Daftar Mahasiswa Baru");
                Console.WriteLine("2. Lihat Daftar Pendaftar");
                Console.WriteLine("3. Cek Kuota Jurusan");
                Console.WriteLine("4. Cetak Laporan Pendaftar");
                Console.WriteLine("0. Logout");

                switch (InputHelper.PromptRequired("Pilih menu: "))
                {
                    case "1": await RegisterApplicant(); break;
                    case "2": await ShowApplicants(); break;
                    case "3": await ShowQuotas(); break;
                    case "4": await PrintReport(); break;
                    case "0": _isLoggedIn = false; _currentUser = ""; return;
                    default:
                        Console.WriteLine("Input tidak valid!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private static async Task RegisterApplicant()
        {
            Console.Clear();
            Console.WriteLine("=== Pendaftaran Mahasiswa Baru ===");

            var rules = await _client.GetDepartmentRulesAsync();

            var name = InputHelper.PromptRequired("Nama: ");
            var origin = InputHelper.PromptRequired("Asal Sekolah (SMA/SMK): ");
            var departmentId = InputHelper.PromptRequired("Jurusan (CS/LIT/ENG): ")?.ToUpper();
            var bankAccount = InputHelper.PromptRequired("Rekening Bank / Kartu Kredit (16 digit): ");

            if (!PaymentValidator.IsValidPaymentFormat(bankAccount))
            {
                Console.WriteLine("Nomor rekening atau kartu tidak valid.");
                Console.ReadKey();
                return;
            }

            var rule = rules.FirstOrDefault(r => r.DepartmentId == departmentId);
            if (rule == null)
            {
                Console.WriteLine("Jurusan tidak valid.");
                Console.ReadKey();
                return;
            }

            if (!string.Equals(origin, rule.RequiredSchoolType, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Asal sekolah harus {rule.RequiredSchoolType}");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Pilih metode pembayaran:");
            Console.WriteLine("1. Transfer Bank");
            Console.WriteLine("2. Kartu Kredit");

            string paymentInput = InputHelper.PromptRequired("Pilihan [1/2]: ");
            string paymentMethod = paymentInput switch
            {
                "1" => "bank",
                "2" => "credit",
                _ => ""
            };

            if (string.IsNullOrEmpty(paymentMethod))
            {
                Console.WriteLine("Metode pembayaran tidak valid.");
                Console.ReadKey();
                return;
            }

            var scores = new Dictionary<string, int>();
            foreach (var subject in rule.RequiredSubjects.Keys)
            {
                while (true)
                {
                    Console.Write($"Nilai {subject}: ");
                    var input = Console.ReadLine();

                    if (int.TryParse(input, out int score) && score >= 0 && score <= 100)
                    {
                        scores[subject] = score;
                        break;
                    }

                    Console.WriteLine("Nilai harus angka antara 0 - 100.");
                }
            }

            var applicant = new Applicant
            {
                Name = name,
                SchoolOrigin = origin,
                DepartmentId = departmentId,
                BankAccount = bankAccount,
                SubjectScores = scores,
                PaymentMethod = paymentMethod
            };

            var result = await _client.RegisterApplicantAsync(applicant);

            Console.WriteLine(result ? "Pendaftaran berhasil!" : "Pendaftaran gagal.");
            Console.ReadKey();
        }

        private static async Task ShowApplicants()
        {
            Console.Clear();
            var list = await _client.GetApplicantsAsync();
            if (list.Count == 0)
                Console.WriteLine("Belum ada pendaftar.");
            else
                list.ForEach(a => Console.WriteLine($"{a.Id}. {a.Name} - {a.DepartmentId} - {a.SchoolOrigin}"));
            Console.ReadKey();
        }

        private static async Task ShowQuotas()
        {
            Console.Clear();
            var quotas = await _client.GetQuotasAsync();
            foreach (var q in quotas)
                Console.WriteLine($"{q.Key}: {q.Value} kursi");
            Console.ReadKey();
        }

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

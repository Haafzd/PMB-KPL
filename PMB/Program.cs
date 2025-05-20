using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using PMB.Models;
using PMB.Services;
using PMB.Reporting;
using PMB.Security;
using PMB.StateMachine;

class Program
{
    private static IConfiguration _configuration;
    private static DepartmentQuotaService _quotaService;
    private static DepartmentRuleValidator _validator;
    private static List<Applicant> _applicants = new List<Applicant>();
    private static List<User> _users = new List<User>();

    static void Main(string[] args)
    {
        TampilkanHeader();
        InisialisasiAplikasi();
        TampilkanMenuUtama();
    }

    static void TampilkanHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("====================================");
        Console.WriteLine("  SISTEM PENERIMAAN MAHASISWA BARU  ");
        Console.WriteLine("====================================");
        Console.ResetColor();
    }

    static void InisialisasiAplikasi()
    {
        try
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            _configuration = configBuilder.Build();

            var quotaConfig = _configuration.Get<QuotaConfig>() ?? throw new Exception("Konfigurasi kuota tidak valid");
            var rules = new DepartmentRuleLoader().LoadRules();

            _quotaService = new DepartmentQuotaService(quotaConfig);
            _validator = new DepartmentRuleValidator(rules);

            Console.WriteLine("\n✅ Aplikasi berhasil diinisialisasi");
        }
        catch (Exception ex)
        {
            TampilkanPesanError($"Gagal memulai aplikasi: {ex.Message}");
            Environment.Exit(1);
        }
    }

    static void TampilkanMenuUtama()
    {
        while (true)
        {
            Console.WriteLine("\nMenu Utama:");
            Console.WriteLine("1. Pengaturan Aplikasi");
            Console.WriteLine("2. Pendaftaran Pelamar");
            Console.WriteLine("3. Cek Kuota Jurusan");
            Console.WriteLine("4. Proses Pembayaran");
            Console.WriteLine("5. Generate Laporan");
            Console.WriteLine("6. Simulasi Login");
            Console.WriteLine("7. Keluar");

            Console.Write("\nPilih menu (1-7): ");
            var pilihan = Console.ReadLine();

            switch (pilihan)
            {
                case "1":
                    PengaturanAplikasi();
                    break;
                case "2":
                    PendaftaranPelamar();
                    break;
                case "3":
                    CekKuotaJurusan();
                    break;
                case "4":
                    ProsesPembayaran();
                    break;
                case "5":
                    GenerateLaporan();
                    break;
                case "6":
                    SimulasiLogin();
                    break;
                case "7":
                    TampilkanPesanInfo("Terima kasih telah menggunakan sistem kami!");
                    Environment.Exit(0);
                    break;
                default:
                    TampilkanPesanError("Pilihan tidak valid!");
                    break;
            }
        }
    }

    static void PengaturanAplikasi()
    {
        Console.Clear();
        TampilkanHeader();
        Console.WriteLine("\n⚙️ Pengaturan Aplikasi");
        Console.WriteLine($"Status Konfigurasi: {(_configuration != null ? "Aktif" : "Tidak Aktif")}");
        Console.WriteLine($"Jumlah Pelamar Terdaftar: {_applicants.Count}");
        Console.WriteLine($"Jumlah Pengguna: {_users.Count}");
        Console.WriteLine("\nTekan tombol apa saja untuk kembali...");
        Console.ReadKey();
    }

    static void PendaftaranPelamar()
    {
        Console.Clear();
        TampilkanHeader();
        Console.WriteLine("\n📝 Pendaftaran Pelamar Baru");

        try
        {
            var applicant = new Applicant();

            Console.Write("Nama Lengkap: ");
            applicant.Name = Console.ReadLine();

            Console.Write("Asal Sekolah: ");
            applicant.SchoolOrigin = Console.ReadLine();

            Console.Write("Nilai Matematika: ");
            applicant.MathScore = Convert.ToInt32(Console.ReadLine());

            Console.Write("Nomor Rekening: ");
            applicant.BankAccount = Console.ReadLine();

            Console.Write("Pilihan Jurusan (CS/EE/CE): ");
            var jurusan = Console.ReadLine().ToUpper();

            if (_validator.IsValid(applicant, jurusan))
            {
                _applicants.Add(applicant);
                TampilkanPesanSukses($"Pendaftaran {applicant.Name} berhasil!");
            }
            else
            {
                TampilkanPesanError("Pelamar tidak memenuhi syarat jurusan");
            }
        }
        catch (Exception ex)
        {
            TampilkanPesanError($"Error: {ex.Message}");
        }
    }

    static void CekKuotaJurusan()
    {
        Console.Clear();
        TampilkanHeader();
        Console.WriteLine("\n📊 Cek Kuota Jurusan");

        Console.Write("Masukkan kode jurusan (CS/EE/CE): ");
        var jurusan = Console.ReadLine().ToUpper();

        var tersedia = _quotaService.IsQuotaAvailable(jurusan);
        Console.WriteLine($"Kuota {jurusan}: {(tersedia ? "Masih tersedia" : "Sudah penuh")}");

        Console.WriteLine("\nTekan tombol apa saja untuk kembali...");
        Console.ReadKey();
    }

    static void ProsesPembayaran()
    {
        Console.Clear();
        TampilkanHeader();
        Console.WriteLine("\n💳 Proses Pembayaran");

        try
        {
            Console.WriteLine("Pilih metode pembayaran:");
            Console.WriteLine("1. Kartu Kredit");
            Console.WriteLine("2. Transfer Bank");
            Console.Write("Pilihan (1-2): ");

            var pilihan = Console.ReadLine();
            Console.Write("Jumlah Pembayaran: ");
            var jumlah = Convert.ToDecimal(Console.ReadLine());

            if (pilihan == "1")
            {
                Console.Write("Nomor Kartu (16 digit): ");
                var ccNumber = Console.ReadLine();
                var payment = new CreditCardPayment(ccNumber);
                var processor = new PaymentProcessor<CreditCardPayment>(payment);
                processor.ExecutePayment(jumlah);
                TampilkanPesanSukses("Pembayaran kartu kredit berhasil!");
            }
            else if (pilihan == "2")
            {
                Console.Write("Nomor Rekening: ");
                var rekening = Console.ReadLine();
                var payment = new BankTransferPayment(rekening);
                var processor = new PaymentProcessor<BankTransferPayment>(payment);
                processor.ExecutePayment(jumlah);
                TampilkanPesanSukses("Pembayaran transfer bank berhasil!");
            }
        }
        catch (Exception ex)
        {
            TampilkanPesanError($"Pembayaran gagal: {ex.Message}");
        }
    }

    static void GenerateLaporan()
    {
        Console.Clear();
        TampilkanHeader();
        Console.WriteLine("\n📄 Generate Laporan");

        try
        {
            var template = ReportTemplateLoader.LoadTemplate();
            var config = new ReportFormatConfig { Separator = ",", UseHeader = true };
            var generator = new ReportGenerator(config);

            var output = generator.GenerateReport(new List<StudentReportData>(), template);
            File.WriteAllText("laporan.csv", output);

            TampilkanPesanSukses("Laporan berhasil di-generate (laporan.csv)");
        }
        catch (Exception ex)
        {
            TampilkanPesanError($"Gagal generate laporan: {ex.Message}");
        }
    }

    static void SimulasiLogin()
    {
        Console.Clear();
        TampilkanHeader();
        Console.WriteLine("\n🔐 Simulasi Login");

        Console.Write("Email: ");
        var email = Console.ReadLine();

        Console.Write("Password: ");
        var password = ReadPassword();

        var user = new User { Email = email, PasswordHashWithSalt = PasswordHasher.HashPassword(password) };
        var fsm = new LoginStateMachine(user);

        while (fsm.CurrentState != LoginState.Authenticated && fsm.CurrentState != LoginState.Locked)
        {
            Console.Write("\nEmail: ");
            var inputEmail = Console.ReadLine();

            Console.Write("Password: ");
            var inputPass = ReadPassword();

            try
            {
                var sukses = fsm.ProvideCredentials(inputEmail, inputPass);
                Console.WriteLine(sukses ? "Login berhasil!" : "Login gagal!");
            }
            catch (Exception ex)
            {
                TampilkanPesanError(ex.Message);
            }
        }
    }

    #region Helper Methods
    static string ReadPassword()
    {
        var pass = "";
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && pass.Length > 0)
            {
                Console.Write("\b \b");
                pass = pass[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                pass += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);

        Console.WriteLine();
        return pass;
    }

    static void TampilkanPesanSukses(string pesan)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n✓ {pesan}");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void TampilkanPesanError(string pesan)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n✗ {pesan}");
        Console.ResetColor();
        Console.ReadKey();
    }

    static void TampilkanPesanInfo(string pesan)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"\nℹ️ {pesan}");
        Console.ResetColor();
    }
    #endregion
}
using System;
using System.IO;
using Microsoft.Extensions.Configuration;

using PMB.Models;
using PMB.Services;
using PMB.Reporting;
using PMB.Services;
using PMB.Security;
using PMB.StateMachine;


class Program
{
    static void Main(string[] args)
    {

        try
        // Load konfigurasi dari appsettings.json
        var config = new ConfigurationBuilder();
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        var configuration = configBuilder.Build();

        var quotaConfig = configuration.Get<QuotaConfig>();



        var ruleLoader = new DepartmentRuleLoader();
        var rules = ruleLoader.LoadRules();

        var validator = new DepartmentRuleValidator(rules);
        var quotaService = new DepartmentQuotaService(quotaConfig);

        var applicant = new Applicant
        {
            var creditCardPayment = new CreditCardPayment("1234567812345678");
            var creditProcessor = new PaymentProcessor<CreditCardPayment>(creditCardPayment);
            creditProcessor.ExecutePayment(1000m);

            var bankTransferPayment = new BankTransferPayment("9876543210");
            var bankProcessor = new PaymentProcessor<BankTransferPayment>(bankTransferPayment);
            bankProcessor.ExecutePayment(500m);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        
        var formatConfig = configuration.GetSection("ReportFormat").Get<ReportFormatConfig>();

        // Load data
        var students = new List<StudentReportData>
        {
            new StudentReportData { Name = "ucup", NIM = "120220001", Major = "Software Engineering", Year = 2022 },
            new StudentReportData { Name = "Ayu", NIM = "120220002", Major = "Data Science", Year = 2021 }
        };

        // Load template
        var template = ReportTemplateLoader.LoadTemplate();

        // Buat generator dan hasilkan laporan
        var generator = new ReportGenerator(formatConfig);
        string reportOutput = generator.GenerateReport(students, template);

        // Tampilkan hasil di console
        Console.WriteLine("=== Laporan Pendaftaran ===");
        Console.WriteLine(reportOutput);

        // Simpan ke file
        File.WriteAllText("laporan.csv", reportOutput);
        Console.WriteLine("\n>> Laporan disimpan ke 'laporan.csv'");

        Console.Write("Set username untuk user baru: ");
            string email = Console.ReadLine();

            Console.Write("Set password untuk user baru: ");
            string password = ReadPassword();

            var user = new User
            {
                Email = email,
                PasswordHashWithSalt = PasswordHasher.HashPassword(password)
            };

            Console.WriteLine("\n\n-- Simulasi Login --");

            var fsm = new LoginStateMachine(user);

            while (fsm.CurrentState != LoginState.Authenticated && fsm.CurrentState != LoginState.Locked)
            {
                Console.Write("\nMasukkan username: ");
                string inputUser = Console.ReadLine();

                Console.Write("Masukkan password: ");
                string inputPass = ReadPassword();

                try
                {
                    bool success = fsm.ProvideCredentials(inputUser, inputPass);

                    if (success)
                    {
                        Console.WriteLine("\n Login berhasil! Anda telah masuk.");
                    }
                    else
                    {
                        Console.WriteLine($"\n Login gagal. Status sekarang: {fsm.CurrentState}");
                        if (fsm.CurrentState == LoginState.Locked)
                            Console.WriteLine(" Akun terkunci setelah 3 kali percobaan.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Error] {ex.Message}");
                }
            }
        }

        // Fungsi input password tidak terlihat di layar
        static string ReadPassword()
        {
            string pass = "";
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
}

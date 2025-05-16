using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;

using PMB.Models;
using PMB.Services;

class Program
{
    static void Main(string[] args)
    {
               var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var quotaConfig = config.Get<QuotaConfig>();

    
        var ruleLoader = new DepartmentRuleLoader();
        var rules = ruleLoader.LoadRules();

        var validator = new DepartmentRuleValidator(rules);
        var quotaService = new DepartmentQuotaService(quotaConfig);

        var applicant = new Applicant
        {
            Name = "Budi",
            SchoolOrigin = "SMA",
            MathScore = 85
        };

        string departmentId = "CS";

        Console.WriteLine($"Validasi untuk pendaftar {applicant.Name} ke jurusan {departmentId}...");
        
        if (validator.IsValid(applicant, departmentId))
        {
            if (quotaService.IsQuotaAvailable(departmentId))
                Console.WriteLine(" Pendaftar diterima!");
            else
                Console.WriteLine(" Kuota jurusan habis.");
        }
        else
        {
            Console.WriteLine(" Pendaftar tidak memenuhi syarat jurusan.");
			Console.Write("Set username untuk user baru: ");
			            string username = Console.ReadLine();

			            Console.Write("Set password untuk user baru: ");
			            string password = ReadPassword();

			            var user = new User
			            {
			                Username = username,
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
}

using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;

using PMB.Models;
using PMB.Reporting;

class Program
{
    static void Main(string[] args)
    {
        // Load konfigurasi dari appsettings.json
        var config = new ConfigurationBuilder()
        var configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        var configuration = configBuilder.Build();
        
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

    }

}

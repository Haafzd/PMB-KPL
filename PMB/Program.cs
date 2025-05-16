using System;
using System.IO;
gitusing Microsoft.Extensions.Configuration;
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
        }
    }
}

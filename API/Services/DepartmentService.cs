using API.Models;
using System.Text.Json;

namespace API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private const string QuotaFile = "Data/quota.json";
        private const string RulesFile = "Data/rules.json";

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly Dictionary<string, int> _quota;
        private readonly List<DepartmentRule> _rules;

        public DepartmentService()
        {
            _quota = LoadQuota();
            _rules = LoadRules();

            foreach (var rule in _rules)
            {
                if (rule.Fee <= 0)
                {
                    Console.WriteLine($"Peringatan: Biaya jurusan {rule.DepartmentId} belum diatur (Fee = 0).");
                }
            }
        }

        public List<DepartmentRule> GetRules() => _rules;

        public Dictionary<string, int> GetQuotas() => _quota;

        public bool ValidateApplicant(Applicant applicant) =>
            GetApplicantValidationReason(applicant) == null;

        public string? GetApplicantValidationReason(Applicant applicant)
        {
            var rule = _rules.FirstOrDefault(r => r.DepartmentId == applicant.DepartmentId);
            if (rule == null)
                return "Jurusan tidak ditemukan.";

            if (applicant.SchoolOrigin != rule.RequiredSchoolType)
                return $"Tipe sekolah harus {rule.RequiredSchoolType}.";

            foreach (var subject in rule.RequiredSubjects)
            {
                if (!applicant.SubjectScores.TryGetValue(subject.Key, out int score) || score < subject.Value)
                    return $"Nilai {subject.Key} minimal {subject.Value}.";
            }

            if (!_quota.TryGetValue(applicant.DepartmentId, out int quota) || quota <= 0)
                return $"Kuota jurusan {applicant.DepartmentId} habis.";

            if (rule.Fee <= 0)
                return $"Biaya jurusan {applicant.DepartmentId} belum diatur.";

            return null;
        }

        public void DecreaseQuota(string departmentId)
        {
            if (_quota.ContainsKey(departmentId) && _quota[departmentId] > 0)
            {
                _quota[departmentId]--;
                SaveQuota();
            }
        }

        public decimal? GetFeeByDepartment(string departmentId)
        {
            var rule = _rules.FirstOrDefault(r => r.DepartmentId == departmentId);
            return rule?.Fee;
        }

        private Dictionary<string, int> LoadQuota()
        {
            try
            {
                if (!File.Exists(QuotaFile))
                    return CreateDefaultQuota();

                var json = File.ReadAllText(QuotaFile);
                if (string.IsNullOrWhiteSpace(json))
                    return CreateDefaultQuota();

                return JsonSerializer.Deserialize<Dictionary<string, int>>(json)
                    ?? CreateDefaultQuota();
            }
            catch (JsonException)
            {
                return CreateDefaultQuota();
            }
        }

        private Dictionary<string, int> CreateDefaultQuota() => new()
        {
            ["CS"] = 50,
            ["LIT"] = 40,
            ["ENG"] = 30
        };

        private List<DepartmentRule> LoadRules()
        {
            try
            {
                if (!File.Exists(RulesFile))
                    return new List<DepartmentRule>();

                var json = File.ReadAllText(RulesFile);
                if (string.IsNullOrWhiteSpace(json))
                    return new List<DepartmentRule>();

                return JsonSerializer.Deserialize<List<DepartmentRule>>(json)
                    ?? new List<DepartmentRule>();
            }
            catch (JsonException)
            {
                return new List<DepartmentRule>();
            }
        }

        private void SaveQuota()
        {
            try
            {
                var dir = Path.GetDirectoryName(QuotaFile);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir ?? "Data");

                var json = JsonSerializer.Serialize(_quota, _jsonOptions);
                File.WriteAllText(QuotaFile, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving quota: {ex.Message}");
            }
        }
    }
}

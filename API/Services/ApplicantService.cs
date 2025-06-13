using API.Models;

using System.Text.Json;

namespace API.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly string _filePath = Path.Combine("Data", "applicants.json");
        private List<Applicant> _applicants;

        public ApplicantService()
        {
            LoadData();
        }

        private void LoadData()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _applicants = JsonSerializer.Deserialize<List<Applicant>>(json) ?? new();
            }
            else
            {
                _applicants = new();
            }
        }

        private void SaveData()
        {
            var json = JsonSerializer.Serialize(_applicants);
            File.WriteAllText(_filePath, json);
        }

        public List<Applicant> GetAllApplicants() => _applicants;

        public Applicant GetApplicantById(int id) => _applicants.FirstOrDefault(a => a.Id == id);

        public void AddApplicant(Applicant applicant)
        {
            applicant.Id = _applicants.Count + 1;
            _applicants.Add(applicant);
            SaveData();
        }
    }
}

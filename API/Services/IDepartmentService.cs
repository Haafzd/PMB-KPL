using API.Models;

namespace API.Services
{
    public interface IDepartmentService
    {
        List<DepartmentRule> GetRules();
        Dictionary<string, int> GetQuotas();
        bool ValidateApplicant(Applicant applicant);
        string? GetApplicantValidationReason(Applicant applicant);
        void DecreaseQuota(string departmentId);
        decimal? GetFeeByDepartment(string departmentId);
    }
}

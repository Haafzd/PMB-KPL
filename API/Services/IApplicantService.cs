using API.Models;

namespace API.Services
{
    public interface IApplicantService
    {
        List<Applicant> GetAllApplicants();
        Applicant GetApplicantById(int id);
        void AddApplicant(Applicant applicant);
    }
}

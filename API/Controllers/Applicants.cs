using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMB.Models;

using System.Linq;

namespace API;

// PMB-API/Controllers/ApplicantController.cs
[ApiController]
[Route("api/[controller]")]
public class Applicants : ControllerBase
{
    private readonly AppDbContext _context;

    public Applicants(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/applicant
    [HttpGet]
    public ActionResult<IEnumerable<PMB.Models.Applicant>> GetApplicants()
    {
        // Cast _context.Applicants to DbSet<Applicant> to resolve the issue
        return ((DbSet<PMB.Models.Applicant>)_context.Applicants).ToList();
    }

    // POST: api/applicant
    [HttpPost]
    public ActionResult<PMB.Models.Applicant> CreateApplicant([FromBody] PMB.Models.Applicant applicant)
    {
        ((DbSet<PMB.Models.Applicant>)_context.Applicants).Add(applicant);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetApplicants), applicant);
    }
}

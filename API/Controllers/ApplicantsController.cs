using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PMB.Models;
using System.Collections.Generic;
using System.Linq;

namespace API
{
    /// Controller untuk manajemen data pelamar
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicantsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApplicantsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Mengambil semua data pelamar
        [HttpGet]
        public ActionResult<IEnumerable<Applicant>> GetApplicants()
        {
            return ((DbSet<PMB.Models.Applicant>)_context.Applicants).ToList();
        }

        // POST: Membuat data pelamar baru
        [HttpPost]
        public ActionResult<Applicant> CreateApplicant([FromBody] Applicant applicant)
        {
            _context.Applicants.Add(applicant);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetApplicants), applicant);
        }
    }
}
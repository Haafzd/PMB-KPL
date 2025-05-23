using Microsoft.EntityFrameworkCore;
using PMB.Models;

namespace API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Applicant> Applicants { get; set; } // Correct type
    }
}
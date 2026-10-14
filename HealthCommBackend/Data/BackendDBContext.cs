using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HealthCommBackend.Models;
using Hl7.Fhir.Model;

namespace HealthCommBackend.Data
{
    public class BackendDBContext : IdentityDbContext<ApplicationUser>
    {
        public BackendDBContext(DbContextOptions<BackendDBContext> options)
            : base(options)
        {
        }
        /*
        public DbSet<Practicioner> Practicioners { get; set; }
        public DbSet<Patient> Patients { get; set; } */
    }
}

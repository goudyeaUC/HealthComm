using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HealthCommBackend.Models;

namespace HealthCommBackend.Data
{
    public class BackendDBContext : IdentityDbContext<ApplicationUser>
    {
        public BackendDBContext(DbContextOptions<BackendDBContext> options)
            : base(options)
        {
        }
    }
}

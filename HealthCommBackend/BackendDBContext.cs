using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HealthCommBackend.Models;

namespace HealthCommBackend
{
    public class BackendDBContext : IdentityDbContext<ApplicationUser>
    {
    }
}

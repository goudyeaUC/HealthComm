using Microsoft.AspNetCore.Identity;
using HealthCommBackend.Models;
using HealthCommBackend.Data;
using Hl7.Fhir.Model;

namespace HealthCommBackend.Services
{
    public class LoginService
    {
        private UserManager<ApplicationUser> _userManager;
        private BackendDBContext _context;

        public LoginService(UserManager<ApplicationUser> userManager, BackendDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task 

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}

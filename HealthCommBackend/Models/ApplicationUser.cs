using Microsoft.AspNetCore.Identity;

namespace HealthCommBackend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation property for the related PatientProfile
        public PatientProfile? PatientProfile { get; set; }

        // Navigation property for the related PractitionerProfile
        public PractitionerProfile? PractitionerProfile { get; set; }
    }
}

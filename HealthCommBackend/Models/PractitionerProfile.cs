namespace HealthCommBackend.Models
{
    public class PractitionerProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Foreign key to ApplicationUser
        public ApplicationUser UnderlyingUserKey { get; set; } // Navigation property to ApplicationUser
    }
}

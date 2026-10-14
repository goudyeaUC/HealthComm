namespace HealthCommBackend.Models
{
    public class PatientProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!; // Foreign key to ApplicationUser
        public ApplicationUser UnderlyingUserKey { get; set; } = null!; // Navigation property to ApplicationUser
    }
}

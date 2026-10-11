namespace HealthCommBackend.Models
{
    public class PatientProfile
    {
        public int Id { get; set; }
        public ApplicationUser UnderlyingUserKey { get; set; }
    }
}

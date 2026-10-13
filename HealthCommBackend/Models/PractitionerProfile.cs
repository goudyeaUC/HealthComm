namespace HealthCommBackend.Models
{
    public class PractitionerProfile
    {
        public int Id { get; set; }
        public ApplicationUser UnderlyingUserKey { get; set; }
    }
}

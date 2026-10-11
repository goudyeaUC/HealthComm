namespace HealthCommBackend.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public ApplicationUser UnderlyingUserKey { get; set; }
    }
}

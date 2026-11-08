namespace HealthCommBackend.Models
{
    public class RefillRequest
    {
        public int Id { get; set; }

        public int PrescriptionId { get; set; }

        public Prescription Prescription { get; set; } = null!;

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = RefillStatus.Requested;

        public string? PatientNote { get; set; }

        public string? ResponseNote { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public string? ReviewedById { get; set; }
        //Id of the ApplicationUser (practitioner) who approved or denied the request

        public DateTime? EstimatedReadyDate { get; set; }
        //Refill timeline shown to the patient once a request is approved
    }

    public static class RefillStatus
    {
        public const string Requested = "requested";
        public const string Approved = "approved";
        public const string Denied = "denied";
    }
}

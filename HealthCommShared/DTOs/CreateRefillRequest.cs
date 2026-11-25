namespace HealthCommShared.DTOs
{
    public class CreateRefillRequest
    {
        public int PrescriptionId { get; set; }

        public string PatientId { get; set; } = string.Empty;

        public string? PatientNote { get; set; }
    }
}

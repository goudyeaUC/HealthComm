namespace HealthCommBackend.Models
{
    public class MedicationRecord
    {
        public int MedicationId { get; set; }

        public int PatientId { get; set; }

        public string MedicationName { get; set; } = string.Empty;

        public string Dosage { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;

        public int Refills { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string PrescribedBy { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
            = DateTime.UtcNow;

        //  public Patient? Patient { get; set; }
    }
}

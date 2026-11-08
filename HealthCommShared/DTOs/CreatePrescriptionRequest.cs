using System;

namespace HealthCommShared.DTOs
{
    public class CreatePrescriptionRequest
    {
        public string PatientId { get; set; } = string.Empty;

        public string PrescriberId { get; set; } = string.Empty;

        public string MedicationName { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;

        public string? SNOMEDDrugCode { get; set; }

        public string? ReasonForPrescription { get; set; }

        public string? SNOMEDReasonCode { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int RefillsRemaining { get; set; }
    }
}

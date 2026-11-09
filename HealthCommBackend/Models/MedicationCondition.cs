namespace HealthCommBackend.Models
{
    public class MedicationCondition
    {
        public int ConditionId { get; set; }


        public int PatientId { get; set; }

        public string ConditionName { get; set; } = string.Empty;

        // Examples: Disease, Allergy, Injury

        public string Category { get; set; } = "Disease";

        // Examples: Active, Resolved, Inactive

        public string ClinicalStatus { get; set; } = "active";

        // Examples: Confirmed, Provisional, Unconfirmed

        public string VerificationStatus { get; set; } = "confirmed";

        // Examples: Mild, Moderate, Severe

        public string? Severity { get; set; }


        public string? BodySite { get; set; }

        public DateTime? OnsetDate { get; set; }

        public DateTime? ResolvedDate { get; set; }

        public DateTime RecordedDate { get; set; }
            = DateTime.UtcNow;


        public string? Notes { get; set; }

        // FHIR mapping fields

        public string? FHIRConditionId { get; set; }


        public string? FHIRCode { get; set; }


        public string? FHIRCodeSystem { get; set; }


       // public PatientProfile? Patient { get; set; }
    }
}

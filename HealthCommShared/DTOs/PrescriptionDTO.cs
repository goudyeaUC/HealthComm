using System;

namespace HealthCommShared.DTOs
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }

        public string PatientId { get; set; } = string.Empty;

        public string PrescriberId { get; set; } = string.Empty;

        public string PrescriberName { get; set; } = string.Empty;

        public string MedicationName { get; set; } = string.Empty;

        public string Instructions { get; set; } = string.Empty;

        public string? SNOMEDDrugCode { get; set; }

        public string? ReasonForPrescription { get; set; }

        public string? SNOMEDReasonCode { get; set; }

        public DateTime DatePrescribed { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime? DateFilled { get; set; }

        public string ActiveStatus { get; set; } = "active";

        public int RefillsRemaining { get; set; }

        // Status of the most recent refill request, or null if none has been made
        public string? RefillStatus { get; set; }

        public DateTime? RefillEstimatedReadyDate { get; set; }

        public bool CanRequestRefill =>
            RefillsRemaining > 0 &&
            ExpirationDate > DateTime.UtcNow &&
            string.Equals(ActiveStatus, "active", StringComparison.OrdinalIgnoreCase) &&
            RefillStatus != "requested";

        public string DisplayText =>
            $"{MedicationName} - {Instructions}";

        public string DetailText =>
            $"Refills: {RefillsRemaining} | Expires: {ExpirationDate:MMM d, yyyy} | {ActiveStatus}";

        public string RefillText => RefillStatus switch
        {
            "requested" => "Refill requested - awaiting review",
            "approved" => RefillEstimatedReadyDate.HasValue
                ? $"Refill approved - ready by {RefillEstimatedReadyDate:MMM d, yyyy}"
                : "Refill approved",
            "denied" => "Last refill request was denied",
            _ => string.Empty
        };
    }
}

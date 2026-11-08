using System;

namespace HealthCommShared.DTOs
{
    public class RefillRequestDTO
    {
        public int Id { get; set; }

        public int PrescriptionId { get; set; }

        public string MedicationName { get; set; } = string.Empty;

        public string PatientId { get; set; } = string.Empty;

        public string PatientName { get; set; } = string.Empty;

        public DateTime RequestedAt { get; set; }

        public string Status { get; set; } = "requested";

        public string? PatientNote { get; set; }

        public string? ResponseNote { get; set; }

        public DateTime? ReviewedAt { get; set; }

        public DateTime? EstimatedReadyDate { get; set; }
    }
}

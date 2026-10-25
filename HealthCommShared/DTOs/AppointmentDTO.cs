using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCommShared.DTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }

        public int PatientProfileId { get; set; }

        public int PractitionerProfileId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public string VisitType { get; set; } = string.Empty;

        public string Status { get; set; } = "proposed";

        public string? Description { get; set; }

        public string? CancellationReason { get; set; }

        public string? FHIRIdentifier { get; set; }

        public DateTime? LastSyncedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

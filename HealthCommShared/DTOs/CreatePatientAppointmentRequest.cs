using System;
using System.Collections.Generic;
using System.Text;

namespace HealthCommShared.DTOs
{
    public class CreatePatientAppointmentRequest
    {
        public int PatientProfileId { get; set; }

        public int PractitionerProfileId { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        public string VisitType { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}

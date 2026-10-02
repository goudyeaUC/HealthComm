using System;
using System.Collections.Generic;
using System.Text;

namespace HealthComm.Models
{
    internal class Appointment
    {
        public string VisitType { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string DisplayText =>
        $"{VisitType} - {AppointmentDate:MMM dd, yyyy hh:mm tt}";
    }
}

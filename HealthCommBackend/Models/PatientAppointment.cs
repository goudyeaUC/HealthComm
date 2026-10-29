namespace HealthCommBackend.Models
{
    public class PatientAppointment
    {
        public int Id { get; set; }

        // Foreign key to PatientProfile

        public int PatientProfileId { get; set; }
        public int PractitionerProfileId { get; set; }

        public string VisitType { get; set; } = string.Empty;

        public DateTime StartDateTime { get; set; }

    }
}

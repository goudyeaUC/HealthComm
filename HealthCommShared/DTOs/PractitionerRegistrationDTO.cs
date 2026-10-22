namespace HealthCommShared.DTOs
{
    public class PractitionerRegistrationDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string? Specialty { get; set; } // FHIR field for description of the practitioner's specialty


    }
}

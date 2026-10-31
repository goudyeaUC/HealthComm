namespace HealthCommBackend.Models
{
    public class PatientProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!; // Foreign key to ApplicationUser
        public ApplicationUser UnderlyingUserKey { get; set; } = null!; // Navigation property to ApplicationUser

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string AdministrativeGender { get; set; } = null!;
        //This is the FHIR standard's version of this property
        //Supported values: male, female, other, unknown

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? LastSyncedAt { get; set; }
        //FHIR field; indicates the last time the patient's information was synchronized with an external system

        public string? FHIRIdentifier { get; set; } //ID key for the patient in a FHIR system
    }
}

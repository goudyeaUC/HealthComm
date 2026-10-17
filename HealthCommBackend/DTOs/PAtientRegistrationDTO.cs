namespace HealthCommBackend.DTOs
{
    public class PatientRegistrationDTO
    {
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string GenderSelection { get; set; } = null!;
        //FHIR AdministrativeGender property- supported values: male, female, other, unknown
        public DateTime DateOfBirth { get; set; }
    }
}

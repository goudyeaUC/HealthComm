using HealthCommBackend.DTOs;

namespace HealthCommBackend.Interfaces
{
    public interface IUserRegistrationService
    {
        Task RegisterPatientAsync(PatientRegistrationDTO patientRegistrationRequest);
        Task RegisterPractitionerAsync(PractitionerRegistrationDto practitionerRegistrationRequest);
    }
}

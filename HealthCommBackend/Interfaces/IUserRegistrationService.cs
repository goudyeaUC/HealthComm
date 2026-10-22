using HealthCommShared.DTOs;
using Microsoft.AspNetCore.Identity;

namespace HealthCommBackend.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<IdentityResult> RegisterPatientAsync(PatientRegistrationDTO patientRegistrationRequest);
        Task<IdentityResult> RegisterPractitionerAsync(PractitionerRegistrationDTO practitionerRegistrationRequest);
    }
}

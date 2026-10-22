using Microsoft.AspNetCore.Identity;
using HealthCommBackend.Models;
using HealthCommBackend.Data;
using HealthCommShared.DTOs;
using HealthCommBackend.Interfaces;
using Microsoft.EntityFrameworkCore;
using Hl7.Fhir.Model;

namespace HealthCommBackend.Services
{
    public class LoginService : IUserRegistrationService
    {
        private UserManager<ApplicationUser> _userManager;
        private BackendDBContext _context;

        public LoginService(UserManager<ApplicationUser> userManager, BackendDBContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IdentityResult> RegisterPatientAsync(PatientRegistrationDTO patientRegistrationRequest)
        {
            var user = new ApplicationUser
            {
                UserName = patientRegistrationRequest.Email,
                Email = patientRegistrationRequest.Email,
                FirstName = patientRegistrationRequest.FirstName,
                LastName = patientRegistrationRequest.LastName,
              
            };
            var result = await _userManager.CreateAsync(user, patientRegistrationRequest.Password);
            if (result.Succeeded)
            {
                var patientProfile = new PatientProfile
                {
                    UserId = patientRegistrationRequest.Email,
                    FirstName = patientRegistrationRequest.FirstName,
                    LastName = patientRegistrationRequest.LastName,
                    DateOfBirth = patientRegistrationRequest.DateOfBirth,
                    Email = patientRegistrationRequest.Email,
                    AdministrativeGender = patientRegistrationRequest.GenderSelection,
                };
                _context.Patients.Add(patientProfile);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IdentityResult> RegisterPractitionerAsync(PractitionerRegistrationDTO practitionerRegistrationRequest)
        {
            var user = new ApplicationUser
            {
                UserName = practitionerRegistrationRequest.Email,
                Email = practitionerRegistrationRequest.Email,
                FirstName = practitionerRegistrationRequest.FirstName,
                LastName = practitionerRegistrationRequest.LastName,
            };
            var result = await _userManager.CreateAsync(user, practitionerRegistrationRequest.Password);
            if (result.Succeeded)
            {
                var practitionerProfile = new PractitionerProfile
                {
                    UserId = practitionerRegistrationRequest.Email,
                    FirstName = practitionerRegistrationRequest.FirstName,
                    LastName = practitionerRegistrationRequest.LastName,
                    DateOfBirth = practitionerRegistrationRequest.DateOfBirth,
                    Specialty = practitionerRegistrationRequest.Specialty,
                    Email = practitionerRegistrationRequest.Email,
                };
                _context.Practitioners.Add(practitionerProfile);
                await _context.SaveChangesAsync();
            }
            return result;
        }

        public async System.Threading.Tasks.Task DeletePatientAsync(string email) //FHIR also has a "Task" type
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Email == email);
            if (patient != null)
            {
                await _userManager.DeleteAsync(await _userManager.FindByEmailAsync(email));
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }
        }
    }
}

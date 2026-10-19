using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HealthCommBackend.Interfaces;
using HealthCommBackend.DTOs;
using HealthCommBackend.Services;
using Microsoft.AspNetCore.Identity;
using HealthCommBackend.Models;

namespace HealthCommBackend.Controllers
{
    public class LoginController : ControllerBase
    {
      private readonly IUserRegistrationService _registrationService;

        public LoginController(IUserRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost("register/patient")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientRegistrationDTO patientRegistrationRequest)
        {
            var result = await _registrationService.RegisterPatientAsync(patientRegistrationRequest);
            if (result.Succeeded)
            {
                return Ok(new { message = "Patient registered successfully." });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("register/practitioner")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterPractitioner([FromBody] PractitionerRegistrationDto practitionerRegistrationRequest)
        {
            var result = await _registrationService.RegisterPractitionerAsync(practitionerRegistrationRequest);
            if (result.Succeeded)
            {
                return Ok(new { message = "Practitioner registered successfully." });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}

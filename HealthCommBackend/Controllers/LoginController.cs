using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HealthCommBackend.Interfaces;
using HealthCommBackend.Services;
using Microsoft.AspNetCore.Identity;
using HealthCommBackend.Models;
using HealthCommShared.DTOs;

namespace HealthCommBackend.Controllers
{
    public class LoginController : ControllerBase
    {
      private readonly IUserRegistrationService _registrationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginController(IUserRegistrationService registrationService, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
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
        public async Task<IActionResult> RegisterPractitioner([FromBody] PractitionerRegistrationDTO practitionerRegistrationRequest)
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

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
       [FromBody] LoginRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.GivenPassword))
            {
                return BadRequest(new
                {
                    message = "Email and password are required."
                });
            }

            var user = await _userManager.FindByEmailAsync(
                request.Email.Trim());

            if (user == null)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            var result = await _signInManager
                .CheckPasswordSignInAsync(
                    user, request.GivenPassword, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                return Unauthorized(new
                {
                    message = "Invalid email or password."
                });
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                message = "Login successful.",
                userId = user.Id,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                roles = roles
            });
        }
    }
}

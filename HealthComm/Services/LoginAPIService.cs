using HealthCommShared.DTOs;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace HealthCommFrontend.Services
{
    public class LoginAPIService
    {
        private readonly HttpClient _httpClient;
        public LoginAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<LoginResultDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("identity/login?useCookies=false", loginRequest);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResultDTO>()
                ?? throw new InvalidOperationException("Failed to deserialize login result.");
            await SecureStorage.Default.SetAsync("access_token", result.AccessToken);
            await SecureStorage.Default.SetAsync("refresh_token", result.RefreshToken);
            return result;
        }

        public async Task<bool> RegisterPatientAsync(PatientRegistrationDTO patientRegistrationRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/register/patient", patientRegistrationRequest);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return true;
                case System.Net.HttpStatusCode.BadRequest:
                    // Handle bad request, possibly log the error or notify the user
                 
                    return false;
                default:
                    // Handle other status codes as needed
                    return false;
            }
        }

        public async Task<bool> RegisterPractitionerAsync(PractitionerRegistrationDTO practitionerRegistrationRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/register/practitioner", practitionerRegistrationRequest);
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.OK:
                    return true;
                case System.Net.HttpStatusCode.BadRequest:
                    return false;
                default:
                    // Handle other status codes as needed
                    return false;
            }
        }
    }
}

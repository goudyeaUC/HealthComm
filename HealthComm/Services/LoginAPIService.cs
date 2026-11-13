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

        //CHANGE THIS TO USE OUR ACTUAL DTOs
        public async Task<bool> LoginAsync(LoginRequestDTO loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/login", loginRequest);

            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterPatientAsync(PatientRegistrationDTO patientRegistrationRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/register/patient", patientRegistrationRequest);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RegisterPractitionerAsync(PractitionerRegistrationDTO practitionerRegistrationRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/register/practitioner", practitionerRegistrationRequest);
            response.EnsureSuccessStatusCode();
            return response.IsSuccessStatusCode;
        }
    }
}

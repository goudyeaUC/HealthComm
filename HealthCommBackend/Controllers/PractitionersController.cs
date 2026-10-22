using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.AspNetCore.Mvc;
using HealthCommShared.DTOs;

namespace HealthCommBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PractitionersController : ControllerBase
    {
        private readonly string _fhirBaseUrl = "https://hapi.fhir.org/baseR4";

        [HttpGet]
        public async Task<IActionResult> GetPractitioners()
        {
            var practitioners = new List<PractitionerRegistrationDTO>();

            try
            {
                var client = new FhirClient(_fhirBaseUrl);

                Bundle results = await client.SearchAsync<Practitioner>(
                    new[] { "_count=10" });

                if (results?.Entry == null)
                    return Ok(practitioners);

                foreach (var entry in results.Entry)
                {
                    try
                    {
                        string? fullUrl = entry.FullUrl;

                        if (string.IsNullOrEmpty(fullUrl))
                            continue;

                        string practitionerId =
                            fullUrl.Split('/').Last();

                        Practitioner? practitioner =
                            await client.ReadAsync<Practitioner>(
                                $"Practitioner/{practitionerId}");

                        string name =
                            practitioner?.Name?.FirstOrDefault()?.ToString()
                            ?? "Unknown";

                        practitioners.Add(new PractitionerRegistrationDTO
                        {
                            Id = practitionerId,
                            Name = name
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            $"Error reading practitioner: {ex.Message}");
                    }
                }

                return Ok(practitioners);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.AspNetCore.Mvc;

namespace HealthCommBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController
    {
        private readonly string _fhirBaseUrl = "https://hapi.fhir.org/baseR4";
        public async Task<List<Slot>> GetAvailableSlots(
    string practitionerId,
    DateTime date)
        {
            var client = new FhirClient(_fhirBaseUrl);

            string dateString = date.ToString("yyyy-MM-dd");

            Bundle results = await client.SearchAsync<Slot>(
                new[]
                {
            "status=free",
            $"start=ge{dateString}T00:00:00",
            $"start=lt{date.AddDays(1):yyyy-MM-dd}T00:00:00",
            $"schedule.actor=Practitioner/{practitionerId}",
            "_count=100"
                });

            return results.Entry
                .Where(e => e.Resource is Slot)
                .Select(e => (Slot)e.Resource)
                .ToList();
        }
    }
}

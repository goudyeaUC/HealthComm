using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Microsoft.AspNetCore.Mvc;
using HealthCommShared.DTOs;

namespace HealthCommBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly string _fhirBaseUrl = "https://hapi.fhir.org/baseR4";

        [HttpGet("sample")]
        public  IActionResult GetSampleAppointments()
        {
            var appointments = new List<AppointmentDTO>
                {
                    new AppointmentDTO
                    {
                        Id = 1,
                        PatientProfileId = 1,
                        PractitionerProfileId = 1,
                        StartDateTime = DateTime.UtcNow.AddDays(2),
                        EndDateTime = DateTime.UtcNow.AddDays(2).AddMinutes(30),
                        VisitType = "General Consultation",
                        Status = "booked",
                        Description = "Routine health consultation",
                        CreatedAt = DateTime.UtcNow
                    },
                    new AppointmentDTO
                    {
                        Id = 2,
                        PatientProfileId = 1,
                        PractitionerProfileId = 1,
                        StartDateTime = DateTime.UtcNow.AddDays(7),
                        EndDateTime = DateTime.UtcNow.AddDays(7).AddMinutes(15),
                        VisitType = "Follow-up",
                        Status = "proposed",
                        Description = "Follow-up appointment",
                        CreatedAt = DateTime.UtcNow
                    }
                };

            return Ok(appointments);
        }
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

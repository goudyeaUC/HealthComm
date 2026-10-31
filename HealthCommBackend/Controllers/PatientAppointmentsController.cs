namespace HealthCommBackend.Controllers
{
    using HealthCommBackend.Services;
    using HealthCommShared.DTOs;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class PatientAppointmentsController : ControllerBase
    {
        private readonly PatientAppointmentService _service;

        public PatientAppointmentsController(
            PatientAppointmentService service)
        {
            _service = service;
        }

        // GET: api/PatientAppointments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _service.GetAllAsync();
            return Ok(appointments);
        }

        // GET: api/PatientAppointments/1
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var appointment = await _service.GetByIdAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointment);
        }

        [HttpGet("practitioners")]
        public async Task<IActionResult> GetAllPractitioners()
        {
            var practitioners = await _service.GetAllPractitionersAsync();

            return Ok(practitioners);
        }

        [HttpGet("patients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _service.GetAllPatientsAsync();

            return Ok(patients);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatientAppointment(
    [FromBody] CreatePatientAppointmentRequest request)
        {
            if (request.PatientProfileId <= 0 ||
                request.PractitionerProfileId <= 0)
            {
                return BadRequest(
                    "Valid patient and practitioner IDs are required.");
            }

            if (request.StartDateTime == default)
            {
                return BadRequest("A valid appointment date and time are required.");
            }

            if (string.IsNullOrWhiteSpace(request.VisitType))
            {
                return BadRequest("Visit type is required.");
            }

            if (request.EndDateTime.HasValue &&
                request.EndDateTime.Value <= request.StartDateTime)
            {
                return BadRequest(
                    "Appointment end time must be after the start time.");
            }

            try
            {
                var appointment =
                    await _service.CreatePatientAppointmentAsync(request);

                return Created(
                    $"/api/PatientAppointments/{appointment.Id}",
                    appointment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("patient/{patientId}/practitioner/{practitionerId}")]
        public async Task<IActionResult> GetAppointmentsByPatientAndPractitioner(
            int patientId,
            int practitionerId)
        {
            if (patientId <= 0 || practitionerId <= 0)
            {
                return BadRequest(
                    "Valid patient ID and practitioner ID are required.");
            }

            var appointments =
                await _service.GetAppointmentsByPatientAndPractitionerAsync(
                    patientId,
                    practitionerId);

            return Ok(appointments);
        }
    
    }
}
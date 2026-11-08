namespace HealthCommBackend.Controllers
{
    using HealthCommBackend.Services;
    using HealthCommShared.DTOs;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly PrescriptionService _service;

        public PrescriptionsController(PrescriptionService service)
        {
            _service = service;
        }

        // GET: api/Prescriptions/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var prescription = await _service.GetPrescriptionByIdAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }

            return Ok(PrescriptionService.ToDto(prescription));
        }

        // GET: api/Prescriptions/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(string patientId)
        {
            var list = await _service.GetPrescriptionsByPatientIdAsync(patientId);
            return Ok(list.Select(PrescriptionService.ToDto));
        }

        // GET: api/Prescriptions/patient?email=someone@example.com
        [HttpGet("patient")]
        public async Task<IActionResult> GetByPatientEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required.");
            }

            var list = await _service.GetPrescriptionsByPatientEmailAsync(email);
            return Ok(list.Select(PrescriptionService.ToDto));
        }

        // GET: api/Prescriptions/prescriber/{prescriberId}
        [HttpGet("prescriber/{prescriberId}")]
        public async Task<IActionResult> GetByPrescriber(string prescriberId)
        {
            var list = await _service.GetPrescriptionsByPrescriberIdAsync(prescriberId);
            return Ok(list.Select(PrescriptionService.ToDto));
        }

        // POST: api/Prescriptions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePrescriptionRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.PatientId) ||
                string.IsNullOrWhiteSpace(request.PrescriberId))
            {
                return BadRequest("Patient and prescriber IDs are required.");
            }

            if (string.IsNullOrWhiteSpace(request.MedicationName) ||
                string.IsNullOrWhiteSpace(request.Instructions))
            {
                return BadRequest("Medication name and instructions are required.");
            }

            if (request.RefillsRemaining < 0)
            {
                return BadRequest("Refills cannot be negative.");
            }

            if (request.ExpirationDate.HasValue &&
                request.ExpirationDate.Value <= DateTime.UtcNow)
            {
                return BadRequest("Expiration date must be in the future.");
            }

            try
            {
                var created = await _service.CreatePrescriptionAsync(request);
                var dto = PrescriptionService.ToDto(created);
                return Created($"/api/Prescriptions/{dto.Id}", dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Prescriptions/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PrescriptionDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.MedicationName) ||
                string.IsNullOrWhiteSpace(dto.Instructions))
            {
                return BadRequest("Medication name and instructions are required.");
            }

            var updated = await _service.UpdatePrescriptionAsync(id, dto);
            if (updated == null)
            {
                return NotFound();
            }

            return Ok(PrescriptionService.ToDto(updated));
        }

        // DELETE: api/Prescriptions/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeletePrescriptionAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}

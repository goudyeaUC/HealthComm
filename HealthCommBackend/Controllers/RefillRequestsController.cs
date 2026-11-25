namespace HealthCommBackend.Controllers
{
    using HealthCommBackend.Services;
    using HealthCommShared.DTOs;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class RefillRequestsController : ControllerBase
    {
        private readonly RefillRequestService _service;

        public RefillRequestsController(RefillRequestService service)
        {
            _service = service;
        }

        // GET: api/RefillRequests/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(string patientId)
        {
            var list = await _service.GetByPatientIdAsync(patientId);
            return Ok(list.Select(RefillRequestService.ToDto));
        }

        // GET: api/RefillRequests/prescriber/{prescriberId}?status=requested
        [HttpGet("prescriber/{prescriberId}")]
        public async Task<IActionResult> GetByPrescriber(string prescriberId, [FromQuery] string? status)
        {
            var list = await _service.GetByPrescriberIdAsync(prescriberId, status?.ToLowerInvariant());
            return Ok(list.Select(RefillRequestService.ToDto));
        }

        // POST: api/RefillRequests
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRefillRequest request)
        {
            if (request.PrescriptionId <= 0 || string.IsNullOrWhiteSpace(request.PatientId))
            {
                return BadRequest("Prescription and patient IDs are required.");
            }

            try
            {
                var created = await _service.CreateAsync(request);
                var dto = RefillRequestService.ToDto(created);
                return Created($"/api/RefillRequests/{dto.Id}", dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/RefillRequests/5/review
        [HttpPut("{id:int}/review")]
        public async Task<IActionResult> Review(int id, [FromBody] ReviewRefillRequest review)
        {
            if (string.IsNullOrWhiteSpace(review.ReviewerId))
            {
                return BadRequest("Reviewer ID is required.");
            }

            try
            {
                var reviewed = await _service.ReviewAsync(id, review);
                if (reviewed == null)
                {
                    return NotFound();
                }

                return Ok(RefillRequestService.ToDto(reviewed));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

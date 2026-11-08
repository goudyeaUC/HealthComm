using HealthCommBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthCommBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationRecordsController : ControllerBase
    {

        private readonly MedicationRecordService _service;


        public MedicationRecordsController(
            MedicationRecordService service)
        {
            _service = service;
        }

        // GET: api/MedicationRecords
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var medicationRecords = await _service.GetAllAsync();
            return Ok(medicationRecords);
        }
    }
}

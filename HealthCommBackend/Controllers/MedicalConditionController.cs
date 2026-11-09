using HealthCommBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace HealthCommBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalConditionController : ControllerBase
    {

        private readonly MedicationConditionService _service;


        public MedicalConditionController(
            MedicationConditionService service)
        {
            _service = service;
        }

        // GET: api/MedicalConditionRecords
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var medicationRecords = await _service.GetAllAsync();
            return Ok(medicationRecords);
        }
    }
}

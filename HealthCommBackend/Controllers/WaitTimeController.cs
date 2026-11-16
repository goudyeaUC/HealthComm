using Microsoft.AspNetCore.Mvc;
using HealthCommBackend.Services;
using HealthCommShared.DTOs;

namespace HealthCommBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaitTimeController : ControllerBase
    {
        private readonly WaitTimeService _waitTimeService;

        public WaitTimeController(WaitTimeService waitTimeService)
        {
            _waitTimeService = waitTimeService;
        }

        [HttpGet]
        public ActionResult<List<HospitalWaitTimeDTO>> GetHospitalWaitTimes()
        {
            var waitTimes = _waitTimeService.GetHospitalWaitTimes();

            return Ok(waitTimes);
        }
    }
}

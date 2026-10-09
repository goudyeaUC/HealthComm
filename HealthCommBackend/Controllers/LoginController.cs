using Hl7.Fhir.Rest;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace HealthCommBackend.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {var client =new FhirClient("https://hapi.fhir.org/baseR4");
            return View();
        }
    }
}

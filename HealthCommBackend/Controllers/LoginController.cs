using Microsoft.AspNetCore.Mvc;

namespace HealthCommBackend.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

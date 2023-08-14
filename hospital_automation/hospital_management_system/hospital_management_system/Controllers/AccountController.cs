using Microsoft.AspNetCore.Mvc;

namespace hospital_management_system.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

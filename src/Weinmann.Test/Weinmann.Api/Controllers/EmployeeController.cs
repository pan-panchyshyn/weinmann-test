using Microsoft.AspNetCore.Mvc;

namespace Weinmann.Api.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

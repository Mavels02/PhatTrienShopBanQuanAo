using Microsoft.AspNetCore.Mvc;

namespace Assignment.Controllers
{
    public class TinTucController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

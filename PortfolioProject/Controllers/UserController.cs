using Microsoft.AspNetCore.Mvc;

namespace PortfolioProject.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

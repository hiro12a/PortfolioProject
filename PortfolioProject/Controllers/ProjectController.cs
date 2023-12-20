using Microsoft.AspNetCore.Mvc;
using PortfolioProject.Data;
using PortfolioProject.Models;

namespace PortfolioProject.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProjectController(ApplicationDbContext db) 
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Projects> projects = _db.Projects.ToList();
            return View(projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Projects projects) 
        {
            
            _db.Projects.Add(projects);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PortfolioProject.Models;
using PortfolioProject.Models.VIewModel;
using System.Diagnostics;

namespace PortfolioProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofwork _unitofwork;

        public HomeController(ILogger<HomeController> logger, IUnitofwork unitofwork)
        {
            _logger = logger;
            _unitofwork = unitofwork;
        }

        public IActionResult Index()
        {
            HomeVM home = new()
            {
                Project = _unitofwork.Project.GetAll().ToList(),
                Skill = _unitofwork.Skill.GetAll().ToList(),
            };

            return View(home);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}

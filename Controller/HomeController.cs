using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using PortfolioProject.Models.VIewModel;
using PortfolioProject.Utility;

namespace PortfolioProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitofwork _unitofwork;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IUnitofwork unitofwork, IEmailSender emailSender)
        {
            _logger = logger;
            _unitofwork = unitofwork;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            HomeVM home = new()
            {
                // Shows the lastest item first
                Project = _unitofwork.Project.GetAll().ToList().OrderByDescending(u=>u.Id),
                Skill = _unitofwork.Skill.GetAll().ToList().OrderByDescending(u=>u.Id),
                Others = _unitofwork.Other.GetAll().ToList()
            };

            return View(home);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail()
        {
            var name = Request.Form["name"];
            var email = Request.Form["email"];
            var subject = "Job Scouting";
            var messageBody = "Name: " + name + "\nEmail: " + email + "\nMessage: " + Request.Form["message"];
            var to = "ythom87@gmail.com"; // Send the email to me 
            
            await _emailSender.SendEmailAsync(to, subject, messageBody);
            TempData["success"] = "Email Sent Successfully";
            return RedirectToAction("Index","Home");
        }

    }
}

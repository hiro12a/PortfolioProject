using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PortfolioProject.Data;
using PortfolioProject.Models;
using System.ComponentModel;

namespace PortfolioProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SkillsController : Controller
    {
        private IUnitofwork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment; // Allows us to access the wwwroot folder
        public SkillsController(IUnitofwork unitofwork, IWebHostEnvironment webHostEnvironment)
        {
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            // Makes sure the newest obj is on the top
            IEnumerable<Skills> skills = _unitofwork.Skill.GetAll().OrderByDescending(u=>u.Id);
            return View(skills);
        }

        public IActionResult Upsert(int? id)
        {
            List<string> types = new List<string>()
            { 
                "Skill",
                "Certification"
            };

            ViewBag.Types = new SelectList(types);

            // Check whether to create or update the skill/certification by id
            if (id == 0 || id == null)
            {
                // id is non existent so it means we need to create
                return View(new Skills());
            }
            else
            {
                // it returns an id so it means we update the skill
                Skills skill = _unitofwork.Skill.Get(x => x.Id == id);
                return View(skill);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Skills skills)
        {

            if (skills.Pictures != null)
            {
                // Rename the uploaded image to a random one
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(skills.Pictures.FileName);
                // Get the image path on where to upload the image
                string filepPath = Path.Combine(_webHostEnvironment.WebRootPath, @"images/skillCerts");

                // Check if there is already an iamge
                if (!string.IsNullOrEmpty(skills.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, skills.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }
                // Uploads the image
                using (var filestream = new FileStream(Path.Combine(filepPath, fileName), FileMode.Create))
                {
                    skills.Pictures.CopyTo(filestream);
                }

                skills.ImageUrl = @"\images\skillCerts\" + fileName;

            }
            _unitofwork.Skill.Update(skills);
            _unitofwork.Save();
            // Allows us to refresh the page with its field populated
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Skills obj)
        {
            Skills? objToDelete = _unitofwork.Skill.Get(u => u.Id == obj.Id);

            if (objToDelete != null)
            {
                if (!string.IsNullOrEmpty(objToDelete.ImageUrl))
                {
                    // Get old image path 
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objToDelete.ImageUrl.TrimStart('\\'));

                    // Delete the image
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Delete Skill
                _unitofwork.Skill.Remove(objToDelete);
                _unitofwork.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioProject.Data;
using PortfolioProject.Models;

namespace PortfolioProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProjectController : Controller
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProjectController(IUnitofwork unitofwork, IWebHostEnvironment webHostEnvironment) 
        {
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Projects> projects = _unitofwork.Project.GetAll();
            return View(projects);
        }

        public IActionResult Upsert(int? id)
        {
            if(id == null || id == 0)
            {
                return View(new Projects());
            }
            else
            {
                Projects projects = _unitofwork.Project.Get(u => u.Id == id);
                return View(projects);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Projects projects) 
        {
            if (ModelState.IsValid)
            {
                // Make sure the user has uploaded an image
                if(projects.Image != null)
                {
                    // Change the name of the image
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(projects.Image.FileName);
                    // Get the image path 
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images/projects");

                    // Delete old image if there is an old image
                    if (!string.IsNullOrEmpty(projects.ImageUrl))
                    {
                        // Find old image path 
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, projects.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Upload the image
                    using (var filestream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        projects.Image.CopyTo(filestream);
                    }

                    // Declare the image url
                    projects.ImageUrl = @"\images\projects\" + fileName;
                }

                _unitofwork.Project.Update(projects);
                _unitofwork.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Projects obj)
        {
            Projects? objToDelete = _unitofwork.Project.Get(u=>u.Id == obj.Id);

            if(objToDelete != null)
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

                // Delete project
                _unitofwork.Project.Remove(objToDelete);
                _unitofwork.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

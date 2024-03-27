using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioProject.Data;
using PortfolioProject.Models;

namespace PortfolioProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OthersController : Controller
    {
        private readonly IUnitofwork _unitofwork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public OthersController(IUnitofwork unitofwork, IWebHostEnvironment webHostEnvironment) 
        {
            _unitofwork = unitofwork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<Others> obj = _unitofwork.Other.GetAll();
            return View(obj);
        }

        public IActionResult Upsert(int? id)
        {
            if(id == null || id == 0)
            {
                return View(new Others());
            }
            else
            {
                Others obj = _unitofwork.Other.Get(u => u.Id == id);
                return View(obj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Others obj) 
        {
            if (ModelState.IsValid)
            {
                // Make sure the user has uploaded an file
                if(obj.OtherFiles != null)
                {
                    // Change the name of the file
                    string fileName = obj.OtherFiles.FileName;
                    // Get the file path 
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images/others");

                    // Delete old file if there is an old image
                    if (!string.IsNullOrEmpty(obj.File))
                    {
                        // Find old file path 
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.File.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Upload the image
                    using (var filestream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        obj.OtherFiles.CopyTo(filestream);
                    }

                    // Declare the image url
                    obj.File = @"\images\others\" + fileName;
                }

                _unitofwork.Other.Update(obj);
                _unitofwork.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Others obj)
        {
            Others? objToDelete = _unitofwork.Other.Get(u=>u.Id == obj.Id);

            if(objToDelete != null)
            {
                if (!string.IsNullOrEmpty(objToDelete.File))
                {
                    // Get old image path 
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objToDelete.File.TrimStart('\\'));

                    // Delete the image
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Delete project
                _unitofwork.Other.Remove(objToDelete);
                _unitofwork.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

using DataAccess.Repository.IRepository;
using PortfolioProject.Models.VIewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Utility;

namespace PortfolioProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUnitofwork _unitofwork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(IUnitofwork unitofwork, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _unitofwork = unitofwork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Login(string returnUrl=null)
        {
            returnUrl ??= Url.Content("~/");

            LoginVM loginVM = new()
            {
                RedirectUrl = returnUrl
            };

            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // For redirecting users to homepage
                    if (string.IsNullOrEmpty(login.RedirectUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return LocalRedirect(login.RedirectUrl); // Makes it so it doesn't redirect to a malicious website
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Inavlid Login Attempt");
            }

            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = SD.Role_Admin)]
        public IActionResult Register()
        {
            // If admin role doesn't exist, create the roles
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).Wait();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User)).Wait();
            }

            // To display value in dropdown for roles
            RegisterVM registerVM = new()
            {
                RoleList = _roleManager.Roles.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name
                })
            };

            return View(registerVM);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            // Get the user details as user fills it
            IdentityUser user = new()
            {
                Email = registerVM.Email,
                NormalizedEmail = registerVM.Email.ToUpper(),
                EmailConfirmed = true,
                UserName = registerVM.Email
            };

            // Create the user
            var result = _userManager.CreateAsync(user, registerVM.Password).GetAwaiter().GetResult();

            if (result.Succeeded)
            {
                // If the user selected a role, choose that role
                if (!string.IsNullOrEmpty(registerVM.Role))
                {
                    await _userManager.AddToRoleAsync(user, registerVM.Role);
                }
                else
                {
                    // Since user did not select role, assign the role of user to them
                    await _userManager.AddToRoleAsync(user, SD.Role_User);
                }

                await _signInManager.SignInAsync(user, isPersistent: false);

                // For redirecting users to homepage
                if (string.IsNullOrEmpty(registerVM.RedirectUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return LocalRedirect(registerVM.RedirectUrl); // Makes it so it doesn't redirect to a malicious website
                }
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            registerVM.RoleList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            });

            return View(registerVM);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

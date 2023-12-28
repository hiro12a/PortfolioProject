using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioProject.Models.VIewModel
{
    public class RegisterVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)] // Hides the password
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)] // Hides the password
        [Compare(nameof(Password))] // Makes sure password and confirm password matches
        public string ConfirmPassword { get; set; }
        public bool RememberMe { get; set; }
        public string? RedirectUrl { get; set; }

        // For Roles
        public string? Role { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}

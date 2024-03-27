using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioProject.Models.VIewModel
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)] // Hides the password
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string? RedirectUrl {  get; set; } 
    }
}

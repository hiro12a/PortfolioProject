using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PortfolioProject.Models
{
    public class Skills
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? name { get; set; }
        [Required]
        public string? type { get; set; } // Select between certification or skill

        [NotMapped]
        public IFormFile? Pictures { get; set; }
        public string? ImageUrl { get; set; }
    }
}

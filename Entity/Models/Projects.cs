using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortfolioProject.Models
{
    public class Projects
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }

        [Required]
        [DisplayName("Website Link")]
        public string? Link { get; set; }

        [Required]
        public string? Tools { get; set; }

        [Required]
        public string? Summary { get; set; }

        // Images
        [NotMapped]
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
    }
}

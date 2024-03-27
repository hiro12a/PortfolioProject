using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioProject.Models
{
    public class Others
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public string? Links { get; set; }

        [NotMapped]
        [DisplayName("Other Files")]
        public IFormFile? OtherFiles { get; set; }
        public string? File { get; set; }
    }
}

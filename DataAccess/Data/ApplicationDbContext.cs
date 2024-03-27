
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortfolioProject.Models;

namespace PortfolioProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        public DbSet<Projects> Projects { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<Others> Others { get; set; }
    }
}

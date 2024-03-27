using DataAccess.Repository.IRepository;
using PortfolioProject.Data;
using PortfolioProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProjectRepository : Repository<Projects>, IProjectRepository
    {
        private ApplicationDbContext _db;

        public ProjectRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Projects obj)
        {
            _db.Projects.Update(obj);
        }
    }
}

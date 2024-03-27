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
    public class SkillRepository : Repository<Skills>, ISkillRepository
    {
        private ApplicationDbContext _db;

        public SkillRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Skills skill)
        {
            _db.Skills.Update(skill);
        }
    }
}

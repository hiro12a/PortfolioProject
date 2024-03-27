using DataAccess.Repository.IRepository;
using PortfolioProject.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitofWork : IUnitofwork
    {
        private ApplicationDbContext _db;
        public ISkillRepository Skill { get; private set; }
        public IProjectRepository Project { get; private set; }
        public IOthersRepository Other { get; private set; }

        public UnitofWork(ApplicationDbContext db)
        {
            _db = db;
            Skill = new SkillRepository(_db);  
            Project = new ProjectRepository(_db);
            Other = new OthersRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

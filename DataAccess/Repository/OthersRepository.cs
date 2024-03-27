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
    public class OthersRepository : Repository<Others>, IOthersRepository
    {
        private ApplicationDbContext _db;

        public OthersRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Others obj)
        {
            _db.Others.Update(obj);
        }
    }
}

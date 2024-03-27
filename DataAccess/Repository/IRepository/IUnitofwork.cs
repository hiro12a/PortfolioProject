using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitofwork
    {
        ISkillRepository Skill { get; }
        IProjectRepository Project { get; }
        IOthersRepository Other { get; }

        void Save(); // Saves here instead of directly into the database
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioProject.Models.VIewModel
{
    public class HomeVM
    {
        public IEnumerable<Projects> Project { get; set; }
        public IEnumerable<Skills> Skill { get; set; }
        public IEnumerable<Others> Others { get; set; }
    }
}

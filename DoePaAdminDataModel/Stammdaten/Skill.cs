using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Skill
    {
        public int SkillID { get; set; }
        public string SkillName { get; set; }  
        public Skill? ParentSkill { get; set; }
        public ICollection<Projekt> Projekte { get; set; }
    }
}

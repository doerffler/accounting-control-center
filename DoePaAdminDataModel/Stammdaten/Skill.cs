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

        public Skill ParentSkill { get; set; }

        public List<Skill> ChildSkills { get; set; }

        public List<Projekt> Projekte { get; set; }

        public Skill()
        {
            Projekte = new List<Projekt>();
            ChildSkills = new List<Skill>();
        }
    }
}

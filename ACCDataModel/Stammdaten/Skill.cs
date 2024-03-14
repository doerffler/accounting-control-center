using System.Collections.Generic;

namespace ACCDataModel.Stammdaten
{
    public class Skill
    {

        public int SkillID { get; set; }

        public string SkillName { get; set; }

        public int? ParentSkillID { get; set; }

        public Skill ParentSkill { get; set; }

        public ICollection<Skill> ChildSkills { get; set; }

        public ICollection<Projekt> Projekte { get; set; }

        public Skill()
        {
            Projekte = new List<Projekt>();
            ChildSkills = new List<Skill>();
        }
    }
}

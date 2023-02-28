using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.Stammdaten
{
    public class Skill
    {
        private ILazyLoader LazyLoader { get; set; }

        public int SkillID { get; set; }

        public string SkillName { get; set; }

        public Skill ParentSkill { get; set; }

        public List<Skill> ChildSkills { get; set; }

        public List<Projekt> Projekte { get; set; }

        public Skill(ILazyLoader lazyLoader): this()
        {
            LazyLoader = lazyLoader;
        }

        public Skill()
        {
            Projekte = new List<Projekt>();
            ChildSkills = new List<Skill>();
        }
    }
}

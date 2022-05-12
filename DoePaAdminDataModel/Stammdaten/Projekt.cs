using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Projekt
    {

        public int ProjektID { get; set; }

        public string Projektname { get; set; }
        public DateTime Projektstart { get; set; }  
        public DateTime Projektende { get; set; }
        public ICollection<Auftrag> ZugehoerigeAuftraege { get; set; }      
        public ICollection<Skill> Skills { get; set; }
    }
}

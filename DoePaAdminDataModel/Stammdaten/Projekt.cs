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
        public List<Auftrag> ZugehoerigeAuftraege { get; set; }
        public List<Skill> Skills { get; set; }
        public Projekt()
        {

        }
    }
}

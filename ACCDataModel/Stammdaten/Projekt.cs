using System;
using System.Collections.Generic;

namespace ACCDataModel.Stammdaten
{
    public class Projekt
    {

        public int ProjektID { get; set; }

        public string Projektname { get; set; }

        public DateTime Projektstart { get; set; }  

        public DateTime Projektende { get; set; }

        public ICollection<Auftrag> ZugehoerigeAuftraege { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public int? RechnungsempfaengerID { get; set; }

        public Debitor Rechnungsempfaenger { get; set; }

        public Projekt()
        {
            ZugehoerigeAuftraege = new List<Auftrag>();
            Skills = new List<Skill>();
        }
    }
}

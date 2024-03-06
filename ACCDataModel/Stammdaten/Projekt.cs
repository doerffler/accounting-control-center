using System;
using System.Collections.Generic;
using System.Text;

namespace ACCDataModel.Stammdaten
{
    public class Projekt
    {

        public int ProjektID { get; set; }

        public string Projektname { get; set; }

        public DateTime Projektstart { get; set; }  

        public DateTime Projektende { get; set; }

        public List<Auftrag> ZugehoerigeAuftraege { get; set; }

        public List<Skill> Skills { get; set; }

        public int? RechnungsempfaengerID { get; set; }

        public Debitor Rechnungsempfaenger { get; set; }

        public Projekt()
        {
            ZugehoerigeAuftraege = new();
            Skills = new();
        }
    }
}

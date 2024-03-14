using ACCDataModel.DPApp;
using System;
using System.Collections.Generic;

namespace ACCDataModel.Stammdaten
{
    public class Geschaeftsjahr
    {

        public int GeschaeftsjahrID { get; set; }

        public string Name { get; set; }

        public DateTime DatumVon { get; set; }

        public DateTime DatumBis { get; set; }

        public ICollection<Feiertag> Feiertage { get; set; }

        public ICollection<Auftrag> Auftraege { get; set; }

        public string Rechnungsprefix { get; set; }

        public Geschaeftsjahr()
        {
            Feiertage = new List<Feiertag>();
            Auftraege = new List<Auftrag>();
        }
    }
}

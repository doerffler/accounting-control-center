using System;
using System.Collections.Generic;
using System.Text;

namespace ACCDataModel.Stammdaten
{
    public class Kostenstelle
    {

        public int KostenstelleID { get; set; }

        public int KostenstellenNummer { get; set; }

        public string Kostenstellenbezeichnung { get; set; }
                
        public int? ZugehoerigeKostenstellenartId { get; set; }

        public Kostenstellenart ZugehoerigeKostenstellenart { get; set; }

        public List<Leistungsnachweis> Leistungsnachweise { get; set; }

        public List<Kostenstelle> UebergeordneteKostenstellen { get; set; }

        public List<Kostenstelle> UntergeordneteKostenstellen { get; set; }

        public Kostenstelle ()
        {
            UebergeordneteKostenstellen = new List<Kostenstelle> ();
            UntergeordneteKostenstellen = new List<Kostenstelle> ();
            Leistungsnachweise = new List<Leistungsnachweis> ();
        }

    }
}

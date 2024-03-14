using System.Collections.Generic;

namespace ACCDataModel.Stammdaten
{
    public class Kostenstelle
    {

        public int KostenstelleID { get; set; }

        public int KostenstellenNummer { get; set; }

        public string Kostenstellenbezeichnung { get; set; }
                
        public int? ZugehoerigeKostenstellenartId { get; set; }

        public Kostenstellenart ZugehoerigeKostenstellenart { get; set; }

        public ICollection<Leistungsnachweis> Leistungsnachweise { get; set; }

        public ICollection<Kostenstelle> UebergeordneteKostenstellen { get; set; }

        public ICollection<Kostenstelle> UntergeordneteKostenstellen { get; set; }

        public Kostenstelle ()
        {
            UebergeordneteKostenstellen = new List<Kostenstelle> ();
            UntergeordneteKostenstellen = new List<Kostenstelle> ();
            Leistungsnachweise = new List<Leistungsnachweis> ();
        }

    }
}

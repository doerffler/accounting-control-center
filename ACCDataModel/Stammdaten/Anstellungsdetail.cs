using System;

namespace ACCDataModel.Stammdaten
{
    public class Anstellungsdetail
    {

        public int AnstellungsdetailID { get; set; }

        public int? ZugehoerigerMitarbeiterID { get; set; }

        public Mitarbeiter ZugehoerigerMitarbeiter { get; set; }

        public DateTime GueltigAb { get; set; }

        public decimal? Monatsgehalt { get; set; }

        public int AnzahlMonatsgehaelter { get; set; }

        public int AnzahlArbeitsstunden { get; set; }

        public int? ZugehoerigeTaetigkeitID { get; set; }

        public Taetigkeit ZugehoerigeTaetigkeit { get; set; }

        public bool IstGekuendigt { get; set; }

    }
}
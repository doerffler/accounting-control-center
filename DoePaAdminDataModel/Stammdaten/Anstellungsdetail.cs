using System;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Anstellungsdetail
    {

        public int AnstellungsdetailID { get; set; }

        public Mitarbeiter ZugehoerigerMitarbeiter { get; set; }

        public DateTime GueltigAb { get; set; }

        public decimal? Monatsgehalt { get; set; }

        public int AnzahlMonatsgehaelter { get; set; }

        public int AnzahlArbeitsstunden { get; set; }

        public Taetigkeit ZugehoerigeTaetigkeit { get; set; }

        public bool IstGekuendigt { get; set; }

    }
}
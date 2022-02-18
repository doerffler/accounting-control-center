using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Mitarbeiter
    {

        public int MitarbeiterID { get; set; }

        public int MitarbeiterNummer { get; set; }

        public DateTime GueltigAb { get; set; }

        public string Vorname { get; set; }

        public string Zuname { get; set; }

        public string Nachname { get; set; }

        public int? Personalnummer { get; set; }

        public Kostenstelle ZugehoerigeKostenstelle { get; set; }

        public Bankverbindung ZugehoerigeBankverbindung { get; set; }

        public string Anrede { get; set; }

        public string Titel { get; set; }

        public Adresse ZugehoerigeAdresse { get; set; }

        public decimal? Monatsgehalt { get; set; }

        public int AnzahlMonatsgehaelter { get; set; }

        public int AnzahlArbeitsstunden { get; set; }

        public Taetigkeit ZugehoerigeTaetigkeit { get; set; }

        public DateTime Geburtsdatum { get; set; }

        public string Kuerzel { get; set; }

        public bool IstGekuendigt { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public abstract class Geschaeftspartner
    {

        public int GeschaeftspartnerID { get; set; }

        public string Vorname { get; set; }

        public string Nachname { get; set; }

        public string Zuname { get; set; }

        public string Anrede { get; set; }

        public string Titel { get; set; }

        public string Position { get; set; }

        public Adresse ZugehoerigeAdresse { get; set; }

        public Kunde ZugehoerigerKunde { get; set; }

        public DateTime GueltigAb { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Mitarbeiter
    {

        public int MitarbeiterID { get; set; }

        public string Kuerzel { get; set; }

        public int? PersonalnummerDatev { get; set; }

        public Kostenstelle ZugehoerigeKostenstelle { get; set; }

        public string Anrede { get; set; }

        public string Titel { get; set; }

        public string Vorname { get; set; }

        public string Zuname { get; set; }

        public string Nachname { get; set; }

        public DateTime Geburtsdatum { get; set; }

        public string IBAN { get; set; }

        public Adresse ZugehoerigeAdresse { get; set; }

        public List<Anstellungsdetail> Anstellungshistorie { get; set; }

    }
}

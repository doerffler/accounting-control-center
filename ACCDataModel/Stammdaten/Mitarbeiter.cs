using System;
using System.Collections.Generic;
using System.Text;

namespace ACCDataModel.Stammdaten
{
    public class Mitarbeiter : Person
    {

        public int MitarbeiterID { get; set; }

        public string Kuerzel { get; set; }

        public string Email { get; set; }

        public int? PersonalnummerDatev { get; set; }

        public Kostenstelle ZugehoerigeKostenstelle { get; set; }

        public DateTime Geburtsdatum { get; set; }

        public string IBAN { get; set; }

        public Adresse ZugehoerigeAdresse { get; set; }

        public List<Anstellungsdetail> Anstellungshistorie { get; set; }

        public Mitarbeiter()
        {
            Anstellungshistorie = new List<Anstellungsdetail>();
        }

    }
}

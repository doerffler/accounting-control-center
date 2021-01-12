using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Adresse
    {

        public int AdresseID { get; set; }

        public string Strasse { get; set; }

        public string Hausnummer { get; set; }

        public string Postfach { get; set; }

        public Postleitzahl ZugehoerigePostleitzahl { get; set; }

        public Adresse()
        {

        }

    }
}

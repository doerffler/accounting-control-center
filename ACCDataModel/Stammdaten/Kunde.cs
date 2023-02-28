using System;
using System.Collections.Generic;
using System.Text;

namespace ACCDataModel.Stammdaten
{
    public class Kunde
    {
        
        public int KundeID { get; set; }

        public string Langname { get; set; }

        public string Kundenname { get; set; }

        public List<Debitor> Rechnungsempfaenger { get; set; }

        public Kunde()
        {
            Rechnungsempfaenger = new();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Kunde
    {
        
        public int KundeID { get; set; }

        public string Kundenname { get; set; }

        public List<Debitor> Rechnungsempfaenger { get; set; }

        public Kunde()
        {
            Rechnungsempfaenger = new();
        }

    }
}

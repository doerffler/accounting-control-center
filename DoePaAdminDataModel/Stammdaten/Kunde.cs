using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Kunde
    {
        
        public int KundeID { get; set; }

        public string Kundenname { get; set; }

        public List<Auftrag> Auftraege { get; set; }

        public Kunde()
        {
            Auftraege = new List<Auftrag>();
        }

    }
}

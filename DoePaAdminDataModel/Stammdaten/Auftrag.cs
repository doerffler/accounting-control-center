using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Auftrag
    {

        public int AuftragID { get; set; }

        public string Auftragsname { get; set; }

        public Kunde Kunde { get; set; }

        public List<Auftragsposition> Auftragspositionen {get; set;}

    }
}

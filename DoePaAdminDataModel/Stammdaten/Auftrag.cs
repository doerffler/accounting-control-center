using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Auftrag
    {
        public DateTime? Auftragsbeginn { get; set; }

        public DateTime? Auftragsende { get; set; }  

        public DateTime Auftragsdatum { get; set; }

        public int AuftragID { get; set; }

        public string Auftragsname { get; set; }

        public List<Auftragsposition> Auftragspositionen { get; set; }

        public Kunde Kunde { get; set; }        

        public string ProjektverantwortlicherMitarbeiter { get; set; }

        public int Vertragsnummer { get; set; }

        public Auftrag()
        {
            Auftragspositionen = new List<Auftragsposition>();
        }
    }
}

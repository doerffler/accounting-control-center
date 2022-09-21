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

        public Mitarbeiter VerantwortlicherMitarbeiter { get; set; }
        
        public Geschaeftsjahr Geschaeftsjahr { get; set; }

        public Projekt ZugehoerigesProjekt { get; set; }

        public int Vertragsnummer { get; set; }

        public Auftrag()
        {
            Auftragspositionen = new List<Auftragsposition>();
        }
    }
}

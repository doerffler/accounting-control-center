using System;
using System.Collections.Generic;

namespace ACCDataModel.Stammdaten
{
    public class Auftrag
    {
        public DateTime? Auftragsbeginn { get; set; }

        public DateTime? Auftragsende { get; set; }  

        public DateTime Auftragsdatum { get; set; }

        public int AuftragID { get; set; }

        public string Auftragsname { get; set; }

        public ICollection<Auftragsposition> Auftragspositionen { get; set; }

        public int? VerantwortlicherMitarbeiterID { get; set; }

        public Mitarbeiter VerantwortlicherMitarbeiter { get; set; }

        public int? ZugehoerigesGeschaeftsjahrID { get; set; }

        public Geschaeftsjahr ZugehoerigesGeschaeftsjahr { get; set; }

        public int? ZugehoerigesProjektID { get; set; }

        public Projekt ZugehoerigesProjekt { get; set; }

        public int Vertragsnummer { get; set; }

        public int? ZugehoerigeWaehrungID { get; set; }

        public Waehrung ZugehoerigeWaehrung { get; set; }

        public ICollection<Leistungsnachweis> Leistungsnachweise {  get; set; }

        public override string ToString()
        {
            return $"{ZugehoerigesGeschaeftsjahr?.Name}|{Auftragsname}";
        }

        public Auftrag()
        {
            Auftragspositionen = new List<Auftragsposition>();
            Leistungsnachweise = new List<Leistungsnachweis>();
        }
    }
}

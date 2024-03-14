using ACCDataModel.Dokumentenmanagement;
using System;
using System.Collections.Generic;

namespace ACCDataModel.Stammdaten
{
    public class Leistungsnachweis
    {
        public int LeistungsnachweisID { get; set; }

        public DateTime Von {  get; set; }
        
        public DateTime Bis { get; set; }

        public int? ZugehoerigeKostenstelleID { get; set; }

        public Kostenstelle ZugehoerigeKostenstelle { get; set; }

        public int? ZugehoerigerAuftragID { get; set; }

        public Auftrag ZugehoerigerAuftrag { get; set; }

        public int? ZugehoerigesLeistungsnachweisDokumentID { get; set; }

        public LeistungsnachweisDokument ZugehoerigesLeistungsnachweisDokument { get; set; }

        public ICollection<Leistungsnachweisposition> Leistungsnachweispositionen { get; set; }

        public Leistungsnachweis()
        {
            Leistungsnachweispositionen = new List<Leistungsnachweisposition>();
        }
    }
}

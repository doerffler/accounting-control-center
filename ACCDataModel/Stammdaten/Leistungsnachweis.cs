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

        public Kostenstelle Kostenstelle { get; set; }

        public Auftrag ZugehoerigerAuftrag { get; set; }

        public LeistungsnachweisDokument ZugehoerigesLeistungsnachweisDokument { get; set; }

        public List<Leistungsnachweisposition> Leistungsnachweispositionen { get; set; }
    }
}

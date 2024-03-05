using ACCDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ACCDataModel.Kostenrechnung
{
    public class Ausgangsrechnungsposition : Rechnungsposition
    {

        public int AusgangsrechnungspositionID { get; set; }


        public int AusgangsrechnungID { get; set; }

        public Ausgangsrechnung ZugehoerigeRechnung { get; set; }

        public ICollection<Eingangsrechnungsposition> ZugehoerigeFremdleistungen { get; set; }

        public int? AuftragspositionID { get; set; }

        public Auftragsposition ZugehoerigeAuftragsposition { get; set; }

        public int? LeistungsnachweispositionID { get; set; }

        public Leistungsnachweisposition Leistungsnachweisposition { get; set; }

        public Ausgangsrechnungsposition() : base ()
        {

        }

    }
}

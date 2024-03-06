using ACCDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACCDataModel.Kostenrechnung
{
    public class Eingangsrechnung : Rechnung
    {

        public int EingangsrechnungID { get; set; }

        public int? ZugehoerigerKreditorID { get; set; }

        public Kreditor ZugehoerigerKreditor { get; set; }

        public ICollection<Eingangsrechnungsposition> Rechnungspositionen { get; set; } = new List<Eingangsrechnungsposition>();

        public int? KorrekturRechnungID { get; set; }

        public Eingangsrechnung KorrekturRechnung { get; set; }

        public int? ZugehoerigerVertragID { get; set; }

        public Vertrag ZugehoerigerVertrag { get; set; }

        public Eingangsrechnung() : base()
        {

        }

    }
}

using ACCDataModel.Stammdaten;
using System.Collections.Generic;

namespace ACCDataModel.Kostenrechnung
{
    public class Eingangsrechnung : Rechnung
    {
        public int EingangsrechnungID { get; set; }

        public int? ZugehoerigerKreditorID { get; set; }

        public Kreditor ZugehoerigerKreditor { get; set; }

        public ICollection<Eingangsrechnungshistorie> ZugehoerigeEingangsrechnungshistorie { get; set; }

        public ICollection<Eingangsrechnungsposition> Rechnungspositionen { get; set; }

        public int? KorrekturRechnungID { get; set; }

        public Eingangsrechnung KorrekturRechnung { get; set; }

        public int? ZugehoerigerVertragID { get; set; }

        public Vertrag ZugehoerigerVertrag { get; set; }

        public Eingangsrechnung() : base()
        {
            Rechnungspositionen = new List<Eingangsrechnungsposition>();
            ZugehoerigeEingangsrechnungshistorie = new List<Eingangsrechnungshistorie>();
    }
}
}

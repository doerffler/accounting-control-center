using ACCDataModel.Stammdaten;
using System.Collections.Generic;

namespace ACCDataModel.Kostenrechnung
{
    public class Ausgangsrechnungsposition : Rechnungsposition
    {

        public int AusgangsrechnungspositionID { get; set; }

        public int ZugehoerigeRechnungID { get; set; }

        public Ausgangsrechnung ZugehoerigeRechnung { get; set; }

        public ICollection<Eingangsrechnungsposition> ZugehoerigeFremdleistungen { get; set; }

        public int? ZugehoerigeAuftragspositionID { get; set; }

        public Auftragsposition ZugehoerigeAuftragsposition { get; set; }

        public int? ZugehoerigeLeistungsnachweispositionID { get; set; }

        public Leistungsnachweisposition ZugehoerigeLeistungsnachweisposition { get; set; }

        public Ausgangsrechnungsposition() : base ()
        {
            ZugehoerigeFremdleistungen  = new List<Eingangsrechnungsposition>();
        }
    }
}

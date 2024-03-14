using ACCDataModel.Stammdaten;
using System.Collections.Generic;

namespace ACCDataModel.Kostenrechnung
{
    public class Ausgangsrechnung : Rechnung
    {

        public int AusgangsrechnungID { get; set; }

        public int? RechnungsempfaengerID { get; set; }

        public Debitor Rechnungsempfaenger { get; set; }

        public string Rechnungstext {  get; set; }

        public ICollection<Ausgangsrechnungshistorie> ZugehoerigeAusgangsrechnungshistorie { get; set; }

        public ICollection<Ausgangsrechnungsposition> Rechnungspositionen { get; set; }

        public int? KorrekturRechnungID { get; set; }

        public Ausgangsrechnung KorrekturRechnung { get; set; }

        public Ausgangsrechnung() : base()
        {
            ZugehoerigeAusgangsrechnungshistorie = new List<Ausgangsrechnungshistorie>();
            Rechnungspositionen = new List<Ausgangsrechnungsposition>();
        }

    }
}

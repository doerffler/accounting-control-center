using ACCDataModel.Stammdaten;
using System.Collections.Generic;

namespace ACCDataModel.Kostenrechnung
{
    public class Ausgangsrechnung : Rechnung
    {

        public int AusgangsrechnungID { get; set; }

        public Debitor Rechnungsempfaenger { get; set; }

        public string Rechnungstext {  get; set; }

        public ICollection<AusgangsrechnungHistorie> AusgangsrechnungHistorie { get; set; }

        public ICollection<Ausgangsrechnungsposition> Rechnungspositionen { get; set; }

        public Ausgangsrechnung KorrekturRechnung { get; set; }
        
        public Ausgangsrechnung() : base()
        {
        
        }

    }
}

using ACCDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACCDataModel.Kostenrechnung
{
    public class Ausgangsrechnung : Rechnung
    {

        public int AusgangsrechnungID { get; set; }

        public Debitor Rechnungsempfaenger { get; set; }

        public ICollection<Ausgangsrechnungsposition> Rechnungspositionen { get; set; } = new List<Ausgangsrechnungsposition>();

        public Ausgangsrechnung KorrekturRechnung { get; set; }
        
        public Ausgangsrechnung() : base()
        {
        
        }

    }
}

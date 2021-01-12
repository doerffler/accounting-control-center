using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Kostenrechnung
{
    public class Ausgangsrechnung : Rechnung
    {

        public ICollection<Ausgangsrechnungsposition> Rechnungspositionen { get; set; } = new List<Ausgangsrechnungsposition>();

        public Ausgangsrechnung KorrekturRechnung { get; set; }

        public Ausgangsrechnung() : base()
        {
        
        }

    }
}

using ACCDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACCDataModel.Kostenrechnung
{
    public class Ausgangsrechnungsposition : Rechnungsposition
    {

        public int AusgangsrechnungspositionID { get; set; }

        public Ausgangsrechnung ZugehoerigeRechnung { get; set; }

        public ICollection<Eingangsrechnungsposition> ZugehoerigeFremdleistungen { get; set; }

        public Auftragsposition ZugehoerigeAuftragsposition { get; set; }

        public Ausgangsrechnungsposition() : base ()
        {

        }

    }
}

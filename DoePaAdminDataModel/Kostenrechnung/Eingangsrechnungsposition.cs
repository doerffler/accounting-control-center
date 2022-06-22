using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Kostenrechnung
{
    public class Eingangsrechnungsposition : Rechnungsposition
    {

        public int EingangsrechnungspositionID { get; set; }

        public Eingangsrechnung ZugehoerigeRechnung { get; set; }

        public Eingangsrechnungsposition () : base()
        {

        }

    }
}

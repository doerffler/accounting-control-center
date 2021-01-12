using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Kostenrechnung
{
    public class Ausgangsrechnungsposition : Rechnungsposition
    {

        public Ausgangsrechnung ZugehoerigeRechnung { get; set; }

        public ICollection<Eingangsrechnung> ZugehoerigeFremdleistungen { get; set; }

        public Ausgangsrechnungsposition() : base ()
        {

        }

    }
}

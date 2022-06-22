using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Kostenrechnung
{
    public class Ausgangsrechnungsposition : Rechnungsposition
    {

        public Ausgangsrechnung ZugehoerigeRechnung { get; set; }

        public ICollection<Eingangsrechnungsposition> ZugehoerigeFremdleistungen { get; set; }

        public Auftragsposition ZugehoerigeAuftragsposition { get; set; }

        public Ausgangsrechnungsposition() : base ()
        {

        }

    }
}

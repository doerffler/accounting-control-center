using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Debitor : Geschaeftspartner
    {

        public int DebitorID { get; set; }

        public List<Projekt> Projekte { get; set; }

        public Kunde ZugehoerigerKunde { get; set; }

        public Debitor() : base()
        {
            Projekte = new List<Projekt>();
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", ZugehoerigerKunde?.Kundenname, base.ToString());
        }

    }
}

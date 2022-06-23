using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Debitor : Geschaeftspartner
    {

        public int DebitorID { get; set; }

        public Kunde ZugehoerigerKunde { get; set; }

    }
}

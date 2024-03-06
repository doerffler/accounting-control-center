﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ACCDataModel.Stammdaten
{
    public class Debitor : Geschaeftspartner
    {

        public int DebitorID { get; set; }

        public List<Projekt> Projekte { get; set; }

        public int? ZugehoerigerKundeID { get; set; }

        public Kunde ZugehoerigerKunde { get; set; }

        public Debitor() : base()
        {

        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", ZugehoerigerKunde?.Kundenname, base.ToString());
        }

    }
}

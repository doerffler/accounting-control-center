﻿using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Kostenrechnung
{
    public class Eingangsrechnung : Rechnung
    {

        public int EingangsrechnungID { get; set; }

        public Kreditor ZugehoerigerKreditor { get; set; }

        public ICollection<Eingangsrechnungsposition> Rechnungspositionen { get; set; } = new List<Eingangsrechnungsposition>();

        public Eingangsrechnung KorrekturRechnung { get; set; }

        public Vertrag ZugehoerigerVertrag { get; set; }

        public Eingangsrechnung() : base()
        {

        }

    }
}

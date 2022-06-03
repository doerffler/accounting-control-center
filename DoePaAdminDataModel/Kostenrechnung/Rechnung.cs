using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;

namespace DoePaAdminDataModel.Kostenrechnung
{
    public abstract class Rechnung
    {

        public int RechnungID { get; set; }

        public string RechnungsNummer { get; set; }

        public DateTime RechnungsDatum { get; set; }

        public decimal? RabattPct { get; set; }

        public DateTime BezahltDatum { get; set; }

        public Rechnung()
        {
        }

    }
}

using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Kostenrechnung
{
    public abstract class Rechnungsposition
    {
                
        public int PositionsNummer { get; set; }

        public Kostenstelle ZugehoerigeKostenstelle { get; set; }

        public decimal? AnzahlStunden { get; set; }

        public decimal? Stundensatz { get; set; }

        public decimal Nettobetrag { get; set; }

        public Waehrung NettobetragWaehrung { get; set; }

        public decimal Steuersatz { get; set; }

        public string Positionsbeschreibung { get; set; }

        public DateTime LeistungszeitraumVon { get; set; }

        public DateTime LeistungszeitraumBis { get; set; }

        public Rechnungsposition()
        {
        }
    }
}

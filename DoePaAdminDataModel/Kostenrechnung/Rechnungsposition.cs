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

        public decimal Stueckzahl { get; set; }

        public int? ZugehoerigeAbrechnungseinheitID { get; set; }

        public Waehrung ZugehoerigeAbrechnungseinheit { get; set; }

        public decimal StueckpreisNetto { get; set; }

        public decimal Nettobetrag
        {
            get
            {
                return StueckpreisNetto * Stueckzahl;
            }
        }

        public Waehrung NettobetragWaehrung { get; set; }

        public decimal Steuersatz { get; set; }

        public string Positionsbeschreibung { get; set; }

        public DateTime? LeistungszeitraumVon { get; set; }

        public DateTime LeistungszeitraumBis { get; set; }

        public Rechnungsposition()
        {
        }
    }
}

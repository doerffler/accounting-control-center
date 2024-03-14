using ACCDataModel.Stammdaten;
using System;

namespace ACCDataModel.Kostenrechnung
{
    public abstract class Rechnungsposition
    {
                
        public int PositionsNummer { get; set; }

        public int? ZugehoerigeKostenstelleID { get; set; }

        public Kostenstelle ZugehoerigeKostenstelle { get; set; }

        public decimal Stueckzahl { get; set; }

        public int? ZugehoerigeAbrechnungseinheitID { get; set; }

        public Abrechnungseinheit ZugehoerigeAbrechnungseinheit { get; set; }

        public decimal StueckpreisNetto { get; set; }

        public decimal Nettobetrag
        {
            get
            {
                return StueckpreisNetto * Stueckzahl;
            }
        }

        public decimal Steuersatz { get; set; }

        public string Positionsbeschreibung { get; set; }

        public DateTime? LeistungszeitraumVon { get; set; }

        public DateTime LeistungszeitraumBis { get; set; }

        public Rechnungsposition()
        {
        }
    }
}

using ACCDataModel.Kostenrechnung;
using System;

namespace ACCDataModel.Stammdaten
{
    public class Leistungsnachweisposition
    {
        public int LeistungsnachweispositionID {  get; set; }

        public Leistungsnachweis Leistungsnachweis {  get; set; }

        public DateTime Datum { get; set; }

        public decimal Geleistet { get; set; }

        public decimal Fakturierbar {  get; set; }

        public int? AusgangangsrechnungspositionID { get; set; }

        public Ausgangsrechnungsposition Ausgangsrechnungsposition { get; set; }

        public string Kommentar {  get; set; }
    }
}

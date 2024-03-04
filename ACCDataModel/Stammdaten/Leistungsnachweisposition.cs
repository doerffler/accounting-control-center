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

        public int AuftragspositionID { get; set; }

        public Auftragsposition Auftragsposition { get; set; }

        public string Kommentar {  get; set; }
    }
}

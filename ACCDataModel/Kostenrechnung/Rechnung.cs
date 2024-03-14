using ACCDataModel.Dokumentenmanagement;
using ACCDataModel.Stammdaten;
using System;

namespace ACCDataModel.Kostenrechnung
{
    public abstract class Rechnung
    {

        public string RechnungsNummer { get; set; }

        public DateTime RechnungsDatum { get; set; }

        public decimal? RabattPct { get; set; }

        public DateTime? BezahltDatum { get; set; }

        public int ZugehoerigeWaehrungID { get; set; }

        public Waehrung ZugehoerigeWaehrung { get; set; }

        public int? ZugehoerigesGeschaeftsjahrID { get; set; }

        public Geschaeftsjahr ZugehoerigesGeschaeftsjahr { get; set; }

        public int? ZugehoerigesDokumentID { get; set; }

        public Rechnungsdokument ZugehoerigesDokument { get; set; }

        public Rechnung()
        {
        }

    }
}

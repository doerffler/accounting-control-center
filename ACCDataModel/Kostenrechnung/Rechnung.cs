using ACCDataModel.Dokumentenmanagement;
using ACCDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ACCDataModel.Kostenrechnung
{
    public abstract class Rechnung
    {

        public string RechnungsNummer { get; set; }

        public DateTime RechnungsDatum { get; set; }

        public decimal? RabattPct { get; set; }

        public DateTime? BezahltDatum { get; set; }

        public int WaehrungID { get; set; }

        public Waehrung ZugehoerigeWaehrung { get; set; }

        public int? GeschaeftsjahrID { get; set; }

        public Geschaeftsjahr ZugehoerigesGeschaeftsjahr { get; set; }

        public int? RechnungsdokumentID { get; set; }

        public Rechnungsdokument ZugehoerigesDokument { get; set; }

        public Rechnung()
        {
        }

    }
}

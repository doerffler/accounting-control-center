using ACCDataModel.Enum;
using System;

namespace ACCDataModel.Kostenrechnung
{
    public class Eingangsrechnungshistorie
    {
        public int EingangsrechnungshistorieID { get; set; }

        public DateTime Zeitstempel { get; set; }

        public IncomingInvoiceStatus Status { get; set; }

        public string Bemerkung { get; set; }

        public string Benutzer { get; set; }

        public int? ZugehoerigeEingangsrechnungID { get; set; }

        public Eingangsrechnung ZugehoerigeEingangsrechnung { get; set; }

        public Eingangsrechnungshistorie() : base()
        {

        }
    }
}

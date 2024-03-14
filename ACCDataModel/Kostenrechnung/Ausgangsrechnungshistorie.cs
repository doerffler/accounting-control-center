using ACCDataModel.Enum;
using System;

namespace ACCDataModel.Kostenrechnung
{
    public class Ausgangsrechnungshistorie
    {
        public int AusgangsrechnungshistorieID { get; set; }

        public DateTime Zeitstempel { get; set; }

        public OutgoingInvoiceStatus Status { get; set; }

        public string Bemerkung { get; set; }

        public string Benutzer { get; set; }

        public int? ZugehoerigeAusgangsrechnungID { get; set; }

        public Ausgangsrechnung ZugehoerigeAusgangsrechnung { get; set; }

        public Ausgangsrechnungshistorie() : base()
        {

        }
    }
}

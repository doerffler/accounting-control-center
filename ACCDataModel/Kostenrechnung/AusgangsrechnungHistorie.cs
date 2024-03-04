using ACCDataModel.Enum;
using System;

namespace ACCDataModel.Kostenrechnung
{
    public class AusgangsrechnungHistorie
    {

        public int AusgangsrechnungHistorieID { get; set; }

        public DateTime Zeitstempel { get; set; }

        public UserActionEnum AktionsTyp { get; set; }

        public string Bemerkung { get; set; }

        public string Benutzer { get; set; }

        public Ausgangsrechnung ZugehoerigeAusgangsrechnung { get; set; }

        public AusgangsrechnungHistorie() : base()
        {

        }

    }
}

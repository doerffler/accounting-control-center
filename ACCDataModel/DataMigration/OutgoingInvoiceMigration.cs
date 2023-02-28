using ACCDataModel.DPApp;
using ACCDataModel.Kostenrechnung;
using ACCDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.DataMigration
{
    public class OutgoingInvoiceMigration
    {

        public OutgoingInvoice OutgoingInvoiceForImport { get; set; }

        public Waehrung RelatedWaehrung { get; set; }

        public Debitor RelatedRechnungsempfaenger { get; set; }

        public Geschaeftsjahr RelatedGeschaeftsjahr { get; set; }

        public Ausgangsrechnung RelatedKorrekturrechnung { get; set; }

        public IEnumerable<OutgoingInvoicePositionMigration> OutgoingInvoicePositions { get; set; }

        public bool IsReadyForMigration { get; set; }

        public Ausgangsrechnung CreateAusgangsrechnung()
        {

            Ausgangsrechnung newAR = new()
            {

                BezahltDatum = OutgoingInvoiceForImport.DatePaid,
                KorrekturRechnung = RelatedKorrekturrechnung,
                //RabattPct = ,
                RechnungsDatum = OutgoingInvoiceForImport.DateDocument ?? DateTime.MinValue,
                Rechnungsempfaenger = RelatedRechnungsempfaenger,
                RechnungsNummer = OutgoingInvoiceForImport.InvoiceNo,
                ZugehoerigesGeschaeftsjahr = RelatedGeschaeftsjahr,
                ZugehoerigeWaehrung = RelatedWaehrung
                
            };

            IList<Ausgangsrechnungsposition> newRechnungspositionen = new List<Ausgangsrechnungsposition>(OutgoingInvoicePositions.Count());

            foreach (OutgoingInvoicePositionMigration invoicePosition in OutgoingInvoicePositions)
            {
                newRechnungspositionen.Add(invoicePosition.CreateAusgangsrechnungsposition(newAR));
            }

            newAR.Rechnungspositionen = newRechnungspositionen;

            return newAR;

        }

    }
}

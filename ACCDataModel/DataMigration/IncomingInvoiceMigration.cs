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
    public class IncomingInvoiceMigration
    {

        public IncomingInvoice IncomingInvoiceForImport { get; set; }

        public Waehrung RelatedWaehrung { get; set; }

        public Debitor RelatedRechnungsempfaenger { get; set; }

        public Geschaeftsjahr RelatedGeschaeftsjahr { get; set; }

        public Ausgangsrechnung RelatedKorrekturrechnung { get; set; }

        public IEnumerable<IncomingInvoicePositionMigration> IncomingInvoicePositions { get; set; }

        public bool IsReadyForMigration { get; set; }

        public Ausgangsrechnung CreateAusgangsrechnung()
        {

            Ausgangsrechnung newAR = new()
            {

                BezahltDatum = IncomingInvoiceForImport.DatePaid,
                KorrekturRechnung = RelatedKorrekturrechnung,
                //RabattPct = ,
                RechnungsDatum = IncomingInvoiceForImport.DateDocument ?? DateTime.MinValue,
                Rechnungsempfaenger = RelatedRechnungsempfaenger,
                RechnungsNummer = IncomingInvoiceForImport.InvoiceNo,
                ZugehoerigesGeschaeftsjahr = RelatedGeschaeftsjahr,
                ZugehoerigeWaehrung = RelatedWaehrung
                
            };

            IList<Ausgangsrechnungsposition> newRechnungspositionen = new List<Ausgangsrechnungsposition>(IncomingInvoicePositions.Count());

            foreach (IncomingInvoicePositionMigration invoicePosition in IncomingInvoicePositions)
            {
                newRechnungspositionen.Add(invoicePosition.CreateAusgangsrechnungsposition(newAR));
            }

            newAR.Rechnungspositionen = newRechnungspositionen;

            return newAR;

        }

    }
}

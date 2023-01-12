using DoePaAdminDataModel.DPApp;
using DoePaAdminDataModel.Kostenrechnung;
using DoePaAdminDataModel.Stammdaten;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DataMigration
{
    public class OutgoingInvoiceMigration
    {

        public OutgoingInvoice OutgoingInvoiceForImport { get; set; }

        public Waehrung RelatedWaehrung { get; set; }

        public IEnumerable<OutgoingInvoicePositionMigration> OutgoingInvoicePositions { get; set; }

        public Ausgangsrechnung CreateAusgangsrechnung()
        {
            Ausgangsrechnung newAR = new()
            {



                ZugehoerigeWaehrung = RelatedWaehrung,

            };

            return newAR;

        }

    }
}

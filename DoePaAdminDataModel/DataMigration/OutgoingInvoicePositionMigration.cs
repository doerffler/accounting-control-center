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
    public class OutgoingInvoicePositionMigration
    {

        public OutgoingInvoicePosition OutgoingInvoicePositionForImport { get; set; }

        public Auftragsposition RelatedAuftragsposition { get; set; }

        public Kostenstelle RelatedKostenstelle { get; set; }

    }
}

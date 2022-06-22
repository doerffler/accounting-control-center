using DoePaAdminDataModel.DPApp;
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

        public IEnumerable<OutgoingInvoicePositionMigration> OutgoingInvoicePositions { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.DPApp
{
    public class IncomingInvoice : Invoice
    {

        public DateTime? DateEntrance { get; set; }

        public string InvoiceInputPer { get; set; }

        public bool Rotational { get; set; }

        public DateTime? PayableBy { get; set; }

        public DateTime? PayableOn { get; set; }

        public string Supplier { get; set; }

        public bool StaffVoucher { get; set; }

    }
}

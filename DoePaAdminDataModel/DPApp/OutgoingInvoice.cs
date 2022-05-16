using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DPApp
{
    public class OutgoingInvoice : Invoice
    {
        
        public string Introduction { get; set; }

        public DateTime? DateSend { get; set; }
                
        public long? ContactId { get; set; }

        public string Client { get; set; }

        public long? StaffIdSignature { get; set; }

        public long? StaffIdSendBy { get; set; }
        
        public string Currency { get; set; }

        public string InvoiceSendPer { get; set; }

        public string Language { get; set; }

        public string ContractNo { get; set; }

        public string OrderNo { get; set; }

        public string TermOfPayment { get; set; }

    }
}

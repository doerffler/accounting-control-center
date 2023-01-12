using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DTO
{
    public class InvoiceDTO
    {

        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DatePaid { get; set; }
        public string BusinessYear { get; set; }
        public string InvoiceRecipient { get; set; }
        public string PostalCode { get; set; }
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string CurrencyISO { get; set; }
        public IList<InvoiceItemDTO> InvoiceItems { get; set; }

        public InvoiceDTO()
        {
            InvoiceItems = new List<InvoiceItemDTO>();
        }
    }
}

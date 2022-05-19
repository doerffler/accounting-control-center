using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DPApp
{
    public class OutgoingInvoicePosition : InvoicePosition
    {
                        
        public decimal? HourlyRateExternal { get; set; }
        
        public decimal? NettoExternal { get; set; }
        
        public decimal? TaxPercentExternal { get; set; }
        
        public decimal? TaxExternal { get; set; }
        
        public decimal? GrossExternal { get; set; }
        
        public long? InvoiceIdExternal { get; set; }
        
        public long? Raid { get; set; }

    }
}

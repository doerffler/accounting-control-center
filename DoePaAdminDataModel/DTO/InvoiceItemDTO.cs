using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DTO
{
    public class InvoiceItemDTO
    {
        public int ItemNumber { get; set; }
        public DateTime? DateServiceFrom { get; set; }
        public DateTime DateServiceUntil { get; set; }
        public string ItemDescription { get; set; }
        public decimal TaxRateDecimal { get; set; }
        public decimal NetUnitPrice { get; set; }
        public decimal UnitQuantity { get; set; }
        public int CostCenterNumber { get; set; }
        public string ItemBillingUnitCode { get; set; }
        public int OrderContractNumber { get; set; }
        public int OrderItemPosition { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DPApp
{
    public abstract class Invoice : DPAppObject
    {

        public string InvoiceNo { get; set; }

        public DateTime? DateDocument { get; set; }

        public string CreatedBy { get; set; }

        public string InvoiceText { get; set; }

        public long BusinessYearId { get; set; }

        public bool TransferredFree { get; set; }

        public DateTime DateTransferred { get; set; }

        public DateTime DatePaid { get; set; }

        public bool Paid { get; set; }

        public string Remark { get; set; }

        public long? InvoiceIdReplacedBy { get; set; }

        public long? AddressId { get; set; }

        public long? DepartmentId { get; set; }

        public long? CostCenterIdDefault { get; set; }

        public long? CostTypeIdDefault { get; set; }

        public long? ProjectIdDefault { get; set; }
        
        public DateTime? DateServiceFromDefault { get; set; }

        public DateTime? DateServiceUntilDefault { get; set; }

        public IEnumerable<InvoicePosition> RelatedInvoicePositions { get; set; }

        public BusinessYear RelatedBusinessYear { get; set; }

        public Contact RelatedContact { get; set; }

        public Address RelatedAddress { get; set; }

        public Department RelatedDepartment { get; set; }

        public Company RelatedCompany { get; set; }

        public decimal? NettoSum
        {
            get
            {
                if (RelatedInvoicePositions != null)
                {
                    return RelatedInvoicePositions.Sum(ip => ip.Netto);
                }
                else
                {
                    return null;
                }
            }
        }

    }
}

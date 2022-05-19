using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DPApp
{
    public abstract class InvoicePosition
    {

        public long Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? Updated_at { get; set; }

        public long RelatedInvoiceId { get; set; }

        public int Sequence { get; set; }

        public string PositionText { get; set; }

        public DateTime DateServiceFrom { get; set; }

        public DateTime DateServiceUntil { get; set; }

        public string TypeOfSettlement { get; set; }

        public decimal? Hours { get; set; }

        public decimal? HourlyRate { get; set; }

        public decimal? Netto { get; set; }

        public decimal? Tax { get; set; }

        public decimal? TaxPercent { get; set; }

        public decimal? Gross { get; set; }

        public string Remark { get; set; }

        public long? CostTypeId { get; set; }

        public long? ProjectId { get; set; }

        public long? CostCenterId { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DTO
{
    public class RemainingBudgetOnOrdersDTO
    {
        public DateTime Date;
        
        public string OrderName;
        public string OrderPosition;

        public decimal ActualRemaining;

        public decimal PlannedRemaining;
    }
}

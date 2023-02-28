using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.DTO
{
    public class RemainingBudgetOnOrdersDTO
    {
        public DateTime Date { get; set; }
        public DateTime? OrderStart { get; set; }
        public DateTime? OrderEnd { get; set; }

        public int KostenstellenNummer { get; set; }
        
        public string OrderName { get; set; }
        public string OrderPosition { get; set; }
        
        public decimal ResidualBudgetActualBefore { get; set; }
        public decimal ActualConsumption { get; set; }
        public decimal ResidualBudgetActualAfter { get; set; }
        
        public decimal ResidualBudgetTargetBefore { get; set; }
        public decimal TargetConsumption { get; set; }
        public decimal ResidualBudgetTargetAfter { get; set; }
    }
}

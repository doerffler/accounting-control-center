using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DTO
{
    public class EmployeeAccountingDTO
    {
        public string Month { get; set; }
        public string Project { get; set; }
        public string Customer { get; set; }
        public decimal AccountingCount { get; set; }
        public string AccountingUnitName { get; set; }
    }
}

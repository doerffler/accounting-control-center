using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.API
{
    public class EmployeeInvoicedHours
    {
        public string Month { get; set; }
        public string Project { get; set; }
        public string Customer { get; set; }
        public double HoursCount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.DPApp
{
    public class Department : DPAppObject
    {

        public string ShortName { get; set; }

        public string Name { get; set; }

        public string Language { get; set; }

        public bool InvoiceRelevant { get; set; }

        public bool Supplier { get; set; }

        public bool Client { get; set; }

        public DateTime ValidFrom { get; set; }

        public long? AddressId { get; set; }

        public long? CompanyId { get; set; }

    }
}

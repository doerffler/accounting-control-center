using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.DPApp
{
    public class Company : DPAppObject
    {

        public string ShortName { get; set; }

        public string Name1 { get; set; }

        public string Name2 { get; set; }

        public DateTime ValidFrom { get; set; }

        public long? AddressId { get; set; }

        public bool? ReverseCharge { get; set; }

        public string VatNo { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.DPApp
{
    public class BusinessYear : DPAppObject
    {

        public string Name { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateUntil { get; set; }

        public string InvoiceName { get; set; }

    }
}

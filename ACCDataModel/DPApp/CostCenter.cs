using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.DPApp
{
    public class CostCenter : DPAppObject
    {

        public int Number { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

    }
}

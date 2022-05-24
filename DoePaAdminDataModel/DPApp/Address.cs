using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.DPApp
{
    public class Address : DPAppObject
    {

        public string Addition1 { get; set; }

        public string Addition2 { get; set; }

        public string Street { get; set; }

        public string StreetNumber { get; set; }

        public string Postcode { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

    }
}

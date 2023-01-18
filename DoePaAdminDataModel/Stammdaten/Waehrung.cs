using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Waehrung
    {

        public int WaehrungID { get; set; }

        public string WaehrungISO { get; set; }

        public string WaehrungName { get; set; }

        public string WaehrungZeichen { get; set; }

        public Dictionary<string, string> WaehrungAdditions { get; set; }

    }
}

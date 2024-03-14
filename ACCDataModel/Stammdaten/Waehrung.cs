using System.Collections.Generic;

namespace ACCDataModel.Stammdaten
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

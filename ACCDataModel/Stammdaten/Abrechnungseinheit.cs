using System.Collections.Generic;

namespace ACCDataModel.Stammdaten
{
    public class Abrechnungseinheit
    {

        public int AbrechnungseinheitID { get; set; }

        public string Name { get; set; }

        public string Abkuerzung { get; set; }

        public Dictionary<string, string> Additions { get; set; }

    }
}

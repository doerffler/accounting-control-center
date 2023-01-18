using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoePaAdminDataModel.Stammdaten
{
    public class Abrechnungseinheit
    {

        public int AbrechnungseinheitID { get; set; }

        public string Name { get; set; }

        public string Abkuerzung { get; set; }

        public Dictionary<string, string> Additions { get; set; }

    }
}

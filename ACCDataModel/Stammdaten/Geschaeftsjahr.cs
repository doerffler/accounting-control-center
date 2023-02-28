using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACCDataModel.Stammdaten
{
    public class Geschaeftsjahr
    {

        public int GeschaeftsjahrId { get; set; }

        public string Name { get; set; }

        public DateTime DatumVon { get; set; }

        public DateTime DatumBis { get; set; }

        public List<Feiertag> Feiertage { get; set; }

        public List<Auftrag> Auftraege { get; set; }

        public string Rechnungsprefix { get; set; }

        public Geschaeftsjahr()
        {
            Feiertage = new();
            Auftraege = new();
        }
    }
}

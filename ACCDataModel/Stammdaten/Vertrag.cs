using System;
using System.Collections.Generic;
using System.Text;

namespace ACCDataModel.Stammdaten
{
    public class Vertrag
    {

        public int VertragID { get; set; }

        public string Vertragsbezeichnung { get; set; }

        public DateTime GueltigVon { get; set;}

        public DateTime GueltigBis { get; set; }

        public Vertrag()
        {

        }
    }
}

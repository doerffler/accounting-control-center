using System;
using System.Collections.Generic;
using System.Text;

namespace DoePaAdminDataModel.Stammdaten
{
    public abstract class Geschaeftspartner
    {

        public int GeschaeftspartnerID { get; set; }

        public string Anschrift { get; set; }

        public Adresse ZugehoerigeAdresse { get; set; }

        public Dictionary<string, string> Additions { get; set; }
        
        public override string ToString()
        {
            return Anschrift;
        }

    }
}

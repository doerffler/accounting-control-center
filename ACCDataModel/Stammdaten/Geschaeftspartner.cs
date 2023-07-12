using System;
using System.Collections.Generic;
using System.Text;

namespace ACCDataModel.Stammdaten
{
    public abstract class Geschaeftspartner
    {
        
        public string Anschrift { get; set; }

        public Adresse ZugehoerigeAdresse { get; set; }

        public Dictionary<string, string> Additions { get; set; }
        
        public override string ToString()
        {
            return Anschrift;
        }

    }
}

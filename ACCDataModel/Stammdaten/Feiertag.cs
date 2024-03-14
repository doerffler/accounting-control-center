using System;

namespace ACCDataModel.Stammdaten
{
    public class Feiertag
    {
        public int FeiertagId { get; set; }

        public DateTime Datum { get; set; }

        public int? ZugehoerigesGeschaeftsjahrID { get; set; }

        public Geschaeftsjahr ZugehoerigesGeschaeftsjahr { get; set; }

        public string FeiertagName { get; set; }

        public bool IstGanztag { get; set; }

        public bool Niedersachsen { get; set; }

        public bool Bremen { get; set; }

        public bool Hamburg { get; set; }

        public bool NordrheinWestfalen { get; set; }

        public bool Bayern { get; set; }

        public bool BadenWuerttemberg { get; set; }
        
        public bool Hessen { get; set; }
        
        public bool RheinlandPfalz { get; set; }
        
        public bool Sachsen { get; set; }
        
        public bool Berlin { get; set; }
        
        public bool SchleswigHolstein { get; set; }
        
        public bool Brandenburg { get; set; }
        
        public bool SachsenAnhalt { get; set; }
        
        public bool Thueringen { get; set; }
        
        public bool MecklenburgVorpommern { get; set; }
        
        public bool Saarland { get; set; }
    }
}
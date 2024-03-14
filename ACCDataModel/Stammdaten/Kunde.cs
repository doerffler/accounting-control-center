using System.Collections.Generic;

namespace ACCDataModel.Stammdaten
{
    public class Kunde
    {
        
        public int KundeID { get; set; }

        public string Langname { get; set; }

        public string Kundenname { get; set; }

        public ICollection<Debitor> Rechnungsempfaenger { get; set; }

        public Kunde()
        {
            Rechnungsempfaenger = new List<Debitor>();
        }

    }
}

using System.Collections.Generic;

namespace ACCDataModel.Stammdaten
{
    public class Debitor : Geschaeftspartner
    {

        public int DebitorID { get; set; }

        public ICollection<Projekt> Projekte { get; set; }

        public int? ZugehoerigerKundeID { get; set; }

        public Kunde ZugehoerigerKunde { get; set; }

        public Debitor() : base()
        {
            Projekte = new List<Projekt>();
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}", ZugehoerigerKunde?.Kundenname, base.ToString());
        }

    }
}

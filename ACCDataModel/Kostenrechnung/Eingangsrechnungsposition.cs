﻿namespace ACCDataModel.Kostenrechnung
{
    public class Eingangsrechnungsposition : Rechnungsposition
    {

        public int EingangsrechnungspositionID { get; set; }

        public int EingangsrechnungID { get; set; }

        public Eingangsrechnung ZugehoerigeRechnung { get; set; }

        public Eingangsrechnungsposition () : base()
        {

        }

    }
}

namespace ACCDataModel.Stammdaten
{

    public class Auftragsposition
    {
        public Abrechnungseinheit Abrechnungseinheit { get; set; }

        public int AuftragspositionID { get; set; }

        public int AuftragspositionNummer { get; set; }

        public decimal Auftragsvolumen { get; set; }

        public decimal StueckpreisNetto { get; set; }

        public int? AuftragID { get; set; }

        public Auftrag Auftrag { get; set; }
        
        public string Positionsbezeichnung { get; set; }   
        
        public override string ToString()
        {
            return $"{Auftrag?.ToString()}|{Positionsbezeichnung}";
        }
    }
}

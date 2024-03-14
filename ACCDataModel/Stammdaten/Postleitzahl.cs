namespace ACCDataModel.Stammdaten
{
    public class Postleitzahl
    {

        public int PostleitzahlID { get; set; }

        public string PLZ { get; set; }

        public string Ortsname { get; set; }

        public string Bundesland { get; set; }

        public string Land { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1} {2}", Land, PLZ, Ortsname);
        }

    }
}

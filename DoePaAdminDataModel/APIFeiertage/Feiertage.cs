using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DoePaAdminDataModel.APIFeiertage
{
    public class Feiertage
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("feiertage")]
        public IEnumerable<Feiertag> FeiertagListe { get; set; }
    }

    public class Feiertag
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("fname")]
        public string Name { get; set; }

        [JsonPropertyName("all_states")]
        public string AllStates { get; set; }

        [JsonPropertyName("ni")]
        public string Niedersachsen { get; set; }

        [JsonPropertyName("hb")]
        public string Bremen { get; set; }

        [JsonPropertyName("hh")]
        public string Hamburg { get; set; }

        [JsonPropertyName("nw")]
        public string NordrheinWestfalen { get; set; }

        [JsonPropertyName("by")]
        public string Bayern { get; set; }

        [JsonPropertyName("bw")]
        public string BadenWuerttemberg { get; set; }

        [JsonPropertyName("he")]
        public string Hessen { get; set; }

        [JsonPropertyName("rp")]
        public string RheinlandPfalz { get; set; }

        [JsonPropertyName("sn")]
        public string Sachsen { get; set; }

        [JsonPropertyName("be")]
        public string Berlin { get; set; }

        [JsonPropertyName("sh")]
        public string SchleswigHolstein { get; set; }

        [JsonPropertyName("bb")]
        public string Brandenburg { get; set; }

        [JsonPropertyName("st")]
        public string SachsenAnhalt { get; set; }

        [JsonPropertyName("th")]
        public string Thueringen { get; set; }

        [JsonPropertyName("mv")]
        public string MecklenburgVorpommern { get; set; }

        [JsonPropertyName("sl")]
        public string Saarland { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }
    }
}

using Newtonsoft.Json;

namespace TinderApp.Lib
{
    public class Versions
    {
        [JsonProperty("active_text")]
        public string ActiveText { get; set; }

        [JsonProperty("age_filter")]
        public string AgeFilter { get; set; }

        [JsonProperty("matchmaker")]
        public string Matchmaker { get; set; }

        [JsonProperty("trending")]
        public string Trending { get; set; }

        [JsonProperty("trending_active_text")]
        public string TrendingActiveText { get; set; }
    }
}
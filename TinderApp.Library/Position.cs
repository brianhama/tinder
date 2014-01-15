using Newtonsoft.Json;

namespace TinderApp.Lib
{
    public class Pos
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lon")]
        public double Lon { get; set; }
    }
}
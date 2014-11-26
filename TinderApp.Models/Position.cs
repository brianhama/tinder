using Newtonsoft.Json;

namespace TinderApp.Models
{
    public class Position
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }
    }
}
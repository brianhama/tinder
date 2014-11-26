using Newtonsoft.Json;

namespace TinderApp.Models
{
    public class AddPhotoRequest
    {
        [JsonProperty("assets")]
        public Asset[] Assets { get; set; }

        [JsonProperty("transmit")]
        public string Transmit { get; set; }
    }

    public class Asset
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("main")]
        public bool Main { get; set; }

        [JsonProperty("xdistance_percent")]
        public int XdistancePercent { get; set; }

        [JsonProperty("xoffset_percent")]
        public int XoffsetPercent { get; set; }

        [JsonProperty("ydistance_percent")]
        public double YdistancePercent { get; set; }

        [JsonProperty("yoffset_percent")]
        public double YoffsetPercent { get; set; }
    }
}
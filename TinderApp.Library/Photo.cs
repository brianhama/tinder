using Newtonsoft.Json;

namespace TinderApp.Lib
{
    public class Photo
    {
        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("fbId")]
        public object FbId { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("processedFiles")]
        public ProcessedFile[] ProcessedFiles { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("xdistance_percent")]
        public double XdistancePercent { get; set; }

        [JsonProperty("xoffset_percent")]
        public double XoffsetPercent { get; set; }

        [JsonProperty("ydistance_percent")]
        public double YdistancePercent { get; set; }

        [JsonProperty("yoffset_percent")]
        public double YoffsetPercent { get; set; }
    }
}
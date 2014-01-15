using Newtonsoft.Json;

namespace TinderApp.Lib
{
    public class Person
    {
        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("birth_date")]
        public string BirthDate { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photos")]
        public Photo[] Photos { get; set; }

        [JsonProperty("ping_time")]
        public string PingTime { get; set; }
    }
}
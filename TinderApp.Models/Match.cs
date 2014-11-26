using Newtonsoft.Json;

namespace TinderApp.Models
{
    public class Match
    {
        [JsonProperty("closed")]
        public bool Closed { get; set; }

        [JsonProperty("created_date")]
        public string CreatedDate { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("last_activity_date")]
        public string LastActivityDate { get; set; }

        [JsonProperty("messages")]
        public Msg[] Messages { get; set; }

        [JsonProperty("participants")]
        public string[] Participants { get; set; }

        [JsonProperty("person")]
        public Person Person { get; set; }
    }
}
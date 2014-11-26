using Newtonsoft.Json;

namespace TinderApp.Models
{
    public class UpdatesResponse
    {
        [JsonProperty("blocks")]
        public string[] Blocks { get; set; }

        [JsonProperty("deleted_lists")]
        public object[] DeletedLists { get; set; }

        [JsonProperty("last_activity_date")]
        public string LastActivityDate { get; set; }

        [JsonProperty("lists")]
        public object[] Lists { get; set; }

        [JsonProperty("matches")]
        public Match[] Matches { get; set; }
    }
}
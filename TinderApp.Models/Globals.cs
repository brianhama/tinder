using Newtonsoft.Json;

namespace TinderApp.Models
{
    /// <summary>
    /// We won't be using any of these variables which the server returns
    /// </summary>
    public class Globals
    {
        [JsonProperty("boost_decay")]
        public int BoostDecay { get; set; }

        [JsonProperty("boost_down")]
        public int BoostDown { get; set; }

        [JsonProperty("boost_up")]
        public int BoostUp { get; set; }

        [JsonProperty("invite_type")]
        public string InviteType { get; set; }

        [JsonProperty("kontagent")]
        public bool Kontagent { get; set; }

        [JsonProperty("matchmaker_default_message")]
        public string MatchmakerDefaultMessage { get; set; }

        [JsonProperty("recs_interval")]
        public int RecsInterval { get; set; }

        [JsonProperty("recs_size")]
        public int RecsSize { get; set; }

        [JsonProperty("share_default_text")]
        public string ShareDefaultText { get; set; }

        [JsonProperty("sparks")]
        public bool Sparks { get; set; }

        [JsonProperty("updates_interval")]
        public int UpdatesInterval { get; set; }
    }
}
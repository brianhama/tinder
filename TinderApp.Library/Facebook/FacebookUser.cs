using Newtonsoft.Json;

namespace TinderApp.Library.Facebook
{
    public class FacebookUser
    {
        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("interested_in")]
        public string[] InterestedIn { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }

        [JsonProperty("middle_name")]
        public string MiddleName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("political")]
        public string Political { get; set; }

        [JsonProperty("religion")]
        public string Religion { get; set; }

        [JsonProperty("timezone")]
        public int Timezone { get; set; }

        [JsonProperty("updated_time")]
        public string UpdatedTime { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }
    }
}
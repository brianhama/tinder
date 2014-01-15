using Newtonsoft.Json;

namespace TinderApp.Lib
{
    public class User
    {
        [JsonProperty("active_time")]
        public string ActiveTime { get; set; }

        [JsonProperty("age_filter_max")]
        public int AgeFilterMax { get; set; }

        [JsonProperty("age_filter_min")]
        public int AgeFilterMin { get; set; }

        [JsonProperty("api_token")]
        public string ApiToken { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("birth_date")]
        public string BirthDate { get; set; }

        [JsonProperty("create_date")]
        public string CreateDate { get; set; }

        [JsonProperty("distance_filter")]
        public int DistanceFilter { get; set; }

        [JsonProperty("distance_filter_min")]
        public int DistanceFilterMin { get; set; }

        [JsonProperty("facebook_id")]
        public string FacebookId { get; set; }

        [JsonProperty("facebook_token")]
        public string FacebookToken { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("gender")]
        public int Gender { get; set; }

        [JsonProperty("gender_filter")]
        public int GenderFilter { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("interested_in")]
        public int[] InterestedIn { get; set; }

        [JsonProperty("ip_address")]
        public string IpAddress { get; set; }

        [JsonProperty("last_fb_sync_date")]
        public string LastFbSyncDate { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pending")]
        public bool Pending { get; set; }

        [JsonProperty("photos")]
        public Photo[] Photos { get; set; }

        [JsonProperty("ping_time")]
        public string PingTime { get; set; }

        [JsonProperty("pos")]
        public Pos Pos { get; set; }

        [JsonProperty("user_number")]
        public int UserNumber { get; set; }
    }
}
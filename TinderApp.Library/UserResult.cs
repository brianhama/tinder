using Newtonsoft.Json;

namespace TinderApp.Lib
{
    public class UserResult
    {
        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("birth_date")]
        public string BirthDate { get; set; }

        [JsonProperty("birth_date_info")]
        public string BirthDateInfo { get; set; }

        [JsonProperty("common_friend_count")]
        public int CommonFriendCount { get; set; }

        [JsonProperty("common_friends")]
        public object[] CommonFriends { get; set; }

        [JsonProperty("common_like_count")]
        public int CommonLikeCount { get; set; }

        [JsonProperty("common_likes")]
        public string[] CommonLikes { get; set; }

        [JsonProperty("distance_mi")]
        public int DistanceMi { get; set; }

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

        [JsonProperty("id")]
        public string PrivateId { get; set; }
    }
}
using Newtonsoft.Json;

namespace TinderApp.Models
{
    public class UserResponse
    {
        [JsonProperty("results")]
        public UserResult Results { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
using Newtonsoft.Json;
using TinderApp.Lib;

namespace TinderApp.Library
{
    public class UserResponse
    {
        [JsonProperty("results")]
        public UserResult Results { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}
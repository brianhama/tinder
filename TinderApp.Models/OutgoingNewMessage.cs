using Newtonsoft.Json;

namespace TinderApp.Models
{
    public class OutgoingNewMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
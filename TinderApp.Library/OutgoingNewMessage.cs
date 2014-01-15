using Newtonsoft.Json;

namespace TinderApp.Library
{
    public class OutgoingNewMessage
    {
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
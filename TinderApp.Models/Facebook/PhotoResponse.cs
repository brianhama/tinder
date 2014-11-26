using Newtonsoft.Json;

namespace TinderApp.Models.Facebook
{
    public class PhotoResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
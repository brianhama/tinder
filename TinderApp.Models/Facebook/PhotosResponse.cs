using Newtonsoft.Json;

namespace TinderApp.Models.Facebook
{
    public class PhotosResponse
    {
        [JsonProperty("data")]
        public PhotoResponse[] Data { get; set; }
    }
}
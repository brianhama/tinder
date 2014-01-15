using Newtonsoft.Json;
using System.Threading.Tasks;
using TinderApp.Lib;
using TinderApp.Lib.API;

namespace TinderApp.Library
{
    public class ReccommendationsRequest
    {
        [JsonProperty("results")]
        public UserResult[] Results { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        public static async Task<ReccommendationsRequest> GetRecommendations()
        {
            return await Client.Get<ReccommendationsRequest>("user/recs");
        }
    }
}
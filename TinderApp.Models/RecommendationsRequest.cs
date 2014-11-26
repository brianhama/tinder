using Newtonsoft.Json;
using System.Threading.Tasks;
using TinderApp.TinderApi;

namespace TinderApp.Models
{
    public class ReccommendationsRequest
    {
        [JsonProperty("results")]
        public UserResult[] Results { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        public static async Task<ReccommendationsRequest> GetRecommendations()
        {
            return await Client.GetAsync<ReccommendationsRequest>("user/recs").ConfigureAwait(false);
        }
    }
}
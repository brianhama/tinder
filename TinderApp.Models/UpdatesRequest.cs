using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TinderApp.TinderApi;

namespace TinderApp.Models
{
    public class UpdatesRequest
    {
        [JsonProperty("last_activity_date")]
        public DateTime? LastActivityDate { get; set; }

        public async Task<UpdatesResponse> GetUpdate()
        {
            UpdatesResponse response = await Client.Post<UpdatesResponse>("updates", this);
            return response;
        }
    }
}
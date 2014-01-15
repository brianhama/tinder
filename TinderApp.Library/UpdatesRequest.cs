using System;
using System.Threading.Tasks;
using TinderApp.Lib.API;

namespace TinderApp.Lib
{
    public class UpdatesRequest
    {
        public DateTime? last_activity_date { get; set; }

        public async Task<UpdatesResponse> GetUpdate()
        {
            UpdatesResponse response = await Client.Post<UpdatesResponse>("updates", this);
            return response;
        }
    }
}
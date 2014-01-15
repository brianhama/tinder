using System.Threading.Tasks;
using TinderApp.Lib.API;

namespace TinderApp.Library
{
    public class PingRequest
    {
        public double lat { get; set; }

        public double lon { get; set; }

        public async Task Ping()
        {
            await Client.Post("user/ping", this);
        }
    }
}
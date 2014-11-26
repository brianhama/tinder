using System.Runtime.Serialization;
using System.Threading.Tasks;
using TinderApp.TinderApi;

namespace TinderApp.Models
{
    [DataContract]
    public class PingRequest
    {
        [DataMember(Name = "lat")]
        public double Latitude { get; set; }

        [DataMember(Name = "lon")]
        public double Longitude { get; set; }

        public async Task Ping()
        {
            await Client.Post("user/ping", this);
        }
    }
}
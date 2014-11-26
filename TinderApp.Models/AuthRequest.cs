using System.Runtime.Serialization;
using System.Threading.Tasks;
using TinderApp.TinderApi;

namespace TinderApp.Models
{
    [DataContract]
    public class AuthRequest
    {
        [DataMember(Name = "facebook_id")]
        public string FacebookID { get; set; }

        [DataMember(Name = "facebook_token")]
        public string FacebookToken { get; set; }

        public async Task<AuthResponse> Send()
        {
            AuthResponse response = await Client.Post<AuthResponse>("auth", this);
            Client.AuthToken = response.AuthToken;
            return response;
        }
    }
}
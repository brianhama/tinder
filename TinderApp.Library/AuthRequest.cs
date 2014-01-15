using System.Threading.Tasks;
using TinderApp.Lib.API;

namespace TinderApp.Lib
{
    public class AuthRequest
    {
        public string facebook_id { get; set; }

        public string facebook_token { get; set; }

        public async Task<AuthResponse> Send()
        {
            AuthResponse response = await Client.Post<AuthResponse>("auth", this);
            Client.AuthToken = response.token;
            return response;
        }
    }
}
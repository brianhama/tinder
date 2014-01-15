using Newtonsoft.Json;
using System.Threading.Tasks;
using TinderApp.Lib.Facebook;

namespace TinderApp.Library.Facebook
{
    public class FacebookUserResponse
    {
        [JsonProperty("results")]
        public FacebookUser Results { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        public async static Task<FacebookUser> GetFacebookUser(string accessToken)
        {
            var response = await FacebookClient.Get<FacebookUser>("me", accessToken);
            return response;
        }
    }
}
using Newtonsoft.Json;
using System.Threading.Tasks;
using TinderApp.Lib.Facebook;

namespace TinderApp.Models.Facebook
{
    public class FacebookUserResponse
    {
        [JsonProperty("results")]
        public FacebookUser Results { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        public async static Task<FacebookUser> GetFacebookUser(string accessToken)
        {
            var response = await FacebookClient.GetAsync<FacebookUser>("me", accessToken).ConfigureAwait(false);
            return response;
        }
    }
}
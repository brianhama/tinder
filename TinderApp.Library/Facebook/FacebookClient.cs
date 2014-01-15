using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TinderApp.Lib.Facebook
{
    public class FacebookClient
    {
        private static HttpClient _client = null;

        static FacebookClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://graph.facebook.com/");
        }

        public async static Task<T> Get<T>(string resource, string accessToken)
        {
            var response = await _client.GetAsync(resource + "?access_token=" + accessToken);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();

                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.DateParseHandling = DateParseHandling.DateTime;
                settings.DefaultValueHandling = DefaultValueHandling.Populate;
                settings.NullValueHandling = NullValueHandling.Include;
                settings.TypeNameHandling = TypeNameHandling.None;

                return JsonConvert.DeserializeObject<T>(responseData);
            }

            throw new Exception(response.ReasonPhrase);
        }
    }
}
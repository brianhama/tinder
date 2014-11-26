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

        public async static Task<T> GetAsync<T>(string resource, string accessToken)
        {
            if (resource.Contains("?"))
                resource = resource + "&access_token=" + accessToken;
            else
                resource = resource + "?access_token=" + accessToken;

            string responseData = await _client.GetStringAsync(resource);

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.DateParseHandling = DateParseHandling.DateTime;
            settings.DefaultValueHandling = DefaultValueHandling.Populate;
            settings.NullValueHandling = NullValueHandling.Include;
            settings.TypeNameHandling = TypeNameHandling.None;

            return JsonConvert.DeserializeObject<T>(responseData);
        }
    }
}
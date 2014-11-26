using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TinderApp.TinderApi
{
    public class ContentClient
    {
        private static HttpClient _client = null;

        static ContentClient()
        {
            _client = new HttpClient();

            /* Need to mimic the iOS client to avoid detection by Tinder's servers. */

            _client.DefaultRequestHeaders.Add("Accept-Language", "en;q=1");
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
            _client.DefaultRequestHeaders.Add("User-Agent", "Tinder/4.0.9 (iPhone; iOS 8.1.1; Scale/2.00)");
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            _client.DefaultRequestHeaders.Add("app-version", "123");
            _client.DefaultRequestHeaders.Add("os_version", "80000100001");
            _client.DefaultRequestHeaders.Add("platform", "ios");
            _client.BaseAddress = new Uri(Constants.BASE_URL);
        }

        public static String AuthToken
        {
            get
            {
                return Client.AuthToken;
            }
        }

        public async static Task<T> PostAsync<T>(string resource, object body)
        {
            string postData = JsonConvert.SerializeObject(body, new IsoDateTimeConverter());

            using (var response = await _client.PostAsync(resource, new StringContent(postData, Encoding.UTF8, "application/json")))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(responseData);
                    return JsonConvert.DeserializeObject<T>(responseData);
                }

                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
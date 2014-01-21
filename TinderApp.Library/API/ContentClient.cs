using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TinderApp.Lib.API
{
    public class ContentClient
    {
        private static HttpClient _client = null;

        static ContentClient()
        {
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept-Language", "en;q=1, fr;q=0.9, de;q=0.8, ja;q=0.7, nl;q=0.6, it;q=0.5");
            _client.DefaultRequestHeaders.Add("Accept", "*/*");
            _client.DefaultRequestHeaders.Add("UserAgent", "Tinder/3.0.2 (iPhone; iOS 7.1; Scale/2.00)");
            _client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
            _client.BaseAddress = new Uri(Constants.BASE_URL);
        }

        public static String AuthToken
        {
            get
            {
                return Client.AuthToken;
            }
        }

        public async static Task<T> Post<T>(string resource, object body)
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
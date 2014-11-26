using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TinderApp.TinderApi
{
    public class Client
    {
        private static string _authToken = "";

        private static HttpClient _client = null;

        static Client()
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
                return _authToken;
            }
            set
            {
                _authToken = value;
                if (string.IsNullOrEmpty(value))
                {
                    _client.DefaultRequestHeaders.Remove("Authorization");
                    _client.DefaultRequestHeaders.Remove("X-Auth-Token");
                }
                else
                {
                    _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", "token=\"" + value + "\"");
                    _client.DefaultRequestHeaders.Add("X-Auth-Token", value);
                }
            }
        }

        public async static Task<T> GetAsync<T>(string resource)
        {
            using (var response = await _client.GetAsync(GetCacheBustingResourceUri(resource)))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();

                    Debug.WriteLine(responseData);

                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.DateParseHandling = DateParseHandling.DateTime;
                    settings.DefaultValueHandling = DefaultValueHandling.Populate;
                    settings.NullValueHandling = NullValueHandling.Include;
                    settings.TypeNameHandling = TypeNameHandling.None;

                    return JsonConvert.DeserializeObject<T>(responseData);
                }

                response.EnsureSuccessStatusCode();
                return default(T);
            }
        }

        public async static Task GetAsync(string resource)
        {
            using (var response = await _client.GetAsync(GetCacheBustingResourceUri(resource)))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(responseData);
                    return;
                }

                response.EnsureSuccessStatusCode();
            }
        }

        public async static Task<T> Post<T>(string resource, object body)
        {
            string postData = JsonConvert.SerializeObject(body, new IsoDateTimeConverter());

            using (var response = await _client.PostAsync(GetCacheBustingResourceUri(resource), new StringContent(postData, Encoding.UTF8, "application/json")))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(responseData);
                    return JsonConvert.DeserializeObject<T>(responseData);
                }

                response.EnsureSuccessStatusCode();
                return default(T);
            }
        }

        public async static Task Post(string resource, object body)
        {
            string postData = JsonConvert.SerializeObject(body, new IsoDateTimeConverter());

            using (var response = await _client.PostAsync(GetCacheBustingResourceUri(resource), new StringContent(postData, Encoding.UTF8, "application/json")))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(responseData);
                    return;
                }

                response.EnsureSuccessStatusCode();
            }
        }

        public static void StopAllRequests()
        {
            _client.CancelPendingRequests();
        }

        internal static string GetCacheBustingResourceUri(string resource)
        {
            if (resource.Contains("?"))
                return resource + "&_=" + DateTime.UtcNow.Ticks.ToString();
            return resource + "?_=" + DateTime.UtcNow.Ticks.ToString();
        }
    }
}
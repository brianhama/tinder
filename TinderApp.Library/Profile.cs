using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinderApp.Lib.API;

namespace TinderApp.Lib
{
    public class Profile
    {
        public int age_filter_max { get; set; }

        public int age_filter_min { get; set; }

        public string bio { get; set; }

        public string birth_date { get; set; }

        public string create_date { get; set; }

        public int distance_filter { get; set; }

        public string facebook_id { get; set; }

        public int gender { get; set; }

        public int gender_filter { get; set; }

        [JsonProperty(PropertyName = "_id")]
        public string ID { get; set; }

        public List<int> interested_in { get; set; }

        public Location location { get; set; }

        public string name { get; set; }

        public List<Photo> photos { get; set; }

        public string ping_time { get; set; }

        public Pos pos { get; set; }

        public static async Task<Profile> GetProfile()
        {
            Profile response = await Client.Get<Profile>("profile");
            return response;
        }

        public async Task SaveProfile()
        {
            await Client.Post("profile", this);
        }
    }
}
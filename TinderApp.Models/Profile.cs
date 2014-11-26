using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinderApp.TinderApi;

namespace TinderApp.Models
{
    public class Profile
    {
        [JsonProperty("age_filter_max")]
        public int AgeFilterMaximum { get; set; }

        [JsonProperty("age_filter_min")]
        public int AgeFilterMinimum { get; set; }

        [JsonProperty("bio")]
        public string BiographyText { get; set; }

        [JsonProperty("create_date")]
        public string CreationDate { get; set; }

        [JsonProperty("location")]
        public Location CurrentLocation { get; set; }

        [JsonProperty("birth_date")]
        public string DateOfBirth { get; set; }

        [JsonProperty("distance_filter")]
        public int DistanceFilter { get; set; }

        [JsonProperty("facebook_id")]
        public string FacebookUserID { get; set; }

        [JsonProperty("gender_filter")]
        public int GenderFilter { get; set; }

        [JsonProperty("gender")]
        public int GenderID { get; set; }

        [JsonProperty(PropertyName = "_id")]
        public string ID { get; set; }

        [JsonProperty("interested_in")]
        public List<int> InterestedInFilter { get; set; }

        [JsonProperty("ping_time")]
        public string LastPingTime { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }

        [JsonProperty("pos")]
        public Position Position { get; set; }

        public static async Task<Profile> GetProfile()
        {
            Profile response = await Client.GetAsync<Profile>("profile").ConfigureAwait(false);
            return response;
        }

        public async Task SaveProfile()
        {
            await Client.Post("profile", this);
        }
    }
}
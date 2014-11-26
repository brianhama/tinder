using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinderApp.TinderApi;

namespace TinderApp.Models
{
    public class SettingsUpdate
    {
        [JsonProperty("age_filter_max")]
        public int AgeFilterMaximum { get; set; }

        [JsonProperty("age_filter_min")]
        public int AgeFilterMinimum { get; set; }

        [JsonProperty("distance_filter")]
        public int DistanceFilter { get; set; }

        [JsonProperty("gender")]
        public int GenderID { get; set; }

        [JsonProperty("interested_in")]
        public List<int> InterestedInFilter { get; set; }

        public async Task SaveSettings()
        {
            await Client.Post("profile", this);
        }
    }
}
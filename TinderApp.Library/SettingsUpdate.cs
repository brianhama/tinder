using System.Collections.Generic;
using System.Threading.Tasks;
using TinderApp.Lib.API;

namespace TinderApp.Library
{
    public class SettingsUpdate
    {
        public int age_filter_max { get; set; }

        public int age_filter_min { get; set; }

        public int distance_filter { get; set; }

        public int gender { get; set; }

        public List<int> interested_in { get; set; }

        public async Task SaveSettings()
        {
            await Client.Post("profile", this);
        }
    }
}
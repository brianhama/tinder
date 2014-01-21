using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderApp.Library
{
    public class AddPhotoRequest
    {

        [JsonProperty("transmit")]
        public string Transmit { get; set; }

        [JsonProperty("assets")]
        public Asset[] Assets { get; set; }
    }

    public class Asset
    {

        [JsonProperty("xdistance_percent")]
        public int XdistancePercent { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("xoffset_percent")]
        public int XoffsetPercent { get; set; }

        [JsonProperty("yoffset_percent")]
        public double YoffsetPercent { get; set; }

        [JsonProperty("ydistance_percent")]
        public double YdistancePercent { get; set; }

        [JsonProperty("main")]
        public bool Main { get; set; }
    }
}

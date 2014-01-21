using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderApp.Library.Facebook
{
    public class PhotosResponse
    {
        [JsonProperty("data")]
        public PhotoResponse[] Data { get; set; }
    }
}

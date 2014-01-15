using System.Runtime.Serialization;

namespace TinderApp.Lib
{
    [DataContract]
    public class Location
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
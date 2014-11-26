using System.Runtime.Serialization;

namespace TinderApp.Models
{
    [DataContract]
    public class AuthResponse
    {
        [DataMember(Name = "token")]
        public string AuthToken { get; set; }

        [DataMember(Name = "globals")]
        public Globals GlobalVariables { get; set; }

        [DataMember(Name = "user")]
        public User UserProfile { get; set; }

        [DataMember(Name = "versions")]
        public Versions VersionInfo { get; set; }
    }
}
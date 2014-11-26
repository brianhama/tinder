using System;
using System.Collections.Generic;
using TinderApp.Models.Facebook;

namespace TinderApp.Models
{
    public class TombstoneData
    {
        public TombstoneData()
        {
        }

        public String AuthToken { get; set; }

        public Globals CurrentGlobals { get; set; }

        public Profile CurrentProfile { get; set; }

        public User CurrentUser { get; set; }

        public FacebookSessionInfo FBSession { get; set; }

        public DateTime? LastActivity { get; set; }

        public Position Location { get; set; }

        public List<Match> Matches { get; set; }

        public List<UserResult> Recommendations { get; set; }
    }
}
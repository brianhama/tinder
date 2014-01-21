using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinderApp.Lib;

namespace TinderApp.Library
{
    public class TombstoneData
    {
        public TombstoneData()
        {
        }

        public String AuthToken { get; set; }

        public FacebookSessionInfo FBSession { get; set; }

        public GeographicalCordinates Location { get; set; }

        public Profile CurrentProfile { get; set; }

        public User CurrentUser { get; set; }

        public Globals CurrentGlobals { get; set; }

        public List<UserResult> Recommendations { get; set; }

        public DateTime? LastActivity { get; set; }

        public List<Match> Matches { get; set; }
    }
}

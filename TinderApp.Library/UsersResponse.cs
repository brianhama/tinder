using System.Collections.Generic;

namespace TinderApp.Lib
{
    public class UsersResponse
    {
        public List<UserResult> results { get; set; }

        public int status { get; set; }
    }
}
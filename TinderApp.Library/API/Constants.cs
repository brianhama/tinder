using System;

namespace TinderApp.Lib.API
{
    public class Constants
    {
        public const string BASE_URL = @"https://api.gotinder.com/";

        public static string GetURL(string relativeUrl)
        {
            return String.Format("{0}{1}", BASE_URL, relativeUrl);
        }
    }
}
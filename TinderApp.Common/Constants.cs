using System;

namespace TinderApp.Common
{
    public class Constants
    {
        public const string BASE_URL = @"https://api.gotinder.com/";
        public const string CONTENT_BASE_URL = @"https://content.gotinder.com/";

        public static string GetURL(string relativeUrl)
        {
            return String.Format("{0}{1}", BASE_URL, relativeUrl);
        }
    }
}
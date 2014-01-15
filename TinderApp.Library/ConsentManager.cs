using System;

namespace TinderApp.Library
{
    public class Consent
    {
        public Boolean IsConsented { get; set; }
    }

    public class ConsentManager
    {
        public static Boolean HasConsented
        {
            get
            {
                try
                {
                    return Coding4Fun.Toolkit.Storage.Serializer.Open<Consent>("HasConsented").IsConsented;
                }
                catch
                {
                    return false;
                }
            }
            set
            {
                if (value)
                {
                    Coding4Fun.Toolkit.Storage.Serializer.Save<Consent>("HasConsented", new Consent() { IsConsented = true });
                }
            }
        }
    }
}
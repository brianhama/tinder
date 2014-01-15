namespace TinderApp.Library
{
    public class Settings
    {
        public string FacebookId { get; set; }

        public string FacebookToken { get; set; }

        public static Settings Load()
        {
            try
            {
                return Coding4Fun.Toolkit.Storage.Serializer.Open<Settings>("Settings");
            }
            catch
            {
                return new Settings();
            }
        }

        public void Save()
        {
            Coding4Fun.Toolkit.Storage.Serializer.Save<Settings>("Settings", this);
        }
    }
}
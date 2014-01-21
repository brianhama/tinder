using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderApp.Library
{
    public static class TombstoneManager
    {
        public static void Save(TombstoneData data)
        {
            Coding4Fun.Toolkit.Storage.Serializer.Save<TombstoneData>("TombstoneState", data);
        }

        public static TombstoneData Load()
        {
            var data = Coding4Fun.Toolkit.Storage.Serializer.Open<TombstoneData>("TombstoneState");
            if (!String.IsNullOrEmpty(data.AuthToken) && data.FBSession != null && data.Location != null && data.CurrentUser != null)
                return data;
            return null;
        }

        public static void Delete()
        {
            Coding4Fun.Toolkit.Storage.PlatformFileAccess.DeleteFile("TombstoneState");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinderApp.Library.MVVM;

namespace TinderApp.Library.ViewModels
{
    public class FacebookAlbumPhotoViewModel : ObservableObject
    {
        public FacebookAlbumPhotoViewModel(string id)
        {
            ID = id;
        }

        public string ID { get; private set; }

        public Uri ImageUri {
            get
            {
                return new Uri(String.Format("https://graph.facebook.com/{0}/picture?access_token={1}&width=200&height=200", ID, TinderSession.CurrentSession.FbSessionInfo.FacebookToken), UriKind.Absolute);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TinderApp.Lib.Facebook;
using TinderApp.Library.Facebook;
using TinderApp.Library.MVVM;

namespace TinderApp.Library.ViewModels
{
    public class FacebookAlbumsViewModel : ObservableObject
    {
        private ObservableCollection<FacebookAlbumPhotoViewModel> _photos = new ObservableCollection<FacebookAlbumPhotoViewModel>();

        public ObservableCollection<FacebookAlbumPhotoViewModel> Photos
        {
            get { return _photos; }
            set { _photos = value; }
        }

        public async Task GetPhotos()
        {
            var photosResponse = await FacebookClient.Get<PhotosResponse>("me/photos?fields=id&limit=50", TinderSession.CurrentSession.FbSessionInfo.FacebookToken);
            foreach (var photo in photosResponse.Data)
            {
                _photos.Add(new FacebookAlbumPhotoViewModel(photo.Id));
            }
            base.RaisePropertyChanged("Photos");
        }
    }
}

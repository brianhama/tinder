using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TinderApp.Lib.Facebook;
using TinderApp.Library.MVVM;
using TinderApp.Models.Facebook;

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
            var photosResponse = await FacebookClient.GetAsync<PhotosResponse>("me/photos?fields=id&limit=50", TinderSession.CurrentSession.FbSessionInfo.FacebookToken).ConfigureAwait(false);
            foreach (var photo in photosResponse.Data)
            {
                _photos.Add(new FacebookAlbumPhotoViewModel(photo.Id));
            }
            base.RaisePropertyChanged("Photos");
        }
    }
}
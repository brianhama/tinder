using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TinderApp.Library.ViewModels;
using TinderApp.Library;
using TinderApp.Lib.API;
using TinderApp.Lib;

namespace TinderApp
{
    public partial class FacebookAlbumsPage : PhoneApplicationPage
    {
        private Boolean _main;
        private FacebookAlbumsViewModel _viewModel;

        public FacebookAlbumsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this.LayoutUpdated += FacebookAlbumsPage_LayoutUpdated;

            if (NavigationContext.QueryString["main"] == "true")
                _main = true;
        }

        private async void FacebookAlbumsPage_LayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= FacebookAlbumsPage_LayoutUpdated;

            _viewModel = new FacebookAlbumsViewModel();
            photoList.DataContext = _viewModel;

            await _viewModel.GetPhotos();

            photoList.ItemsSource = _viewModel.Photos;
        }

        private async void photoList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 1)
            {
                var photo = e.AddedItems[0] as FacebookAlbumPhotoViewModel;
                AddPhotoRequest request = new AddPhotoRequest();
                request.Transmit = "fb";

                Asset asset = new Asset() { Id = photo.ID, Main = _main, XdistancePercent = 1, YdistancePercent = 1, XoffsetPercent = 0, YoffsetPercent = 0 };

                request.Assets = new Asset[] { asset };

                TinderSession.CurrentSession.CurrentProfile.photos = await ContentClient.Post<List<Photo>>("media", request);

                NavigationService.GoBack();
            }
        }
    }
}
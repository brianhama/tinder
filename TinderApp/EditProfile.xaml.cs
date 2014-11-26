using Microsoft.Phone.Controls;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using TinderApp.Library;
using TinderApp.Models;

namespace TinderApp
{
    public partial class EditProfile : PhoneApplicationPage
    {
        public EditProfile()
        {
            InitializeComponent();
        }

        protected async override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            BioUpdate bioUpdate = new BioUpdate();
            bioUpdate.Bio = TinderSession.CurrentSession.CurrentProfile.BiographyText;
            await bioUpdate.SaveProfile();

            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.LayoutUpdated += SettingsPage_LayoutUpdated;
            base.OnNavigatedTo(e);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/FacebookAlbumsPage.xaml?main=true", UriKind.Relative));
        }

        private void SettingsPage_LayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= SettingsPage_LayoutUpdated;

            DataContext = TinderSession.CurrentSession.CurrentProfile;

            if (TinderSession.CurrentSession.CurrentProfile.Photos.Count >= 1)
                this.image1.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.Photos[0].Url, UriKind.Absolute));

            if (TinderSession.CurrentSession.CurrentProfile.Photos.Count >= 2)
                this.image2.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.Photos[1].Url, UriKind.Absolute));

            if (TinderSession.CurrentSession.CurrentProfile.Photos.Count >= 3)
                this.image3.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.Photos[2].Url, UriKind.Absolute));

            if (TinderSession.CurrentSession.CurrentProfile.Photos.Count >= 4)
                this.image4.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.Photos[3].Url, UriKind.Absolute));

            if (TinderSession.CurrentSession.CurrentProfile.Photos.Count >= 5)
                this.image5.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.Photos[4].Url, UriKind.Absolute));

            if (TinderSession.CurrentSession.CurrentProfile.Photos.Count >= 6)
                this.image6.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.Photos[5].Url, UriKind.Absolute));
        }
    }
}
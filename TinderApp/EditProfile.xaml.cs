using Microsoft.Phone.Controls;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using TinderApp.Library;

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
            bioUpdate.Bio = TinderSession.CurrentSession.CurrentProfile.bio;
            await bioUpdate.SaveProfile();

            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.LayoutUpdated += SettingsPage_LayoutUpdated;
            base.OnNavigatedTo(e);
        }

        private void SettingsPage_LayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= SettingsPage_LayoutUpdated;

            DataContext = TinderSession.CurrentSession.CurrentProfile;

            if (TinderSession.CurrentSession.CurrentProfile.photos.Count >= 1)
                this.image1.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.photos[0].Url, UriKind.Absolute));

            if (TinderSession.CurrentSession.CurrentProfile.photos.Count >= 2)
                this.image2.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.photos[1].Url, UriKind.Absolute));

            if (TinderSession.CurrentSession.CurrentProfile.photos.Count >= 3)
                this.image3.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.photos[2].Url, UriKind.Absolute));

            if (TinderSession.CurrentSession.CurrentProfile.photos.Count >= 4)
                this.image4.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.photos[3].Url, UriKind.Absolute));

            if (TinderSession.CurrentSession.CurrentProfile.photos.Count >= 5)
                this.image5.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.photos[4].Url, UriKind.Absolute));

            if (TinderSession.CurrentSession.CurrentProfile.photos.Count >= 6)
                this.image6.Source = new BitmapImage(new Uri(TinderSession.CurrentSession.CurrentProfile.photos[5].Url, UriKind.Absolute));
        }
    }
}
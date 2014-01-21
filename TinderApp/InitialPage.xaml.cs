using Microsoft.Phone.Controls;
using System;
using System.Device.Location;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using TinderApp.Library;
using TinderApp.Library.Facebook;
using TinderApp.Library.MVVM;
using Windows.Devices.Geolocation;

namespace TinderApp
{
    public partial class InitialPage : PhoneApplicationPage
    {
        public InitialPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            webBrowser.Navigating += webBrowser_Navigating;
            this.LayoutUpdated += InitialPage_LayoutUpdated;

            Open.Completed += Open_Completed;

            base.OnNavigatedTo(e);
        }

        void Open_Completed(object sender, EventArgs e)
        {
            if (ConsentManager.HasConsented)
                LoginButtonBorder.Visibility = Visibility.Visible;
            else
                TermsBorder.Visibility = System.Windows.Visibility.Visible;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            webBrowser.Navigating -= webBrowser_Navigating;
            Open.Completed -= Open_Completed;
            base.OnNavigatingFrom(e);
        }

        private void agreeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TermsBorder.Visibility = Visibility.Collapsed;
            ConsentManager.HasConsented = true;
            LoginButtonBorder.Visibility = Visibility.Visible;
        }

        private async System.Threading.Tasks.Task Authenticate(string accessToken, string fbid)
        {
            ProfilePhoto.Background = new ImageBrush() { ImageSource = new BitmapImage(new Uri(String.Format("https://graph.facebook.com/me/picture?access_token={0}&height=100&width=100", accessToken))) };

            FacebookSessionInfo sessionInfo = new FacebookSessionInfo();
            sessionInfo.FacebookToken = accessToken;
            sessionInfo.FacebookID = fbid;

            Geolocator location = new Geolocator();
            location.DesiredAccuracy = PositionAccuracy.Default;
            var usrLocation = await location.GetGeopositionAsync();

            TinderSession activeSession = TinderSession.CreateNewSession(sessionInfo, new GeographicalCordinates() { Latitude = usrLocation.Coordinate.Latitude, Longitude = usrLocation.Coordinate.Longitude });
            if (await activeSession.Authenticate())
            {
                (App.Current as App).RightSideBar.DataContext = activeSession.Matches;

                TopBarViewModel.ShowTopButtons = System.Windows.Visibility.Visible;

                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

                App.RootFrame.RemoveBackEntry();
            }
        }

        private void FacebookLoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButtonBorder.Visibility = System.Windows.Visibility.Collapsed;
            WebViewBorder.Visibility = System.Windows.Visibility.Visible;

            webBrowser.Navigate(new Uri("https://www.facebook.com/dialog/oauth?client_id=464891386855067&redirect_uri=https://www.facebook.com/connect/login_success.html&scope=basic_info,email,public_profile,user_about_me,user_activities,user_birthday,user_education_history,user_friends,user_interests,user_likes,user_location,user_photos,user_relationship_details&response_type=token", UriKind.Absolute));
        }

        private void InitialPage_LayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= InitialPage_LayoutUpdated;

            if (ConsentManager.HasConsented)
            {
                if (!(App.Current as App).JustLoggedOut)
                {
                    TombstoneData data = TombstoneManager.Load();
                    if (data != null)
                    {
                        TinderSession activeSession = TinderSession.FromTombstoneData(data);
                        (App.Current as App).RightSideBar.DataContext = activeSession.Matches;

                        TopBarViewModel.ShowTopButtons = System.Windows.Visibility.Visible;

                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

                        App.RootFrame.RemoveBackEntry();

                        return;
                    }
                }
                else
                {
                    LoginButtonBorder.Visibility = System.Windows.Visibility.Visible;
                }
            }

            if (!(App.Current as App).JustLoggedOut)
                Open.Begin();
        }

        private async void webBrowser_Navigating(object sender, NavigatingEventArgs e)
        {
            if (e.Uri.ToString().StartsWith("https://www.facebook.com/connect/login_success.html"))
            {
                e.Cancel = true;

                WebViewBorder.Visibility = System.Windows.Visibility.Collapsed;
                LoginButtonBorder.Visibility = System.Windows.Visibility.Collapsed;
                LoggedInPanel.Visibility = System.Windows.Visibility.Visible;

                if (Pulsate.GetCurrentState() != ClockState.Active)
                {
                    Pulsate.RepeatBehavior = RepeatBehavior.Forever;
                    Pulsate.Begin();
                }

                FacebookUser user = null;
                string accessToken = "";

                try
                {

                    accessToken = e.Uri.ToString().Substring(e.Uri.ToString().IndexOf("access_token=") + "access_token=".Length);
                    if (accessToken.IndexOf("&") > 0)
                        accessToken = accessToken.Substring(0, accessToken.IndexOf("&"));

                    user = await FacebookUserResponse.GetFacebookUser(accessToken);

                }
                catch { }

                if (user == null)
                {
                    if (Pulsate.GetCurrentState() == ClockState.Active)
                    {
                        Pulsate.Stop();
                    }
                    await webBrowser.ClearCookiesAsync();
                    WebViewBorder.Visibility = System.Windows.Visibility.Collapsed;
                    LoginButtonBorder.Visibility = System.Windows.Visibility.Visible;
                    LoggedInPanel.Visibility = System.Windows.Visibility.Collapsed;
                    MessageBox.Show("Unable to login using Facebook.  Please try again.");
                }
                else
                    await Authenticate(accessToken, user.Id);
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (webBrowser.Visibility == System.Windows.Visibility.Visible)
            {
                LoginButtonBorder.Visibility = System.Windows.Visibility.Visible;
                webBrowser.Visibility = System.Windows.Visibility.Collapsed;
                e.Cancel = true;
            }

            base.OnBackKeyPress(e);
        }
    }
}
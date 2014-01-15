using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using TinderApp.Library;
using TinderApp.Library.Facebook;
using TinderApp.Library.MVVM;

namespace TinderApp
{
    public partial class InitialPage : PhoneApplicationPage
    {
        private GeographicalCordinates _location;
        private LocationManager _locationManager;

        public InitialPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            webBrowser.Navigating += webBrowser_Navigating;
            this.LayoutUpdated += InitialPage_LayoutUpdated;

            _locationManager = new LocationManager();
            _locationManager.OnPositionDetermined += _locationManager_OnPositionDetermined;

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            webBrowser.Navigating -= webBrowser_Navigating;
            _locationManager.OnPositionDetermined -= _locationManager_OnPositionDetermined;
            _locationManager = null;
            base.OnNavigatingFrom(e);
        }

        private async void _locationManager_OnPositionDetermined(object sender, PositionDeterminedEventArgs ca)
        {
            if (ca.IsPermissionFailure || ca.IsOtherFailure)
                MessageBox.Show("Unable to determine your location.  Please ensure that location services are enabled.");
            else
            {
                _location = ca.Location;

                Settings settings = Settings.Load();

                if (!String.IsNullOrEmpty(settings.FacebookToken) && !String.IsNullOrEmpty(settings.FacebookId))
                {
                    await Authenticate(settings);
                }
                else
                {
                    LoginButtonBorder.Visibility = Visibility.Visible;
                }
            }
        }

        private void agreeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TermsBorder.Visibility = Visibility.Collapsed;
            ConsentManager.HasConsented = true;

            if (Pulsate.GetCurrentState() != ClockState.Active)
            {
                Pulsate.RepeatBehavior = RepeatBehavior.Forever;
                Pulsate.Begin();
            }

            _locationManager.BeginGetLocation();
        }

        private async System.Threading.Tasks.Task Authenticate(Settings settings)
        {
            FacebookSessionInfo sessionInfo = new FacebookSessionInfo();
            sessionInfo.FacebookToken = settings.FacebookToken;
            sessionInfo.FacebookID = settings.FacebookId;

            TinderSession activeSession = TinderSession.CreateNewSession(sessionInfo, _location);
            if (await activeSession.Authenticate())
            {
                (App.Current as App).RightSideBar.DataContext = activeSession.Matches;

                TopBarViewModel.ShowTopButtons = System.Windows.Visibility.Visible;

                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

                App.RootFrame.RemoveBackEntry();
            }
        }

        private void downloadNearbyBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Phone.Tasks.MarketplaceDetailTask task = new Microsoft.Phone.Tasks.MarketplaceDetailTask();
            task.ContentIdentifier = "e0b331a9-85e8-df11-9264-00237de2db9e";
            task.ContentType = Microsoft.Phone.Tasks.MarketplaceContentType.Applications;
            task.Show();
        }

        private void FacebookLoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButtonBorder.Visibility = System.Windows.Visibility.Collapsed;
            downloadNearbyBtn.Visibility = System.Windows.Visibility.Collapsed;
            WebViewBorder.Visibility = System.Windows.Visibility.Visible;

            webBrowser.Navigate(new Uri("https://www.facebook.com/dialog/oauth?client_id=464891386855067&redirect_uri=https://www.facebook.com/connect/login_success.html&scope=basic_info,email,public_profile,user_about_me,user_activities,user_birthday,user_education_history,user_friends,user_interests,user_likes,user_location,user_photos,user_relationship_details&response_type=token", UriKind.Absolute));
        }

        private void InitialPage_LayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= InitialPage_LayoutUpdated;

            if (ConsentManager.HasConsented)
            {
                if (Pulsate.GetCurrentState() != ClockState.Active)
                {
                    Pulsate.RepeatBehavior = RepeatBehavior.Forever;
                    Pulsate.Begin();
                }

                _locationManager.BeginGetLocation();
            }
            else
                TermsBorder.Visibility = Visibility.Visible;
        }

        private async void webBrowser_Navigating(object sender, NavigatingEventArgs e)
        {
            if (e.Uri.ToString().StartsWith("https://www.facebook.com/connect/login_success.html"))
            {
                e.Cancel = true;

                WebViewBorder.Visibility = System.Windows.Visibility.Collapsed;

                LoginButtonBorder.Visibility = System.Windows.Visibility.Collapsed;

                if (Pulsate.GetCurrentState() != ClockState.Active)
                {
                    Pulsate.RepeatBehavior = RepeatBehavior.Forever;
                    Pulsate.Begin();
                }

                string accessToken = e.Uri.ToString().Substring(e.Uri.ToString().IndexOf("access_token=") + "access_token=".Length);
                if (accessToken.IndexOf("&") > 0)
                    accessToken = accessToken.Substring(0, accessToken.IndexOf("&"));

                var user = await FacebookUserResponse.GetFacebookUser(accessToken);

                Settings settings = new Settings();
                settings.FacebookId = user.Id;
                settings.FacebookToken = accessToken;
                settings.Save();

                await Authenticate(settings);
            }
        }
    }
}
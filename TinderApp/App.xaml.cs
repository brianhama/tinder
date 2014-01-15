using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using TinderApp.Library;
using TinderApp.Library.MVVM;

namespace TinderApp
{
    public partial class App : Application
    {
        public App()
        {
            TopBarViewModel.ShowTopButtons = Visibility.Collapsed;
            UnhandledException += Application_UnhandledException;
            InitializeComponent();

            InitializePhoneApplication();

            ThemeManager.ToLightTheme();
        }

        public static PhoneApplicationFrame RootFrame { get; private set; }

        public RightSideBarControl RightSideBar { get; set; }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        private void Home_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            VisualStateManager.GoToState(Application.Current.RootVisual as PhoneApplicationFrame, "Default", true);
            if (TinderSession.CurrentSession != null)
                RootFrame.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void privacyPolicy_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Microsoft.Phone.Tasks.WebBrowserTask webTask = new Microsoft.Phone.Tasks.WebBrowserTask();
            webTask.Uri = new Uri("http://www.gotinder.com/privacy/", UriKind.Absolute);
            webTask.Show();
        }

        private void profileButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            VisualStateManager.GoToState(Application.Current.RootVisual as PhoneApplicationFrame, "Default", true);
            if (TinderSession.CurrentSession != null)
                RootFrame.Navigate(new Uri("/EditProfile.xaml", UriKind.Relative));
        }

        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debugger.Break();
            }
        }

        #region Phone application initialization

        private bool phoneApplicationInitialized = false;

        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Reset)
                RootFrame.Navigated += ClearBackStackAfterReset;
        }

        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            RootFrame.Navigated -= ClearBackStackAfterReset;

            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            while (RootFrame.RemoveBackEntry() != null)
            {
                ; // do nothing
            }
        }

        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            RootFrame = new PhoneApplicationFrame();
            RootFrame.SetValue(PhoneApplicationFrame.StyleProperty, Application.Current.Resources["CustomFrame"]);
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            RootFrame.Navigated += CheckForResetNavigation;

            phoneApplicationInitialized = true;
        }

        #endregion Phone application initialization

        private void settingsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            VisualStateManager.GoToState(Application.Current.RootVisual as PhoneApplicationFrame, "Default", true);
            if (TinderSession.CurrentSession != null)
                RootFrame.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }
    }
}
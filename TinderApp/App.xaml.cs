using Microsoft.Phone.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;
using TinderApp.Lib.API;
using TinderApp.Library;
using TinderApp.Library.Controls;
using TinderApp.Library.MVVM;

namespace TinderApp
{
    public partial class App : Application, IApp
    {
        public App()
        {
            TopBarViewModel.ShowTopButtons = Visibility.Collapsed;
            UnhandledException += Application_UnhandledException;
            InitializeComponent();

            InitializePhoneApplication();

            ThemeManager.ToLightTheme();
        }

        public static CustomPhoneApplicationFrame RootFrame { get; private set; }

        public CustomPhoneApplicationFrame RootFrameInstance { get { return RootFrame; } }

        public RightSideBarControl RightSideBar { get; set; }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {

            if (e.ExceptionObject.Message.Contains("Unauthorized"))
            {
                Logout();
                return;
            }

            Console.WriteLine("Exception: " + e.ExceptionObject.Message);

            e.Handled = true;
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

            RootFrame = new CustomPhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            RootFrame.SetValue(CustomPhoneApplicationFrame.StyleProperty, Application.Current.Resources["CustomFrame"]);

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

        private void PhoneApplicationService_Activated(object sender, Microsoft.Phone.Shell.ActivatedEventArgs e)
        {
            if (!e.IsApplicationInstancePreserved)
            {
                TombstoneData data = TombstoneManager.Load();
                if (data != null)
                    TinderSession.FromTombstoneData(data);
            }
        }

        private void PhoneApplicationService_Closing(object sender, Microsoft.Phone.Shell.ClosingEventArgs e)
        {
            if (TinderSession.CurrentSession.IsAuthenticated)
                TombstoneManager.Save(TinderSession.CurrentSession.ToTombstoneData());
        }

        private void PhoneApplicationService_Deactivated(object sender, Microsoft.Phone.Shell.DeactivatedEventArgs e)
        {
            if (TinderSession.CurrentSession.IsAuthenticated)
                TombstoneManager.Save(TinderSession.CurrentSession.ToTombstoneData());
        }

        public void ShowTopBar()
        {
            RootFrame.ViewModel.TopBarVisible = true;
        }

        public Boolean JustLoggedOut { get; set; }

        public void Logout()
        {
            JustLoggedOut = true;
            Client.StopAllRequests();
            TinderSession.CurrentSession.Logout();
            TombstoneManager.Delete();
            RootFrame.ViewModel.TopBarVisible = false;
            RootFrameInstance.Navigate(new Uri("/InitialPage.xaml", UriKind.Relative));
            while (RootFrameInstance.CanGoBack)
                RootFrameInstance.RemoveBackEntry();
        }
    }
}
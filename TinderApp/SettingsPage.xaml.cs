using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Navigation;
using TinderApp.Library;

namespace TinderApp
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected async override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            TinderSession.CurrentSession.CurrentProfile.interested_in.Clear();

            if (this.showMenCheckbox.IsChecked.Value)
                TinderSession.CurrentSession.CurrentProfile.interested_in.Add(0);

            if (this.showWomenCheckBox.IsChecked.Value)
                TinderSession.CurrentSession.CurrentProfile.interested_in.Add(1);

            SettingsUpdate settingsUpdate = new SettingsUpdate();
            settingsUpdate.age_filter_max = (Int32)maxAge.Value;
            settingsUpdate.age_filter_min = (Int32)minAge.Value;
            settingsUpdate.gender = genderDropDown.SelectedIndex;
            settingsUpdate.interested_in = TinderSession.CurrentSession.CurrentProfile.interested_in;
            await settingsUpdate.SaveSettings();

            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.LayoutUpdated += SettingsPage_LayoutUpdated;
            base.OnNavigatedTo(e);
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            TinderSession.CurrentSession.Logout();
            NavigationService.Navigate(new Uri("/InitialPage.xaml", UriKind.Relative));
        }

        private void SettingsPage_LayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= SettingsPage_LayoutUpdated;

            DataContext = TinderSession.CurrentSession.CurrentProfile;

            if (TinderSession.CurrentSession.CurrentProfile.interested_in.Contains(0))
                this.showMenCheckbox.IsChecked = true;

            if (TinderSession.CurrentSession.CurrentProfile.interested_in.Contains(1))
                this.showWomenCheckBox.IsChecked = true;
        }
    }
}
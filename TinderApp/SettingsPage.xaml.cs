using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Navigation;
using TinderApp.Library;
using TinderApp.Models;

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
            TinderSession.CurrentSession.CurrentProfile.InterestedInFilter.Clear();

            if (this.showMenCheckbox.IsChecked.Value)
                TinderSession.CurrentSession.CurrentProfile.InterestedInFilter.Add(0);

            if (this.showWomenCheckBox.IsChecked.Value)
                TinderSession.CurrentSession.CurrentProfile.InterestedInFilter.Add(1);

            SettingsUpdate settingsUpdate = new SettingsUpdate();
            settingsUpdate.AgeFilterMaximum = (Int32)maxAge.Value;
            settingsUpdate.AgeFilterMinimum = (Int32)minAge.Value;
            settingsUpdate.GenderID = genderDropDown.SelectedIndex;
            settingsUpdate.InterestedInFilter = TinderSession.CurrentSession.CurrentProfile.InterestedInFilter;
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
            (App.Current as App).Logout();
        }

        private void SettingsPage_LayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= SettingsPage_LayoutUpdated;

            DataContext = TinderSession.CurrentSession.CurrentProfile;

            if (TinderSession.CurrentSession.CurrentProfile.InterestedInFilter.Contains(0))
                this.showMenCheckbox.IsChecked = true;

            if (TinderSession.CurrentSession.CurrentProfile.InterestedInFilter.Contains(1))
                this.showWomenCheckBox.IsChecked = true;
        }
    }
}
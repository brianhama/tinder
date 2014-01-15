using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Navigation;
using TinderApp.Views;

namespace TinderApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private UserReccommendationsViewModel _viewModel;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            _viewModel = new UserReccommendationsViewModel();
            DataContext = _viewModel;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.OnMatch -= _viewModel_OnMatch;

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.OnMatch += _viewModel_OnMatch;
            _viewModel.OnAnimation += _viewModel_OnAnimation;

            base.OnNavigatedTo(e);
        }

        private void _viewModel_OnAnimation(object sender, UserReccommendationsViewModel.AnimationEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                switch (e.AnimationName)
                {
                    case "Like":
                        LikeAnimation.Begin();
                        break;

                    case "Pass":
                        PassAnimation.Begin();
                        break;
                }
            });
        }

        private void _viewModel_OnMatch(object sender, EventArgs e)
        {
            matchBorder.Visibility = Visibility.Visible;
            ShowMatch.Begin();
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ProfileInfoPage.Data = _viewModel.CurrentReccomendation;

            NavigationService.Navigate(new Uri("/ProfileInfoPage.xaml?id=data", UriKind.Relative));
        }

        private void keepPlayingButton_Click(object sender, RoutedEventArgs e)
        {
            matchBorder.Visibility = System.Windows.Visibility.Collapsed;
            _viewModel.NextRecommendation();
        }

        private void sendMessageBtn_Click(object sender, RoutedEventArgs e)
        {
            matchBorder.Visibility = System.Windows.Visibility.Collapsed;

            string currentId = _viewModel.CurrentReccomendation.Id;

            _viewModel.NextRecommendation();
            NavigationService.Navigate(new Uri("/ViewConversationPage.xaml?id=" + currentId, UriKind.Relative));
        }
    }
}
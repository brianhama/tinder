using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using TinderApp.Views;

namespace TinderApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private UserRecommendationsViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();

            _viewModel = new UserRecommendationsViewModel();
            DataContext = _viewModel;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.OnMatch -= _viewModel_OnMatch;

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            (App.Current as App).ShowTopBar();

            _viewModel.OnMatch += _viewModel_OnMatch;
            _viewModel.OnAnimation += _viewModel_OnAnimation;

            base.OnNavigatedTo(e);
        }

        private void _viewModel_OnAnimation(object sender, UserRecommendationsViewModel.AnimationEventArgs e)
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

        private double GetAngle(double x, double y)
        {
            // Note that this function works in xaml coordinates, where positive y is down, and the
            // angle is computed clockwise from the x-axis.
            double angle = Math.Atan2(y, x);

            // Atan2() returns values between pi and -pi.  We want a value between
            // 0 and 2 pi.  In order to compensate for this, we'll add 2 pi to the angle
            // if it's less than 0, and then multiply by 180 / pi to get the angle
            // in degrees rather than radians, which are the expected units in XAML.
            if (angle < 0)
            {
                angle += 2 * Math.PI;
            }

            return angle * 180 / Math.PI;
        }

        private Orientation GetDirection(double x, double y)
        {
            return Math.Abs(x) >= Math.Abs(y) ? System.Windows.Controls.Orientation.Horizontal : System.Windows.Controls.Orientation.Vertical;
        }

        private void keepPlayingButton_Click(object sender, RoutedEventArgs e)
        {
            matchBorder.Visibility = System.Windows.Visibility.Collapsed;
            _viewModel.NextRecommendation();
        }

        private void OnDragDelta(object sender, ManipulationDeltaEventArgs e)
        {
            // HorizontalChange and VerticalChange from DragDeltaGestureEventArgs are now
            // DeltaManipulation.Translation.X and DeltaManipulation.Translation.Y.
            transform.TranslateX += e.DeltaManipulation.Translation.X;
        }

        private void OnFlick(object sender, ManipulationCompletedEventArgs e)
        {
            double horizontalVelocity = e.FinalVelocities.LinearVelocity.X;
            double verticalVelocity = e.FinalVelocities.LinearVelocity.Y;

            double angle = Math.Round(this.GetAngle(horizontalVelocity, verticalVelocity));

            if (this.GetDirection(horizontalVelocity, verticalVelocity) == System.Windows.Controls.Orientation.Horizontal)
            {
                if (angle >= 180)
                    _viewModel.RejectUser();
                else
                    _viewModel.LikeUser();

                transform.TranslateX = 0;
            }
        }

        private void OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            if (e.IsInertial)
            {
                this.OnFlick(sender, e);
            }
            else
                transform.TranslateX = 0;
        }

        private void OnManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            this.OnDragDelta(sender, e);
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
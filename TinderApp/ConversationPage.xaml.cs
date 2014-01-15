using Microsoft.Phone.Controls;
using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;
using TinderApp.Views.ViewModels.Conversation;

namespace TinderApp
{
    public partial class ConversationPage : PhoneApplicationPage
    {
        public ConversationViewModel ViewModel;

        public ConversationPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            this.LayoutUpdated += ConversationPage_LayoutUpdated;

            base.OnNavigatedTo(e);
        }

        private void ConversationPage_LayoutUpdated(object sender, EventArgs e)
        {
            this.LayoutUpdated -= ConversationPage_LayoutUpdated;

            string id = NavigationContext.QueryString["id"];
            ViewModel = new ConversationViewModel(id);
            DataContext = ViewModel.Match;

            _profileName.Text = ViewModel.Match.Name;
            ImageBrush fillBrush = new ImageBrush();

            BitmapImage image = new BitmapImage(ViewModel.Match.ProfilePhoto);
            image.CreateOptions = BitmapCreateOptions.BackgroundCreation;

            fillBrush.SetValue(ImageBrush.ImageSourceProperty, image);
            fillBrush.Stretch = Stretch.UniformToFill;
            _profilePhotoCircle.Fill = fillBrush;
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ProfileInfoPage.xaml?id=" + ViewModel.Match.Data.Person.Id, UriKind.Relative));
        }

        private async void OnSendingMessage(object sender, ConversationViewMessageEventArgs e)
        {
            await ViewModel.Match.SendMessage((e.Message as ConversationViewMessage).Text);
        }
    }
}
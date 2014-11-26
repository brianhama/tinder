using Microsoft.Phone.Controls;
using TinderApp.Library.ViewModels;

namespace TinderApp.Library.Controls
{
    public class CustomPhoneApplicationFrame : PhoneApplicationFrame
    {
        private readonly CustomPhoneApplicationFrameViewModel _viewModel;

        public CustomPhoneApplicationFrame()
        {
            _viewModel = new CustomPhoneApplicationFrameViewModel();
            DataContext = _viewModel;
        }

        public CustomPhoneApplicationFrameViewModel ViewModel
        {
            get { return _viewModel; }
        }

        public void LoggedIn()
        {
            _viewModel.ProfileImageBrushChanged();
        }
    }
}
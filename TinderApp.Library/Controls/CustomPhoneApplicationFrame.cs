using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinderApp.Library.ViewModels;

namespace TinderApp.Library.Controls
{
    public class CustomPhoneApplicationFrame : PhoneApplicationFrame
    {
        public CustomPhoneApplicationFrame()
        {
            _viewModel = new CustomPhoneApplicationFrameViewModel();
            DataContext = _viewModel;
        }

        private readonly CustomPhoneApplicationFrameViewModel _viewModel;

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

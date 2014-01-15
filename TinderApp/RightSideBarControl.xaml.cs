using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using TinderApp.Library.ViewModels;

namespace TinderApp
{
    public partial class RightSideBarControl : UserControl
    {
        public RightSideBarControl()
        {
            InitializeComponent();

            if (!DesignerProperties.IsInDesignTool)
                (App.Current as App).RightSideBar = this;
        }

        private void RadDataBoundListBox_ItemTap(object sender, Telerik.Windows.Controls.ListBoxItemTapEventArgs e)
        {
            MatchViewModel vm = (e.Item.AssociatedDataItem as Telerik.Windows.Data.IDataSourceItem).Value as MatchViewModel;

            VisualStateManager.GoToState(Application.Current.RootVisual as PhoneApplicationFrame, "Default", false);

            (Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/ConversationPage.xaml?id=" + vm.Data.Id, UriKind.Relative));
        }
    }
}
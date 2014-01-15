using System.Windows;

namespace TinderApp.Library.MVVM
{
    public class TopBarViewModel : ObservableObject
    {
        public static Visibility ShowTopButtons { get; set; }
    }
}
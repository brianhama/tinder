using System.ComponentModel;
using System.Windows;

namespace TinderApp.Library.MVVM
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                Deployment.Current.Dispatcher.SmartBeginInvoke(() =>
                {
                    propertyChanged(this, new PropertyChangedEventArgs(propertyName));
                });
            }
        }
    }
}
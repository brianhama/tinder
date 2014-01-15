using System;
using System.Windows.Threading;

namespace TinderApp.Library
{
    public static class DispatcherExtensions
    {
        public static void SmartBeginInvoke(this Dispatcher dispatcher, Action a)
        {
            if (dispatcher.CheckAccess())
            {
                a.Invoke();
            }
            else
            {
                dispatcher.BeginInvoke(a);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderApp.Library.Controls
{
    public interface IApp
    {
        CustomPhoneApplicationFrame RootFrameInstance { get; }

        void Logout();
    }
}

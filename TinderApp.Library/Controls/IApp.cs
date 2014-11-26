namespace TinderApp.Library.Controls
{
    public interface IApp
    {
        CustomPhoneApplicationFrame RootFrameInstance { get; }

        void Logout();
    }
}
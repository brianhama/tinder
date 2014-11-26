using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TinderApp.Library.MVVM;
using TinderApp.Models;
using TinderApp.TinderApi;
using TinderApp.Views.ViewModels.Conversation;

namespace TinderApp.Library.ViewModels
{
    public class MatchViewModel : ObservableObject
    {
        private Match _data;
        private ObservableCollection<ConversationMessageViewModel> _messages = new ObservableCollection<ConversationMessageViewModel>();

        public MatchViewModel(Match data)
        {
            _data = data;
        }

        public Match Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                RaisePropertyChanged("Name");
                RaisePropertyChanged("ProfilePhoto");
                RaisePropertyChanged("IsNew");
            }
        }

        public Boolean IsNew
        {
            get
            {
                return _data.Closed;
            }
        }

        public String MatchDate
        {
            get
            {
                if (Data.Messages.Any())
                    return Data.Messages.Last().Message;
                return "Matched " + String.Format("{0:d}", DateTime.Parse(_data.CreatedDate));
            }
        }

        public ObservableCollection<ConversationMessageViewModel> Messages
        {
            get { return _messages; }
        }

        public String Name
        {
            get
            {
                return _data.Person.Name;
            }
        }

        public Uri ProfilePhoto
        {
            get
            {
                return Utility.GetMainPhoto(_data.Person.Photos);
            }
        }

        public async Task SendMessage(string message)
        {
            await Client.Post<NewOutgoingMessageResponse>("user/matches/" + Data.Id, new OutgoingNewMessage() { Message = message });
            await TinderSession.CurrentSession.GetUpdate();
        }

        public void UpdateMessages()
        {
            Deployment.Current.Dispatcher.SmartBeginInvoke(() =>
            {
                foreach (var msg in Data.Messages)
                    _messages.Add(new ConversationMessageViewModel(msg));
                RaisePropertyChanged("Messages");
                RaisePropertyChanged("Name");
                RaisePropertyChanged("ProfilePhoto");
            });
        }
    }
}
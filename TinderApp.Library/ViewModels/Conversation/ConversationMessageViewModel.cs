using System;
using Telerik.Windows.Controls;
using TinderApp.Library;
using TinderApp.Library.MVVM;
using TinderApp.Models;

namespace TinderApp.Views.ViewModels.Conversation
{
    public class ConversationMessageViewModel : ObservableObject
    {
        public ConversationMessageViewModel(Msg message)
        {
            Text = message.Message;
            TimeStamp = DateTime.Parse(message.SentDate);
            Type = message.To == TinderSession.CurrentSession.CurrentUser.Id ? ConversationViewMessageType.Incoming : ConversationViewMessageType.Outgoing;
        }

        public String FormattedTimeStamp
        {
            get
            {
                return Utility.GetPrettyDate(TimeStamp);
            }
        }

        public Int32 Group { get; set; }

        public string Text { get; private set; }

        public DateTime TimeStamp { get; private set; }

        public ConversationViewMessageType Type { get; private set; }
    }
}
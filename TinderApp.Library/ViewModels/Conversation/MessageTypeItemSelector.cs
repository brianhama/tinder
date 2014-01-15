using System.Windows;
using Telerik.Windows.Controls;
using TinderApp.Views.ViewModels.Conversation;

namespace TinderApp.Library.ViewModels.Conversation
{
    public class MessageTypeItemSelector : DataTemplateSelector
    {
        public DataTemplate IncomingTemplate
        {
            get;
            set;
        }

        public DataTemplate OutgoingTemplate
        {
            get;
            set;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ConversationMessageViewModel message = (ConversationMessageViewModel)item;

            switch (message.Type)
            {
                case ConversationViewMessageType.Incoming:
                    return this.IncomingTemplate;

                case ConversationViewMessageType.Outgoing:
                    return this.OutgoingTemplate;
            }

            return null;
        }
    }
}
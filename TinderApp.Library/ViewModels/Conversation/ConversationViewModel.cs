using System;
using System.Linq;
using TinderApp.Library;
using TinderApp.Library.MVVM;
using TinderApp.Library.ViewModels;

namespace TinderApp.Views.ViewModels.Conversation
{
    public class ConversationViewModel : ObservableObject
    {
        private static string[] MessagePrompts = { "Don't swipe and drive", "How many friends have you made on Tinder?", "Tinder can't type for you... at least not yet.", "'Hi' isn't memorable.", "Billions of messages have been sent on Tinder.  What will yours be?", "I wonder what tagline they have.", "If 46% of your body heat comes from your ears, where does the rest come from?", "Send a f*%#^+g messages already!", "Have some manners... say something!", "Why did you swipe right?", "If you don't ask, the answer is always no.", "Tell me about a crazy expierence you just had.", "YOLO!" };

        private static string GetMessagePrompt()
        {
            Random rnd = new Random();
            return MessagePrompts[rnd.Next(MessagePrompts.Length)];
        }

        private readonly string _id;
        private readonly MatchViewModel _match;

        public ConversationViewModel(string id)
        {
            _id = id;
            _match = TinderSession.CurrentSession.Matches.Matches.FirstOrDefault(a => a.Data.Id == id);
            if (_match != null)
                _match.UpdateMessages();
        }

        public MatchViewModel Match
        {
            get { return _match; }
        }

        public String MessagePrompt
        {
            get
            {
                return GetMessagePrompt();
            }
        }
    }
}
using System.Linq;
using TinderApp.Library;
using TinderApp.Library.MVVM;
using TinderApp.Library.ViewModels;

namespace TinderApp.Views.ViewModels.Conversation
{
    public class ConversationViewModel : ObservableObject
    {
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
    }
}
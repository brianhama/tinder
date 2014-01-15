using System.Collections.ObjectModel;
using System.Linq;
using TinderApp.Lib;
using TinderApp.Library.MVVM;
using TinderApp.Views.ViewModels.Conversation;

namespace TinderApp.Library.ViewModels
{
    public class MatchesViewModel : ObservableObject
    {
        private ObservableCollection<MatchViewModel> _matches = new ObservableCollection<MatchViewModel>();

        public ObservableCollection<MatchViewModel> Matches
        {
            get { return _matches; }
        }

        internal void Update(Match[] matches)
        {
            lock (_matches)
            {
                foreach (var match in matches)
                {
                    var existingMatch = _matches.FirstOrDefault(a => a.Data.Id == match.Id);
                    if (existingMatch != null)
                    {
                        foreach (var msg in match.Messages)
                            existingMatch.Messages.Add(new ConversationMessageViewModel(msg));
                    }
                    else
                    {
                        _matches.Insert(0, new MatchViewModel(match));
                    }
                }

                RaisePropertyChanged("Matches");
            }
        }
    }
}
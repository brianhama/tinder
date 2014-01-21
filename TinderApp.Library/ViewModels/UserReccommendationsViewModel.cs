using System;
using System.Windows.Input;
using System.Windows.Media;
using TinderApp.Lib;
using TinderApp.Lib.API;
using TinderApp.Library;
using TinderApp.Library.MVVM;

namespace TinderApp.Views
{
    public class UserReccommendationsViewModel : ObservableObject
    {
        private static SolidColorBrush GRAY_BRUSH = new SolidColorBrush(Colors.LightGray);
        private static SolidColorBrush BLACK_BRUSH = new SolidColorBrush(Colors.Black);

        private readonly ICommand _likeUserCommand;

        private readonly ICommand _rejectUserCommand;

        private UserResult _currentRec;

        public UserReccommendationsViewModel()
        {
            _likeUserCommand = new RelayCommand(LikeUser);
            _rejectUserCommand = new RelayCommand(RejectUser);

            if (TinderSession.CurrentSession != null && TinderSession.CurrentSession.IsAuthenticated && TinderSession.CurrentSession.Recommendations.Count > 0)
            {
                _currentRec = TinderSession.CurrentSession.Recommendations.Pop();

                RaisePropertyChanged("PhotoCount");
                RaisePropertyChanged("Name");
                RaisePropertyChanged("Age");
                RaisePropertyChanged("FriendCount");
                RaisePropertyChanged("LikeCount");
                RaisePropertyChanged("LikesBrush");
                RaisePropertyChanged("FriendsBrush");
                RaisePropertyChanged("PhotosBrush");
                RaisePropertyChanged("ProfilePhoto");
                RaisePropertyChanged("CurrentReccomendation");
            }
        }

        public event EventHandler<AnimationEventArgs> OnAnimation;

        public event EventHandler OnMatch;

        public String Age
        {
            get
            {
                if (_currentRec == null)
                    return "Try Later";
                return String.Format("{0:N0}", Math.Floor(DateTime.UtcNow.Subtract(DateTime.Parse(_currentRec.BirthDate)).TotalDays / 365));
            }
        }

        public UserResult CurrentReccomendation
        {
            get { return _currentRec; }
        }

        public Int32 FriendCount
        {
            get
            {
                if (_currentRec == null)
                    return 0;
                return _currentRec.CommonFriendCount;
            }
        }

        public Int32 LikeCount
        {
            get
            {
                if (_currentRec == null)
                    return 0;
                return _currentRec.CommonLikeCount;
            }
        }

        public SolidColorBrush FriendsBrush
        {
            get
            {
                return FriendCount > 0 ? BLACK_BRUSH : GRAY_BRUSH;
            }
        }
        public SolidColorBrush LikesBrush
        {
            get
            {
                return LikeCount > 0 ? BLACK_BRUSH : GRAY_BRUSH;
            }
        }
        public SolidColorBrush PhotosBrush
        {
            get
            {
                return PhotoCount > 0 ? BLACK_BRUSH : GRAY_BRUSH;
            }
        }

        public ICommand LikeUserCommand
        {
            get { return _likeUserCommand; }
        }

        public String Name
        {
            get
            {
                if (_currentRec == null)
                    return "No Nearby People";
                return _currentRec.Name;
            }
        }

        public Int32 PhotoCount
        {
            get
            {
                if (_currentRec == null)
                    return 0;
                return _currentRec.Photos.Length;
            }
        }

        public Uri ProfilePhoto
        {
            get
            {
                if (_currentRec == null)
                    return null;
                return Utils.GetMainPhoto(_currentRec.Photos);
            }
        }

        public ICommand RejectUserCommand
        {
            get { return _rejectUserCommand; }
        }

        public async void LikeUser()
        {
            RaiseAnimation("Like");
            LikeResponse response = await Client.Get<LikeResponse>("like/" + _currentRec.Id);
            if (response.Match)
            {
                RaiseOnMatch();
            }
            else
            {
                NextRecommendation();
            }
        }

        public async void NextRecommendation()
        {
            if (TinderSession.CurrentSession.Recommendations.Count > 0)
                _currentRec = TinderSession.CurrentSession.Recommendations.Pop();
            else
                _currentRec = null;
            RaisePropertyChanged("PhotoCount");
            RaisePropertyChanged("Name");
            RaisePropertyChanged("Age");
            RaisePropertyChanged("FriendCount");
            RaisePropertyChanged("LikeCount");
            RaisePropertyChanged("ProfilePhoto");
            RaisePropertyChanged("CurrentReccomendation");
            RaisePropertyChanged("LikesBrush");
            RaisePropertyChanged("FriendsBrush");
            RaisePropertyChanged("PhotosBrush");

            if (TinderSession.CurrentSession.Recommendations.Count == 0)
                await TinderSession.CurrentSession.GetRecommendations();
        }

        public async void RejectUser()
        {
            RaiseAnimation("Pass");
            await Client.Get("pass/" + _currentRec.Id);
            NextRecommendation();
        }

        private void RaiseAnimation(string animation)
        {
            if (OnAnimation != null)
                OnAnimation(this, new AnimationEventArgs(animation));
        }

        private void RaiseOnMatch()
        {
            if (OnMatch != null)
                OnMatch(this, new EventArgs());
        }

        public class AnimationEventArgs : EventArgs
        {
            public AnimationEventArgs(string animation)
            {
                AnimationName = animation;
            }

            public String AnimationName { get; set; }
        }
    }
}
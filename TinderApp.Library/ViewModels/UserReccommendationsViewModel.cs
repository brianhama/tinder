using System;
using System.Windows.Input;
using TinderApp.Lib;
using TinderApp.Lib.API;
using TinderApp.Library;
using TinderApp.Library.MVVM;

namespace TinderApp.Views
{
    public class UserReccommendationsViewModel : ObservableObject
    {
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
                return _currentRec.CommonFriendCount;
            }
        }

        public Int32 LikeCount
        {
            get
            {
                return _currentRec.CommonLikeCount;
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
                return _currentRec.Name;
            }
        }

        public Int32 PhotoCount
        {
            get
            {
                return _currentRec.Photos.Length;
            }
        }

        public Uri ProfilePhoto
        {
            get
            {
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
            _currentRec = TinderSession.CurrentSession.Recommendations.Pop();
            RaisePropertyChanged("PhotoCount");
            RaisePropertyChanged("Name");
            RaisePropertyChanged("Age");
            RaisePropertyChanged("FriendCount");
            RaisePropertyChanged("LikeCount");
            RaisePropertyChanged("ProfilePhoto");
            RaisePropertyChanged("CurrentReccomendation");

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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TinderApp.Lib;
using TinderApp.Library.ViewModels;

namespace TinderApp.Library
{
    public class TinderSession
    {
        private static TinderSession _currentSession = null;

        private readonly FacebookSessionInfo _fbSessionInfo;

        private readonly GeographicalCordinates _location;

        private Profile _currentProfile;

        private User _currentUser = null;

        private Globals _globalInfo;

        private volatile bool _isUpdating = false;

        private DateTime? _lastActivity = new DateTime?();

        private MatchesViewModel _matches = new MatchesViewModel();

        private Stack<UserResult> _recommendations = new Stack<UserResult>();

        private DispatcherTimer _updateTimer;

        private TinderSession(FacebookSessionInfo fbSession, GeographicalCordinates location)
        {
            _fbSessionInfo = fbSession;
            _location = location;
        }

        public static TinderSession CurrentSession
        {
            get
            {
                if (_currentSession == null)
                    return null;
                return _currentSession;
            }
        }

        public Profile CurrentProfile
        {
            get { return _currentProfile; }
        }

        public User CurrentUser
        {
            get { return _currentUser; }
        }

        public FacebookSessionInfo FbSessionInfo
        {
            get { return _fbSessionInfo; }
        }

        public Globals GlobalInfo
        {
            get { return _globalInfo; }
        }

        public Boolean IsAuthenticated
        {
            get
            {
                return _currentUser != null;
            }
        }

        public DateTime? LastActivity
        {
            get { return _lastActivity; }
            set { _lastActivity = value; }
        }

        public MatchesViewModel Matches
        {
            get { return _matches; }
            set { _matches = value; }
        }

        public Stack<UserResult> Recommendations
        {
            get { return _recommendations; }
        }

        public static TinderSession CreateNewSession(FacebookSessionInfo fbSession, GeographicalCordinates location)
        {
            _currentSession = new TinderSession(fbSession, location);

            return _currentSession;
        }

        public async Task<Boolean> Authenticate()
        {
            AuthRequest request = new AuthRequest();
            request.facebook_token = FbSessionInfo.FacebookToken;
            request.facebook_id = FbSessionInfo.FacebookID;
            AuthResponse response = await request.Send();
            if (response.token.Length > 0)
            {
                _currentUser = response.user;
                _globalInfo = response.globals;
                _currentProfile = await Profile.GetProfile();

                await PingWithLocation();
                await GetUpdate();
                await GetRecommendations();

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    _updateTimer = new DispatcherTimer();
                    _updateTimer.Interval = TimeSpan.FromMilliseconds(_globalInfo.UpdatesInterval);
                    _updateTimer.Tick += _updateTimer_Tick;
                    _updateTimer.Start();
                });

                return true;
            }

            return false;
        }

        public async Task GetRecommendations()
        {
            ReccommendationsRequest response = await ReccommendationsRequest.GetRecommendations();
            if (response.Status == 200)
            {
                if (Recommendations.Count == 0)
                    foreach (var rec in response.Results)
                        Recommendations.Push(rec);
            }
        }

        public async Task<UpdatesResponse> GetUpdate()
        {
            if (_isUpdating)
                return null;

            try
            {
                _isUpdating = true;

                UpdatesRequest request = new UpdatesRequest();
                request.last_activity_date = LastActivity;
                UpdatesResponse response = await request.GetUpdate();

                if (response.Matches != null && response.Matches.Length > 0)
                {
                    _matches.Update(response.Matches);
                }

                LastActivity = DateTime.Parse(response.LastActivityDate);

                return response;
            }
            finally
            {
                _isUpdating = false;
            }
        }

        public void Logout()
        {
            Settings settings = new Settings();
            settings.Save();
        }

        private async void _updateTimer_Tick(object sender, EventArgs e)
        {
            await GetUpdate();
        }

        private async Task PingWithLocation()
        {
            PingRequest ping = new PingRequest();
            ping.lat = _location.Latitude;
            ping.lon = _location.Longitude;
            await ping.Ping();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TinderApp.Library.Controls;
using TinderApp.Library.ViewModels;
using TinderApp.Models;
using TinderApp.Models.Facebook;
using TinderApp.TinderApi;

namespace TinderApp.Library
{
    [DataContract]
    public class TinderSession
    {
        private static TinderSession _currentSession = null;

        private readonly FacebookSessionInfo _fbSessionInfo;

        private readonly Position _location;

        private Profile _currentProfile;

        private User _currentUser = null;

        private Globals _globalInfo;

        private volatile bool _isUpdating = false;

        private DateTime? _lastActivity = new DateTime?();

        private MatchesViewModel _matches = new MatchesViewModel();

        private Stack<UserResult> _recommendations = new Stack<UserResult>();

        private DispatcherTimer _updateTimer;

        public TinderSession()
        {
        }

        private TinderSession(FacebookSessionInfo fbSession, Position location)
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
                return _currentUser != null && !String.IsNullOrEmpty(Client.AuthToken);
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

        public static TinderSession CreateNewSession(FacebookSessionInfo fbSession, Position location)
        {
            _currentSession = new TinderSession(fbSession, location);

            return _currentSession;
        }

        public static TinderSession FromTombstoneData(TombstoneData data)
        {
            Client.AuthToken = data.AuthToken;
            _currentSession = new TinderSession(data.FBSession, data.Location);
            _currentSession._currentProfile = data.CurrentProfile;
            _currentSession._currentUser = data.CurrentUser;
            _currentSession._globalInfo = data.CurrentGlobals;
            _currentSession._lastActivity = data.LastActivity;
            _currentSession._matches = new MatchesViewModel(data.Matches);
            _currentSession._recommendations = new Stack<UserResult>(data.Recommendations);
            _currentSession.StartUpdatesTimer();

            (Application.Current as TinderApp.Library.Controls.IApp).RootFrameInstance.LoggedIn();

            return _currentSession;
        }

        public async Task<Boolean> Authenticate()
        {
            AuthRequest request = new AuthRequest();
            request.FacebookToken = FbSessionInfo.FacebookToken;
            request.FacebookID = FbSessionInfo.FacebookID;
            AuthResponse response = await request.Send();
            if (response.AuthToken.Length > 0)
            {
                _currentUser = response.UserProfile;
                _globalInfo = response.GlobalVariables;
                _currentProfile = await Profile.GetProfile();

                await PingWithLocation();
                await GetUpdate();
                await GetRecommendations();

                StartUpdatesTimer();

                (Application.Current as TinderApp.Library.Controls.IApp).RootFrameInstance.LoggedIn();

                return true;
            }

            return false;
        }

        public async Task GetRecommendations()
        {
            ReccommendationsRequest response = await ReccommendationsRequest.GetRecommendations().ConfigureAwait(false);
            if (response.Status == 200)
            {
                if (Recommendations.Count == 0)
                    foreach (var rec in response.Results)
                        Recommendations.Push(rec);
            }
        }

        public async Task GetUpdate()
        {
            if (_isUpdating)
                return;

            try
            {
                _isUpdating = true;

                UpdatesRequest request = new UpdatesRequest();
                request.LastActivityDate = LastActivity;
                UpdatesResponse response = await request.GetUpdate().ConfigureAwait(false);

                if (response.Matches != null && response.Matches.Length > 0)
                {
                    _matches.Update(response.Matches.Where(a => a.Person != null).ToArray());
                }

                LastActivity = DateTime.Parse(response.LastActivityDate);
            }
            catch (HttpRequestException e)
            {
                if (e.Message.Contains("Unauthorized"))
                {
                    (Application.Current as IApp).Logout();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during update: " + ex.Message);
            }
            finally
            {
                _isUpdating = false;
            }
        }

        public void Logout()
        {
            _updateTimer.Stop();
            _currentSession = null;
        }

        public TombstoneData ToTombstoneData()
        {
            return new TombstoneData()
            {
                AuthToken = Client.AuthToken,
                CurrentGlobals = _globalInfo,
                CurrentProfile = _currentProfile,
                CurrentUser = _currentUser,
                FBSession = _fbSessionInfo,
                LastActivity = _lastActivity,
                Location = _location,
                Matches = new List<Match>(Matches.Matches.Select(a => a.Data)),
                Recommendations = _recommendations.ToList()
            };
        }

        private async void _updateTimer_Tick(object sender, EventArgs e)
        {
            await GetUpdate();
        }

        private async Task PingWithLocation()
        {
            PingRequest ping = new PingRequest();
            ping.Latitude = _location.Latitude;
            ping.Longitude = _location.Longitude;
            await ping.Ping();
        }

        private void StartUpdatesTimer()
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                _updateTimer = new DispatcherTimer();
                _updateTimer.Interval = TimeSpan.FromMilliseconds(_globalInfo.UpdatesInterval);
                _updateTimer.Tick += _updateTimer_Tick;
                _updateTimer.Start();
            });
        }
    }
}
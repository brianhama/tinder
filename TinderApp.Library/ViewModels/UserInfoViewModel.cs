using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using TinderApp.Lib;
using TinderApp.Lib.API;
using TinderApp.Library.MVVM;

namespace TinderApp.Library.ViewModels
{
    public class UserInfoViewModel : ObservableObject
    {
        private readonly UserResult _data;

        public UserInfoViewModel(UserResult data)
        {
            _data = data;
        }

        public UserInfoViewModel()
        {
            _data = JsonConvert.DeserializeObject<UserResult>(@"{""_id"":""528326b8c9f97bc0740021ba"",""bio"":""We can tell everyone we met at a Star Wars convention."",""birth_date"":""1978-07-08T00:00:00.000Z"",""gender"":1,""name"":""Natalie"",""ping_time"":""2014-01-13T19:24:27.976Z"",""photos"":[{""id"":""f77097c0-3dcf-4ee3-86c8-19004405e147"",""main"":""main"",""shape"":""center_square"",""fileName"":""f77097c0-3dcf-4ee3-86c8-19004405e147.jpg"",""extension"":""jpg"",""processedFiles"":[{""width"":640,""height"":640,""url"":""http://images.gotinder.com/528326b8c9f97bc0740021ba/640x640_f77097c0-3dcf-4ee3-86c8-19004405e147.jpg""},{""width"":320,""height"":320,""url"":""http://images.gotinder.com/528326b8c9f97bc0740021ba/320x320_f77097c0-3dcf-4ee3-86c8-19004405e147.jpg""},{""width"":172,""height"":172,""url"":""http://images.gotinder.com/528326b8c9f97bc0740021ba/172x172_f77097c0-3dcf-4ee3-86c8-19004405e147.jpg""},{""width"":84,""height"":84,""url"":""http://images.gotinder.com/528326b8c9f97bc0740021ba/84x84_f77097c0-3dcf-4ee3-86c8-19004405e147.jpg""}],""url"":""http://images.gotinder.com/528326b8c9f97bc0740021ba/f77097c0-3dcf-4ee3-86c8-19004405e147.jpg""},{""id"":""98910d82-d7aa-43c2-b57e-6f943a6a19fa"",""shape"":""center_square"",""fileName"":""98910d82-d7aa-43c2-b57e-6f943a6a19fa.jpg"",""extension"":""jpg"",""processedFiles"":[{""width"":640,""height"":640,""url"":""http://images.gotinder.com/528326b8c9f97bc0740021ba/640x640_98910d82-d7aa-43c2-b57e-6f943a6a19fa.jpg""},{""width"":320,""height"":320,""url"":""http://images.gotinder.com/528326b8c9f97bc0740021ba/320x320_98910d82-d7aa-43c2-b57e-6f943a6a19fa.jpg""},{""width"":172,""height"":172,""url"":""http://images.gotinder.com/528326b8c9f97bc0740021ba/172x172_98910d82-d7aa-43c2-b57e-6f943a6a19fa.jpg""},{""width"":84,""height"":84,""url"":""http://images.gotinder.com/528326b8c9f97bc0740021ba/84x84_98910d82-d7aa-43c2-b57e-6f943a6a19fa.jpg""}],""url"":""http://images.gotinder.com/528326b8c9f97bc0740021ba/98910d82-d7aa-43c2-b57e-6f943a6a19fa.jpg""}],""birth_date_info"":""fuzzy birthdate active, not displaying real birth_date"",""id"":""528326b8c9f97bc0740021ba"",""common_friends"":[],""common_likes"":[""14352220141""],""common_like_count"":1,""common_friend_count"":0,""distance_mi"":29}");
        }

        public string Bio
        {
            get
            {
                return _data.Bio;
            }
        }

        public List<Uri> CommonFriends
        {
            get
            {
                List<Uri> returnValue = new List<Uri>();
                foreach (string friend in _data.CommonFriends)
                {
                    returnValue.Add(new Uri(String.Format("http://graph.facebook.com/{0}/picture?width=128&height=128 ", friend), UriKind.Absolute));
                }
                return returnValue;
            }
        }

        public string Distance
        {
            get
            {
                return String.Format("{0:N0} miles away", _data.DistanceMi);
            }
        }

        public string LastActive
        {
            get
            {
                return "active " + Utils.GetPrettyDate(DateTime.Parse(_data.PingTime));
            }
        }

        public List<Uri> LikePhotos
        {
            get
            {
                List<Uri> returnValue = new List<Uri>();
                foreach (string like in _data.CommonLikes)
                {
                    returnValue.Add(new Uri(String.Format("http://graph.facebook.com/{0}/picture?width=128&height=128 ", like), UriKind.Absolute));
                }
                return returnValue;
            }
        }

        public string NameAndAge
        {
            get
            {
                return String.Format("{0}, {1:N0}", _data.Name, Math.Floor(DateTime.UtcNow.Subtract(DateTime.Parse(_data.BirthDate)).TotalDays / 365));
            }
        }

        public List<Uri> Photos
        {
            get
            {
                List<Uri> returnValue = new List<Uri>();
                foreach (var photo in _data.Photos)
                {
                    returnValue.Add(new Uri(photo.Url, UriKind.Absolute));
                }
                return returnValue;
            }
        }

        public Visibility ShowBio
        {
            get
            {
                return _data.Bio.Length > 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility ShowFriends
        {
            get
            {
                return _data.CommonFriendCount > 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility ShowLikes
        {
            get
            {
                return _data.CommonLikeCount > 0 ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public static async Task<UserInfoViewModel> LoadUserInfo(string id)
        {
            UserResponse response = await Client.Get<UserResponse>("user/" + id);
            return new UserInfoViewModel(response.Results);
        }
    }
}
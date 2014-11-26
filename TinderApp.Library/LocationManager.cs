using System;
using System.Device.Location;
using TinderApp.Models;

namespace TinderApp.Library
{
    public class LocationManager
    {
        #region Delegates

        public delegate void PositionDeterminedHandler(object sender, PositionDeterminedEventArgs ca);

        #endregion Delegates

        private Boolean _hasDeterminedPosition = false;
        private GeoCoordinateWatcher watcher;

        static LocationManager()
        {
            LastPosition = new Position() { Latitude = 35, Longitude = 35 };
        }

        public event PositionDeterminedHandler OnPositionDetermined;

        public static Position LastPosition { get; set; }

        public Boolean HasDeterminedPosition
        {
            get { return _hasDeterminedPosition; }
        }

        public void BeginGetLocation()
        {
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);

            if (watcher.Permission == GeoPositionPermission.Granted)
            {
                watcher.Start();
            }
            else
                PositionDetermined(new PositionDeterminedEventArgs(true, false));
        }

        private Position GetPositionFromGeoCoordinate(GeoCoordinate e)
        {
            return new Position() { Latitude = (float)e.Latitude, Longitude = (float)e.Longitude };
        }

        private void PositionDetermined(PositionDeterminedEventArgs e)
        {
            _hasDeterminedPosition = true;
            LastPosition = e.Location;

            if (OnPositionDetermined != null)
                OnPositionDetermined(this, e);
        }

        private void watcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (!e.Position.Location.IsUnknown)
            {
                watcher.Stop();
                PositionDetermined(new PositionDeterminedEventArgs(GetPositionFromGeoCoordinate(e.Position.Location)));
            }
        }
    }

    public class PositionDeterminedEventArgs : EventArgs
    {
        public PositionDeterminedEventArgs(Boolean isPermissionFailure, Boolean isOtherFailure)
        {
            IsPermissionFailure = isPermissionFailure;
            IsOtherFailure = isOtherFailure;
        }

        public PositionDeterminedEventArgs(Position coordinate)
        {
            Location = coordinate;
            IsPermissionFailure = false;
            IsOtherFailure = false;
        }

        public Boolean IsOtherFailure { get; private set; }

        public Boolean IsPermissionFailure { get; private set; }

        public Position Location { get; private set; }
    }
}
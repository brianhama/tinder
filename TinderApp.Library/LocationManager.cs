using System;
using System.Device.Location;
using System.Windows.Threading;

namespace TinderApp.Library
{
    public class GeographicalCordinates
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class LocationManager
    {
        #region Delegates

        public delegate void PositionDeterminedHandler(object sender, PositionDeterminedEventArgs ca);

        #endregion Delegates

        private Boolean _hasDeterminedPosition = false;
        private DispatcherTimer locationTimeout = null;
        private GeoCoordinateWatcher watcher;

        static LocationManager()
        {
            LastPosition = new GeographicalCordinates() { Latitude = 35, Longitude = 35 };
        }

        public event PositionDeterminedHandler OnPositionDetermined;

        public static GeographicalCordinates LastPosition { get; set; }

        public Boolean HasDeterminedPosition
        {
            get { return _hasDeterminedPosition; }
        }

        public void BeginGetLocation()
        {
            watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High);
            watcher.PositionChanged += new EventHandler<GeoPositionChangedEventArgs<GeoCoordinate>>(watcher_PositionChanged);

            if (watcher.Permission == GeoPositionPermission.Granted)
            {
                locationTimeout = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(60) };
                locationTimeout.Tick += new EventHandler(locationTimeout_Tick);
                locationTimeout.Start();
                watcher.Start();
            }
            else
                PositionDetermined(new PositionDeterminedEventArgs(true, false));
        }

        private GeographicalCordinates GetGeographicalCordinatesFromGeoCoordinate(GeoCoordinate e)
        {
            return new GeographicalCordinates() { Latitude = (float)e.Latitude, Longitude = (float)e.Longitude };
        }

        private void LocationTimeout(object e)
        {
        }

        private void locationTimeout_Tick(object sender, EventArgs e)
        {
            if (locationTimeout != null)
            {
                locationTimeout.Stop();
                locationTimeout = null;
            }
            watcher.Stop();
            PositionDetermined(
                new PositionDeterminedEventArgs(new GeographicalCordinates() { Latitude = 35, Longitude = 35 }));
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
            if (!e.Position.Location.IsUnknown &&
                DateTime.UtcNow.Subtract(e.Position.Timestamp.UtcDateTime).TotalHours <= 1)
            {
                if (locationTimeout != null)
                {
                    locationTimeout.Stop();
                    locationTimeout = null;
                }

                watcher.Stop();
                PositionDetermined(
                    new PositionDeterminedEventArgs(GetGeographicalCordinatesFromGeoCoordinate(e.Position.Location)));
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

        public PositionDeterminedEventArgs(GeographicalCordinates coordinate)
        {
            Location = coordinate;
            IsPermissionFailure = false;
            IsOtherFailure = false;
        }

        public Boolean IsOtherFailure { get; private set; }

        public Boolean IsPermissionFailure { get; private set; }

        public GeographicalCordinates Location { get; private set; }
    }
}
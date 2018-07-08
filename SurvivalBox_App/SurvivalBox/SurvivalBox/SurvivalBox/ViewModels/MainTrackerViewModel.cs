using System;
using System.Collections.ObjectModel;
using DataParser;
using Prism.Mvvm;
using Xamarin.Forms.Maps;

namespace SurvivalBox.ViewModels
{
    public class MainTrackerViewModel : BindableBase
    {
        public Map TrackerMap { get; set; }

        private ObservableCollection<GPSData> _gpsData;
        public ObservableCollection<GPSData> GPSData
        {
            get => _gpsData;
            set => SetProperty(ref _gpsData, value);
        }

        public MainTrackerViewModel()
        {
            GPSData = new ObservableCollection<GPSData>()
            {
                new GPSData(new GPSPosition(new Coordinate(37, CoordinateDirection.E), new Coordinate(-122, CoordinateDirection.N)), DateTime.UtcNow.Subtract(new TimeSpan(4, 4, 2, 12)), 22, 32),
                new GPSData(new GPSPosition(new Coordinate(45, CoordinateDirection.N), new Coordinate(2, CoordinateDirection.S)), DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 1, 12)), 1, 15),
                new GPSData(new GPSPosition(new Coordinate(234, CoordinateDirection.W), new Coordinate(25, CoordinateDirection.W)), DateTime.UtcNow, 255, 3212),
            };
        }

        public void StartTrackerMap()
        {
            var position = new Position(37, -122); // Latitude, Longitude
            var pin = new Pin
            {
                Type = PinType.SavedPin,
                Position = position,
                Label = "custom pin",
                Address = "custom detail info"
            };
            TrackerMap.Pins.Add(pin);
            TrackerMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(37, -122), Distance.FromMiles(1)));
        }
    }
}

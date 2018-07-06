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

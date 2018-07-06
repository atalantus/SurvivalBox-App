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

        public MainTrackerViewModel() { }
    }
}

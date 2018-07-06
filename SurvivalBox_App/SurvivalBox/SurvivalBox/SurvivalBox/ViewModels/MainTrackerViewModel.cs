using System.Collections.ObjectModel;
using DataParser;
using Prism.Mvvm;

namespace SurvivalBox.ViewModels
{
    public class MainTrackerViewModel : BindableBase
    {
        public MainTrackerViewModel()
        {
        }

        private ObservableCollection<GPSData> _gpsData;
        public ObservableCollection<GPSData> GPSData
        {
            get => _gpsData;
            set => SetProperty(ref _gpsData, value);
        }
    }
}

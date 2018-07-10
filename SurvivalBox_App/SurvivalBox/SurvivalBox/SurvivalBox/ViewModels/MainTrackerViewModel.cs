using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataParser;
using Prism.AppModel;
using Prism.Commands;
using Prism.Common;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using SurvivalBox.Models;
using SurvivalBox.Services;
using Xamarin.Forms.Maps;
using GPSDataRaw = DataParser.GPSDataRaw;

namespace SurvivalBox.ViewModels
{
    public class MainTrackerViewModel : BindableBase, IPageLifecycleAware
    {
        private readonly IPageDialogService _dialogService;

        public TrackerMap TrackerMap { get; set; }

        private ObservableCollection<GPSDataRaw> _gpsData;
        public ObservableCollection<GPSDataRaw> GPSData
        {
            get => _gpsData;
            set => SetProperty(ref _gpsData, value);
        }

        private MapType _mapType = MapType.Hybrid;
        public MapType MapType
        {
            get => _mapType;
            set => SetProperty(ref _mapType, value);
        }

        public List<string> MapTypeItems => Enum.GetNames(typeof(MapType)).ToList();

        private BoxConnection _boxConnection;

        public MainTrackerViewModel(IPageDialogService dialogService)
        {
            _dialogService = dialogService;

            GPSData = new ObservableCollection<GPSDataRaw>()
            {
                new GPSDataRaw(new GPSPosition(new Coordinate(37.797534f, CoordinateDirection.E), new Coordinate(-122.401827f, CoordinateDirection.N)), DateTime.UtcNow.Subtract(new TimeSpan(4, 4, 2, 12)), 22, 32),
                new GPSDataRaw(new GPSPosition(new Coordinate(37.797510f, CoordinateDirection.N), new Coordinate(-122.402060f, CoordinateDirection.S)), DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 1, 12)), 1, 15),
                new GPSDataRaw(new GPSPosition(new Coordinate(37.790269f, CoordinateDirection.W), new Coordinate(-122.400589f, CoordinateDirection.W)), DateTime.UtcNow, 255, 3212),
                new GPSDataRaw(new GPSPosition(new Coordinate(37.790265f, CoordinateDirection.E), new Coordinate(-122.400474f, CoordinateDirection.N)), DateTime.UtcNow.Subtract(new TimeSpan(4, 4, 2, 12)), 22, 32),
                new GPSDataRaw(new GPSPosition(new Coordinate(37.790228f, CoordinateDirection.N), new Coordinate(-122.400391f, CoordinateDirection.S)), DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 1, 12)), 1, 15),
                new GPSDataRaw(new GPSPosition(new Coordinate(37.790126f, CoordinateDirection.W), new Coordinate(-122.400360f, CoordinateDirection.W)), DateTime.UtcNow, 255, 3212),
                new GPSDataRaw(new GPSPosition(new Coordinate(37.789250f, CoordinateDirection.E), new Coordinate(-122.401451f, CoordinateDirection.N)), DateTime.UtcNow.Subtract(new TimeSpan(4, 4, 2, 12)), 22, 32),
                new GPSDataRaw(new GPSPosition(new Coordinate(37.788440f, CoordinateDirection.N), new Coordinate(-122.400396f, CoordinateDirection.S)), DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 1, 12)), 1, 15),
                new GPSDataRaw(new GPSPosition(new Coordinate(37.787999f, CoordinateDirection.W), new Coordinate(-122.399780f, CoordinateDirection.W)), DateTime.UtcNow, 255, 3212),
            };

            MapType = MapType.Hybrid;
        }

        public void RequestBluetooth()
        {
            // Check for device
            _boxConnection = BoxConnection.Instance;

            switch (_boxConnection.GetBluetoothStatus())
            {
                case BluetoothStatus.CONNECTED:
                    _dialogService.DisplayAlertAsync("Bluetooth connection successful",
                        "You connected successfully to a Bluetooth device!", "OK");
                    break;
                case BluetoothStatus.NOT_SUPPORTED:
                    _dialogService.DisplayAlertAsync("Bluetooth connection failed",
                        "Looks like your device doesn't support Bluetooth!", "OK");
                    break;
                case BluetoothStatus.NOT_ENABLED:
                    _dialogService.DisplayAlertAsync("Bluetooth connection failed",
                        "Connect to your SurvivalBox via Bluetooth in order to track your GPS-Position!", "OK");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void StartTrackerMap()
        {
            foreach (var gpsData in GPSData)
            {
                TrackerMap.RouteCoordinates.Add(new GPSData(gpsData.position.latitude.value, gpsData.position.longitude.value, gpsData.trueVelocity, gpsData.time));
            }

            TrackerMap.MoveToRegion(MapSpan.FromCenterAndRadius(TrackerMap.RouteCoordinates.Last().Position, Distance.FromMiles(1.0)));
        }

        public void OnAppearing()
        {
            RequestBluetooth();
        }

        public void OnDisappearing()
        {
            throw new NotImplementedException();
        }
    }
}

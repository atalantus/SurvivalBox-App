using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DataParser;
using Prism.Commands;
using Prism.Mvvm;
using SurvivalBox.Models;
using SurvivalBox.Services;
using Xamarin.Forms.Maps;
using GPSData = DataParser.GPSData;

namespace SurvivalBox.ViewModels
{
    public class MainTrackerViewModel : BindableBase
    {
        public TrackerMap TrackerMap { get; set; }

        private ObservableCollection<GPSData> _gpsData;
        public ObservableCollection<GPSData> GPSData
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

        public MainTrackerViewModel()
        {
            GPSData = new ObservableCollection<GPSData>()
            {
                new GPSData(new GPSPosition(new Coordinate(37.797534f, CoordinateDirection.E), new Coordinate(-122.401827f, CoordinateDirection.N)), DateTime.UtcNow.Subtract(new TimeSpan(4, 4, 2, 12)), 22, 32),
                new GPSData(new GPSPosition(new Coordinate(37.797510f, CoordinateDirection.N), new Coordinate(-122.402060f, CoordinateDirection.S)), DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 1, 12)), 1, 15),
                new GPSData(new GPSPosition(new Coordinate(37.790269f, CoordinateDirection.W), new Coordinate(-122.400589f, CoordinateDirection.W)), DateTime.UtcNow, 255, 3212),
                new GPSData(new GPSPosition(new Coordinate(37.790265f, CoordinateDirection.E), new Coordinate(-122.400474f, CoordinateDirection.N)), DateTime.UtcNow.Subtract(new TimeSpan(4, 4, 2, 12)), 22, 32),
                new GPSData(new GPSPosition(new Coordinate(37.790228f, CoordinateDirection.N), new Coordinate(-122.400391f, CoordinateDirection.S)), DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 1, 12)), 1, 15),
                new GPSData(new GPSPosition(new Coordinate(37.790126f, CoordinateDirection.W), new Coordinate(-122.400360f, CoordinateDirection.W)), DateTime.UtcNow, 255, 3212),
                new GPSData(new GPSPosition(new Coordinate(37.789250f, CoordinateDirection.E), new Coordinate(-122.401451f, CoordinateDirection.N)), DateTime.UtcNow.Subtract(new TimeSpan(4, 4, 2, 12)), 22, 32),
                new GPSData(new GPSPosition(new Coordinate(37.788440f, CoordinateDirection.N), new Coordinate(-122.400396f, CoordinateDirection.S)), DateTime.UtcNow.Subtract(new TimeSpan(0, 2, 1, 12)), 1, 15),
                new GPSData(new GPSPosition(new Coordinate(37.787999f, CoordinateDirection.W), new Coordinate(-122.399780f, CoordinateDirection.W)), DateTime.UtcNow, 255, 3212),
            };

            MapType = MapType.Street;
        }

        public void StartTrackerMap()
        {
            foreach (var gpsData in GPSData)
            {
                TrackerMap.RouteCoordinates.Add(new Models.GPSData(gpsData.position.latitude.value, gpsData.position.longitude.value, gpsData.trueVelocity, gpsData.time));
            }

            TrackerMap.MoveToRegion(MapSpan.FromCenterAndRadius(TrackerMap.RouteCoordinates.Last().Position, Distance.FromMiles(1.0)));
        }
    }
}

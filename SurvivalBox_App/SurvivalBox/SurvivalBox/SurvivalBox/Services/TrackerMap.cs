using System.Collections.Generic;
using SurvivalBox.Models;
using Xamarin.Forms.Maps;

namespace SurvivalBox.Services
{
    public class TrackerMap : Map
    {
        public List<GPSData> RouteCoordinates { get; set; }

        public TrackerMap()
        {
            RouteCoordinates = new List<GPSData>();
        }
    }
}
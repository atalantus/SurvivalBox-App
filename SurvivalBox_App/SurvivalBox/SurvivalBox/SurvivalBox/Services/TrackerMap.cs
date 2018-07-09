using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace SurvivalBox.Services
{
    public class TrackerMap : Map
    {
        public List<Position> RouteCoordinates { get; set; }

        public TrackerMap()
        {
            RouteCoordinates = new List<Position>();
        }
    }
}
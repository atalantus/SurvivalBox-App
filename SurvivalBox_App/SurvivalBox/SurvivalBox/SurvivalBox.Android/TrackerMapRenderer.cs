using System.Collections.Generic;
using Android.Content;
using Android.Gms.Maps.Model;
using SurvivalBox.Droid;
using SurvivalBox.Models;
using SurvivalBox.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(TrackerMap), typeof(TrackerMapRenderer))]
namespace SurvivalBox.Droid
{
    public class TrackerMapRenderer : MapRenderer
    {
        List<GPSData> routeCoordinates;

        public TrackerMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (TrackerMap)e.NewElement;
                routeCoordinates = formsMap.RouteCoordinates;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);

            var polylineOptions = new PolylineOptions();
            polylineOptions.InvokeColor(0x66FF0000);

            foreach (var position in routeCoordinates)
            {
                polylineOptions.Add(new LatLng(position.Latitude, position.Longitude));

                var markerOptions = new MarkerOptions();
                markerOptions.SetPosition(new LatLng(position.Latitude, position.Longitude));
                markerOptions.SetTitle(position.Label);
                markerOptions.SetSnippet(position.Address);
                map.AddMarker(markerOptions);
            }

            NativeMap.AddPolyline(polylineOptions);
        }
    }
}
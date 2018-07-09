using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls.Maps;
using SurvivalBox.Services;
using SurvivalBox.UWP;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(TrackerMap), typeof(TrackerMapRenderer))]
namespace SurvivalBox.UWP
{
    public class TrackerMapRenderer : MapRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (TrackerMap)e.NewElement;
                var nativeMap = Control as MapControl;

                var coordinates = new List<BasicGeoposition>();
                foreach (var position in formsMap.RouteCoordinates)
                {
                    var basicGeoposition =
                        new BasicGeoposition() {Latitude = position.Latitude, Longitude = position.Longitude};
                    coordinates.Add(basicGeoposition);
                    var pin = new MapIcon();

                    pin.Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///map_pin_icon.png"));
                    pin.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                    pin.Location = new Geopoint(basicGeoposition);
                    pin.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 0.9);

                    nativeMap.MapElements.Add(pin);
                }

                var polyline = new MapPolyline();
                polyline.StrokeColor = Windows.UI.Color.FromArgb(128, 255, 0, 0);
                polyline.StrokeThickness = 5;
                polyline.Path = new Geopath(coordinates);
                nativeMap.MapElements.Add(polyline);
            }
        }
    }
}
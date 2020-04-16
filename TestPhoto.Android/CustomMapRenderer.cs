using Android.Content;
using Android.Gms.Maps.Model;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(TestXamarinFirebase.CustomMap), typeof(TestXamarinFirebase.CustomMapRenderer))]

namespace TestXamarinFirebase
{
    public class CustomMapRenderer : MapRenderer
    {
        CustomCircle circle;
        List<CustomCircle> _circles;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                circle = formsMap.Circle;
                _circles = formsMap.CustomCircles;
            }
        }

        protected override void OnMapReady(Android.Gms.Maps.GoogleMap map)
        {
            base.OnMapReady(map);

            //var circleOptions = new CircleOptions();
            //circleOptions.InvokeCenter(new LatLng(circle.Position.Latitude, circle.Position.Longitude));
            //circleOptions.InvokeRadius(circle.Radius);
            //circleOptions.InvokeFillColor(0X66FF0000);
            //circleOptions.InvokeStrokeColor(0X66FF0000);
            //circleOptions.InvokeStrokeWidth(0);

            //NativeMap.AddCircle(circleOptions);

            foreach (var customCircle in _circles)
            {
                var circleOptions = new CircleOptions()
                    .InvokeCenter(new LatLng(customCircle.Position.Latitude, customCircle.Position.Longitude))
                    .InvokeRadius(customCircle.Radius)
                    .InvokeFillColor(0X66FF0000)
                    .InvokeStrokeColor(0X66FF0000)
                    .InvokeStrokeWidth(0);

                NativeMap.AddCircle(circleOptions);
            }

            
        }
    }
}

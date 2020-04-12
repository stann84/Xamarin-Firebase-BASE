
using System;
using TestXamarinFirebase.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TestXamarinFirebase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomMapPage : ContentPage
    {
        User user = new User();

        public CustomMapPage()
        {

            InitializeComponent();

            // CheckLocation();

            // var location = await Geolocation.GetLastKnownLocationAsync();

            //    var pin = new Pin
            //    {

            //    Type = PinType.Place,
            //        //Position = new Position(37.79752, -122.40183),
            //        Position = new Position(location.Latitude, location.Latitude)
            //        Label = "Xamarin San Francisco Office",
            //        Address = "394 Pacific Ave, San Francisco CA"
            //    };

            //    var position = new Position(37.79752, -122.40183);
            //    customMap.Circle = new CustomCircle
            //    {
            //        Position = position,
            //        Radius = 1000
            //    };

            //    customMap.Pins.Add(pin);
            //    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1.0)));
            //}
            //public async void CheckLocation()
            //{
            //    var location = await Geolocation.GetLastKnownLocationAsync();

            //    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
        }
    }
}
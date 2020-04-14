using System;
using System.Collections.Generic;
using TestXamarinFirebase.Helper;
using TestXamarinFirebase.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TestXamarinFirebase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        FBDatabase dataBase = new FBDatabase("https://tchat-7c40f.firebaseio.com/"); // url vers la Database firebase
        IAuth auth;                     // Accès à l'Identification
        User user = new User();         // Objet ou seront stockées les données de l'Utilisateur

        public CustomMap customMap1 { get; set; }


        public MapPage()
        {
            InitializeComponent();

            auth = DependencyService.Get<IAuth>();
            user = auth.IsAuth();   // Récuppère l'id & l'email
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var allUsers = await dataBase.GetAllUsers();
            //listUsers.ItemsSource = allUsers;
            
            Console.WriteLine(lblLongitude);
        }

        #region Bouton location

        public async void BtnLocation_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                // var location = new Location(47.645160, -122.1306032);


                if (location != null)
                {
                    // Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    lblLatitude.Text = location.Latitude.ToString();
                    lblLongitude.Text = location.Longitude.ToString();

                    // je recupere les donnée deja inscrite
                    user = await dataBase.ReadDatabase(user);

                    // j'ajoute les coordonnées
                    user.Longitude = lblLongitude.Text;
                    user.Latitude = lblLatitude.Text;
                    // je lance la carte
                    //await Map.OpenAsync(location);
                    // await Xamarin.Forms.
                    // je met a jour l utilisateur avec les coordoones
                    await dataBase.UpdateUser(user);

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

        }
        #endregion

        #region geocodage


        public async void BtnGeocodage_Clicked(object sender, EventArgs e)
        {
            var placemark = new Placemark
            {
                
                CountryName = "United States",
                AdminArea = "WA",
                Thoroughfare = "Microsoft Building 25",
                Locality = "Redmond"
            };
            var options = new MapLaunchOptions { Name = "Microsoft Building 25" };

            await placemark.OpenMapsAsync();
        }
        #endregion

        #region Custom Map

        public async void BtnCustomMap_Clicked(object sender, EventArgs e)
        {
            //FBDatabase dataBase = new FBDatabase("https://tchat-7c40f.firebaseio.com/"); // url vers la Database firebase
            //var users = await dataBase.GetAllUsers();
           
            try
            {
                 var location = await Geolocation.GetLastKnownLocationAsync();
                // var location = new Location(47.645160, -122.1306032);


                if (location != null)
                   
                {
                     Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    //lblLatitude.Text = location.Latitude.ToString();
                    //lblLongitude.Text = location.Longitude.ToString();

                    // je lis les données dans le user
                    user = await dataBase.ReadDatabase(user);

                    var customMap = new CustomMap
                    {
                        MapType = MapType.Street,
                        WidthRequest = App.ScreenWidth,
                        HeightRequest = App.ScreenHeight,
                    };

                    var pin = new Pin
                    {  
                        Type = PinType.Place,
                        Position = new Position(location.Latitude, location.Longitude),
                        Label = user.Nom,
                        Address = "adresse"
                    };

                    var position = new Position(location.Latitude, location.Longitude);

                    customMap.Circle = new CustomCircle
                    {
                        Position = position,
                        Radius = 1000,
                    };
                    Console.WriteLine("position est = " + position);

                    #region j'ajoute tous les autre cercles

                    foreach (User u in dataBase.Users)
                    {
                        var customPin = new CustomPin();
                          
                        customPin.MapPin.Label = u.Nom;
                        customPin.MapPin.Position = new Position(double.Parse(u.Latitude),double.Parse(u.Longitude));
                        //customPin.MapPin.Label = "1";
                        //pin.MapPin.Address = u.Adresse;
                        customPin.Id = u.Id;
                        customMap.Pins.Add(pin);

                       // customMap.CustomPins.Add(customPin);
                        Console.WriteLine("pin de  : " +u.Nom);
                        Console.WriteLine("Custom pin " + pin);
                    }
                    #region commentaire ancien pin 
                    //public static void ForEach<T>(this System.Collections.Generic.IEnumerable<T> enumeration, Action<T> action);



                    //var pin1 = new CustomPin();
                    //pin1.MapPin.Label = "Test";
                    //pin1.MapPin.Position = new Position(32, 10);
                    //pin1.MapPin.Label = "1";
                    //pin1.MapPin.Address = "394 Pacific Ave, San Francisco CA";
                    //pin1.Id = "Xamarin";

                    //var pin2 = new CustomPin();
                    //pin2.MapPin.Label = "Test2";
                    //pin2.MapPin.Position = new Position(33, 11);
                    //pin2.MapPin.Label = "2";
                    //pin2.MapPin.Address = "394 Pacific Ave, San Francisco CA";
                    //pin2.Id = "Xamarin";

                    //customMap.CustomPins = new List<CustomPin> { pin1, pin2 };
                    //customMap.Pins.Add(pin1.MapPin);
                    //customMap.Pins.Add(pin2.MapPin);

                    //customMap.CustomPins = new List<CustomPin>();

                    //if (dataBase.Items != null && App.Items.Count > 0)
                    //{
                    //    foreach (var t in App.Items)
                    //    {
                    //        var temp = new CustomPin()
                    //        {
                    //            Pin = new Pin()
                    //            {
                    //                Label = t.Name,
                    //                Type = PinType.Place,
                    //                Position = new Position(t.Lat, t.Lon),
                    //                Address = t.Address1
                    //            },
                    //            Url = t.Link
                    //        };
                    //        customMap.CustomPins.Add(temp);
                    //    }
                    //    foreach (var pin in customMap.CustomPins)
                    //    {
                    //        customMap.Pins.Add(pin.Pin);
                    //    }
                    // dont delete below code ,they will save you if timer doesnt work .

                    //var temp1 = new MapSpan(customMap.CustomPins [0].Pin.Position,
                    //              if(Device.OS == TargetPlatform.iOS)
                    //              customMap.MoveToRegion (MapSpan.FromCenterAndRadius (customMap.CustomPins [0].Pin.Position, Distance.FromMiles (0.20)));

                    //    if (Device.OS == TargetPlatform.Android)
                    //        customMap.MoveToRegion(MapSpan.FromCenterAndRadius(customMap.CustomPins[0].Pin.Position, Distance.FromMiles(55.0)));
                    //    if (Device.OS == TargetPlatform.iOS)
                    //    {
                    //        Device.StartTimer(TimeSpan.FromMilliseconds(500), () => {
                    //            customMap.MoveToRegion(MapSpan.FromCenterAndRadius(customMap.CustomPins[0].Pin.Position, Distance.FromMiles(55.0)));
                    //            return false;
                    //        });
                    //    }
                    //}
                    #endregion


                    // customMap.Pins.Add(pin);
                    // customMap.Circle.Add

                     // customMap1.CustomCircle.Add(customMap.Circle);
                        #endregion

                        #region carte

                        // je lance la carte
                    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(10.0)));
                    Content = customMap;

                    // j'ajoute les coordonnées
                    user.Longitude = location.Longitude.ToString();
                    user.Latitude = location.Latitude.ToString();

                    // je met a jour l utilisateur avec les coordoones
                    await dataBase.UpdateUser(user);
                    #endregion

                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }

        }
        #endregion

    }
}
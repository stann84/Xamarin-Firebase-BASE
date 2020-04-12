﻿using System;
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
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                // var location = new Location(47.645160, -122.1306032);


                if (location != null)
                {
                     Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    //lblLatitude.Text = location.Latitude.ToString();
                    //lblLongitude.Text = location.Longitude.ToString();

                    // je recupere les donnée deja inscrite

                    var customMap = new CustomMap
                    {
                        MapType = MapType.Street,
                        WidthRequest = App.ScreenWidth,
                        HeightRequest = App.ScreenHeight
                    };

                    var pin = new Pin
                    {

                        Type = PinType.Place,
                        Position = new Position(location.Latitude, location.Longitude),
                         Label = "moi",
                        Address = "adresse"
                    };

                    var position = new Position(location.Latitude, location.Longitude);
                    customMap.Circle = new CustomCircle
                    {
                        Position = position,
                        Radius = 1000,
                    };
                   // Console.WriteLine("position est = " + position);

                    // j'ajoute tous les autre cercles
                    


                    //customMap.Circle = new List<CustomCircle> {  customMap.Circle };



                    // customMap.Pins.Add(pin);

                    // je lance la carte
                    customMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(10.0)));
                    Content = customMap;
                    // je lis les données dans le user
                    user = await dataBase.ReadDatabase(user);

                    // j'ajoute les coordonnées
                    user.Longitude = location.Longitude.ToString();
                    user.Latitude = location.Latitude.ToString();

                    // je met a jour l utilisateur avec les coordoones
                    await dataBase.UpdateUser(user);

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
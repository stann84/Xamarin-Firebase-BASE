using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestXamarinFirebase.Helper;
using TestXamarinFirebase.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
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
           // var allUsers = await dataBase.GetAllUsers();
            // listUsers.ItemsSource = allUsers;
            // Console.WriteLine(lblLongitude);
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
                    await Map.OpenAsync(location);
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

    }
}
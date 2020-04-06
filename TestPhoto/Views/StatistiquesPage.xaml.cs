using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestXamarinFirebase.Helper;
using TestXamarinFirebase.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestXamarinFirebase
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatistiquesPage : ContentPage
    {
        FBDatabase dataBase = new FBDatabase("https://tchat-7c40f.firebaseio.com/"); // url vers la Database firebase
        User user = new User();         // Objet ou seront stockées les données de l'Utilisateur
        public List<User> Users { get; set; }


        public StatistiquesPage()
        {
            InitializeComponent();
            ReadNbrUsers();
            ReadStatistic();

        }
        public async void ReadNbrUsers()
        {

            // Nombre total d'utlisateur
            Users = await dataBase.GetAllUsers();
            Total.Text = (" Nombre de participants ") + Users.Count.ToString();


        }
        public async void ReadStatistic () {
        // Nombre d'utiliseur avec des symptomes
        
            var list = await dataBase.GetAllUsers();

            int nbrFievre = list.Count(a => a.Fievre == true);
            NbrFievre.Text = nbrFievre.ToString();
            int nbrToux = list.Count(a => a.Toux == true);
            NbrToux.Text = nbrToux.ToString();
            int nbrMauxDeGorge = list.Count(a => a.MauxDeGorge == true);
            NbrMauxDeGorge.Text = nbrMauxDeGorge.ToString();
            int nbrCourbature = list.Count(a => a.Courbature == true);
            NbrCourbature.Text = nbrCourbature.ToString();
            int nbrOdorate = list.Count(a => a.Odorat == true);
            NbrOdorat.Text = nbrOdorate.ToString();
            int nbrFatigue = list.Count(a => a.Fatigue == true);
            NbrFatigue.Text = nbrFatigue.ToString();
            int nbrGeneRespiratoire = list.Count(a => a.GeneRespiratoire == true);
            NbrGeneRespiratoire.Text = nbrGeneRespiratoire.ToString();
            int nbrDiarrhee = list.Count(a => a.Diarrhee == true);
            NbrDiarrhee.Text = nbrDiarrhee.ToString();
            int nbrConjonctivite = list.Count(a => a.Conjonctivite == true);
            NbrConjonctivite.Text = nbrConjonctivite.ToString();
            int nbrDepiste = list.Count(a => a.Depiste == true);
            NbrDepiste.Text = nbrDepiste.ToString();

        }



        public async void Geolocalisation_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MapPage());

        // await dataBase.UpdateUser(user);
    }


    // user.Fievre = Fievre.Text
}
}
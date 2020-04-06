using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestXamarinFirebase.Model;

namespace TestXamarinFirebase.Helper
{
    class FBDatabase
    {
        public FirebaseClient firebase { get; set; }

        public FBDatabase(string adresse)
        {
            firebase = new FirebaseClient(adresse);
        }

        #region lister tout les users

        public async Task<List<User>> GetAllUsers()
        {
            try { 
            var userlist = (await firebase
                    .Child("Users")
                    .OnceAsync<User>())
                    .Select(item => new User(){
                        Fievre = item.Object.Fievre,
                        Diagnostique = item.Object.Diagnostique,
                        Toux = item.Object.Toux,
                        MauxDeGorge = item.Object.MauxDeGorge,
                        Courbature = item.Object.Courbature,
                        Odorat = item.Object.Odorat,
                        Fatigue = item.Object.Fatigue,
                        GeneRespiratoire = item.Object.GeneRespiratoire,
                        Diarrhee = item.Object.Diarrhee,
                        Conjonctivite = item.Object.Conjonctivite,
                        Depiste = item.Object.Depiste,
                    }).ToList();
                return userlist;
            }                    
            catch (Exception e ) {
                Debug.WriteLine($"Error:{e}");
                return null;
            }

        }

        #endregion

        #region Charger un user   
        public async Task<User> GetUser(User user)
        {
            try
            {
                // Charger tous les utilisateurs
                var allUsers = (await firebase
                    .Child("Users")
                    .OnceAsync<User>())
                    .Select(item => new User
                {
                    PhotoUrl = item.Object.PhotoUrl,
                    PhotoName = item.Object.PhotoName,
                    Tel = item.Object.Tel,
                    Email = item.Object.Email,
                    Id = item.Object.Id,
                    Nom = item.Object.Nom,
                    Prenom = item.Object.Prenom,
                    Fievre = item.Object.Fievre,
                    Diagnostique = item.Object.Diagnostique,
                    Toux = item.Object.Toux,
                    MauxDeGorge = item.Object.MauxDeGorge,
                    Courbature = item.Object.Courbature,
                    Odorat = item.Object.Odorat,
                    Fatigue = item.Object.Fatigue,
                    GeneRespiratoire = item.Object.GeneRespiratoire,
                    Diarrhee = item.Object.Diarrhee,
                    Conjonctivite = item.Object.Conjonctivite,
                    Depiste = item.Object.Depiste,
                }).ToList();

                // recherche dans la liste un utilisateur par son Id
                var myUser = allUsers.Where(a => a.Id == user.Id).FirstOrDefault();
                if (myUser != null)
                    return myUser;
                else
                {
                    // Si aucun utilisateur trouvé, on crée le compte dans la dataBase
                    await WriteDataBase(user);
                    return new User { Email = user.Email, Id = user.Id, Nom = user.Nom, Prenom = user.Prenom, PhotoUrl = user.PhotoUrl, Tel = user.Tel };
                }

            }
            catch (Exception e)
            {
                // await DisplayAlert("Erreur", e.Message, "OK");
                return null;
            }
        }

        #endregion

        #region ecrire dans la bdd

        public async Task WriteDataBase(User util)
        {
            await firebase.Child("Users").PostAsync(new User()
            {
                PhotoUrl = util.PhotoUrl,
                Tel = util.Tel,
                Email = util.Email,
                Id = util.Id,
                Nom = util.Nom,
                Prenom = util.Prenom
            });
        }
        #endregion

        #region metre a jour un user

        public async Task UpdateUser(User UpUser)
        {
            var UpdatePerson = (await firebase.Child("Users").OnceAsync<User>()).Where(a => a.Object.Id == UpUser.Id).FirstOrDefault();

            await firebase.Child("Users").Child(UpdatePerson.Key).PutAsync(new User()
            {
                Id = UpUser.Id,
                Email = UpUser.Email,
                Nom = UpUser.Nom,
                Prenom = UpUser.Prenom,
                Tel = UpUser.Tel,
                PhotoUrl = UpUser.PhotoUrl,
                PhotoName = UpUser.PhotoName,
                // Symptomes
                Fievre = UpUser.Fievre,
                Diagnostique = UpUser.Diagnostique,
                Toux = UpUser.Toux,
                MauxDeGorge = UpUser.MauxDeGorge,
                Courbature = UpUser.Courbature,
                Odorat = UpUser.Odorat,
                Fatigue = UpUser.Fatigue,
                GeneRespiratoire = UpUser.GeneRespiratoire,
                Diarrhee = UpUser.Diarrhee,
                Conjonctivite = UpUser.Conjonctivite,
                Depiste = UpUser.Depiste,
                //Geolocalisation
                Longitude = UpUser.Longitude,
                Latitude = UpUser.Latitude,
            });
        }
        #endregion

        #region lire un user

        public async Task<User> ReadDatabase(User user)
        {
            user = await GetUser(user);    // Récuppère les données utilisateurs de la Database
            return user;
        }
        #endregion

    }
}

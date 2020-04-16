﻿namespace TestXamarinFirebase.Model
{
    public class User
    {
        // Identitee
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoName { get; set; }
        public string Tel { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public string Age { get; set; }
        // Symptomes
        public bool Fievre { get; set; }
        public bool Diagnostique { get; set; }
        public bool Toux { get; set; }
        public bool MauxDeGorge { get; set; }
        public bool Courbature { get; set; }
        public bool Odorat { get; set; }
        public bool Fatigue { get; set; }
        public bool GeneRespiratoire { get; set; }
        public bool Diarrhee { get; set; }
        public bool Conjonctivite { get; set; }
        public bool Depiste { get; set; }
        public bool Cutane { get; set; }
        public bool AgnosieGustative { get; set; }
        //Geolocalisation
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}


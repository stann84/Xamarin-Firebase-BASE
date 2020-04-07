﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestXamarinFirebase
{
    public partial class App : Application
    {
        //custom map
        public static double ScreenHeight;
        public static double ScreenWidth;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MenuPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

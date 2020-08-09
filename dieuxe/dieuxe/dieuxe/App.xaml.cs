using dieuxe.Helpers;
using dieuxe.Models;
using dieuxe.Services;
using dieuxe.ViewModels;
using dieuxe.Views;
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dieuxe
{
    public partial class App : Application
    {
       
        public App()
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            //LocationUpdateService = DependencyService.Get<ILocationUpdateService>();
            //LocationUpdateService.LocationChanged += LocationUpdateService_LocationChanged;

            InitializeComponent();
            SetMainPage();
            //MainPage = new NavigationPage(new MainTabbedPage());
            //{
            //    BarBackgroundColor = Color.White
            //};
          
        }

        private async  void SetMainPage()
        {
            if (string.IsNullOrEmpty(Settings.AccessToken)|| string.IsNullOrEmpty(Settings.LoailienHe))
               // if (string.IsNullOrEmpty(Settings.AccessToken))
                {
                MainPage = new NavigationPage(new Login()); 
            }
            else {
                int loainv = Convert.ToInt32(Settings.LoailienHe);

                MainPage = new NavigationPage(new MainTabbedPage(loainv));
                
            }

        }

       

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {
        }
    }
}


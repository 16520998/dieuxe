using dieuxe.Helpers;
using dieuxe.ViewModels;
using dieuxe.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace dieuxe
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void xemlichdangky_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new xemlichdadangky());
        }

        private async void dangkilich_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new dangkythongtin());
        }

        private async void profile_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new profile());
        }

        private async void changePW_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePWPage());
        }

        private async void dangxuat_Clicked(object sender, EventArgs e)
        {
            Settings.AccessToken = "";
            Application.Current.MainPage = new NavigationPage(new Login());
        }
    }
}

using dieuxe.Helpers;
using dieuxe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dieuxe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class getuser : ContentPage
    {

        //push page k có tabbed bar
        public Action<ContentPage> PushPage;

        void NavigateToPage(ContentPage page)
        {
            PushPage?.Invoke(page);
        }
        //

        public getuser()
        {
            InitializeComponent();
            this.BindingContext = new getuserViewModels();
           
        }
        private void dangxuat_Clicked(object sender, EventArgs e)
        {
            Settings.AccessToken = "";
             Application.Current.MainPage = new NavigationPage(new Login());
        }
        private  void doimk_Clicked(object sender, EventArgs e)
        {
            NavigateToPage(new ChangePWPage());
        }
        private  void xemlichdangky_Clicked(object sender, EventArgs e)
        {
            NavigateToPage(new xemlichdadangky());
           
              
        }
        private async void profile_Clicked(object sender, EventArgs e)
        {
            NavigateToPage(new profile());
            
        }
    }
}
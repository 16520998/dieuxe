using dieuxe.Models;
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
    public partial class dangkythongtin : ContentPage
    {
        //push page k có tabbed bar
        public Action<ContentPage> PushPage;

        void NavigateToPage(ContentPage page)
        {
            PushPage?.Invoke(page);
        }
        //
        dangkylichViewmodel dkl;
        public BanDo Map1 { get; set; }
        public dangkythongtin()
        {
            InitializeComponent();
            
            //this.Master = new MasterPage();
            dkl = new dangkylichViewmodel();
            BindingContext = dkl;
            Map1 = new dieuxe.Views.BanDo(dkl);
            listden.IsVisible = false;
            sua.IsVisible = false;
        }
        public dangkythongtin(DangKyLichChiTiet lich)
        {
            InitializeComponent();

            //this.Master = new MasterPage();
            dkl = new dangkylichViewmodel();
            dkl.Lich = lich;
            BindingContext = dkl;
            tennguoidi.Text = lich.TenNguoiDi;
            noiden.Text = lich.NoiDen;
            noidi.Text = lich.NoiDi;
            tennguoidi.Text = lich.TenNguoiDi;
            gioden.Time= TimeSpan.Parse(lich.GioDen);
            giove.Time = TimeSpan.Parse(lich.GioVe);
            ngayden.Date = DateTime.Parse(lich.NgayDen);
            ngayve.Date = DateTime.Parse(lich.NgayVe);
            songuoi.Text = lich.SoNguoi.ToString();
            sdt.Text = lich.SdtLienLac;
            Map1 = new dieuxe.Views.BanDo(dkl);
            listden.IsVisible = false;
            dk.IsVisible = false;
            sua.IsVisible = true;
        }
           
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Login());
        }
        private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            listdi.IsVisible = false;
            listden.IsVisible = false;
        }

        private  void mapdi_Clicked(object sender, EventArgs e)
        {
            listden.IsVisible = false;
            listdi.IsVisible = false;
            Map1.Loai = "noidi";
            //Map1.DisplayCurLocation();
            NavigateToPage(Map1);

        }

        private  void mapden_Clicked(object sender, EventArgs e)
        {
            listden.IsVisible = false;
            listdi.IsVisible = false;
            Map1.Loai = "noiden";
            //Map1.DisplayCurLocation();
            NavigateToPage(Map1);
        }

        private async void noiden_TextChanged(object sender, TextChangedEventArgs e)
        {
            listden.IsVisible = true;
            listdi.IsVisible = false;

            if (!string.IsNullOrWhiteSpace(dkl.NoiDen))
            {
                await dkl.GetPlacesPredictionsAsync("noiden");
            }
        }
        private async void noidi_TextChanged(object sender, EventArgs eventArgs)
        {

            listdi.IsVisible = true;
            listden.IsVisible = false;

            if (!string.IsNullOrWhiteSpace(dkl.NoiDi))
            {
                await dkl.GetPlacesPredictionsAsync("noidi");
            }

        }

        private void listkq_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //listkq.IsVisible = false;
            var details = e.Item as placesearchdetail.Addressdi;
            noidi.Text = details.Address;
            listdi.IsVisible = false;
        }
        private void listkq1_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            //listkq1.IsVisible = false;
            var details = e.Item as placesearchdetail.Addressdden;
            noiden.Text = details.Address;
            listden.IsVisible = false;
        }

        

        private async void xemlich_Clicked(object sender, EventArgs e)
        {

            var button = sender as Button;
            var lich = button.BindingContext as DangKyLichChiTiet;

        }

        private void dk_Clicked(object sender, EventArgs e)
        {
            listden.IsVisible = false;
            listdi.IsVisible = false;
        }

        private async void sua_Clicked(object sender, EventArgs e)
        {
            listden.IsVisible = false;
            listdi.IsVisible = false;
            var button = sender as Button;
            var lich = button.BindingContext as DangKyLichChiTiet;
          dkl.SuaCommand.Execute(lich);
            await Navigation.PopAsync();
        }


        

    }
}
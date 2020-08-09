using dieuxe.ViewModels;
using dieuxe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;
using dieuxe.Helpers;

namespace dieuxe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class xemlichdadangky : ContentPage
    {
        xemlichdadangkyViemodel xl;
        public xemlichdadangky()
        {
            InitializeComponent();
            xl = new xemlichdadangkyViemodel();
            BindingContext = xl;
            
        }



        private async  void xoa_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var lich = button.BindingContext as DangKyLichChiTiet;
            
            xl.deleteCommand.Execute(lich);
           
        }

        private async void sua_Clicked(object sender, EventArgs e)
        {
            
            var button = sender as Button;
            var lich = button.BindingContext as DangKyLichChiTiet;
            await Navigation.PushAsync(new dangkythongtin(lich));
        }

        private void submit_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var lich = button.CommandParameter as List<DangKyLichChiTiet>;
            var vm = BindingContext as xemlichdadangkyViemodel;
            vm.PostCommand.Execute(lich);

            
            //if (selectedItem != null)
            //{
            //    Debug.WriteLine("name:>." + selectedItem.SelectItem);
            //}
            foreach (lich i in xemlich.ItemsSource)
            {
                Debug.WriteLine(i.SelectItem==null?"null": "giá trị " + i.SelectItem.TenLienHe);
            }

        }

       
    }
}

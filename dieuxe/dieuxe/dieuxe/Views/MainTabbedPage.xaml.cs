using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dieuxe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage : Xamarin.Forms.TabbedPage
    {
       
        public MainTabbedPage()
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
        }
        public MainTabbedPage(int id)
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            NavigationPage tb = new NavigationPage(new notifyPage());
            tb.IconImageSource = "notify.png";
            tb.Title = "Thông báo";

            NavigationPage tk = new NavigationPage(new ThongkeView());
            tk.IconImageSource = "thongke.png";
            tk.Title = "Thống kê";


            var trangtaikhoan = new getuser
            {
                PushPage = (page) =>
                {
                    Navigation.PushAsync(page, true);
                }
            };
            NavigationPage menu = new NavigationPage(trangtaikhoan);
            menu.IconImageSource = "menu.png";
            menu.Title = "Tài khoản";

            if (id == 1)
            {
                var trangnhanvien = new dangkythongtin
                {
                    PushPage = (page) =>
                    {
                        Navigation.PushAsync(page, true);
                    }
                };
                NavigationPage navigationPage = new NavigationPage(trangnhanvien);
                navigationPage.IconImageSource = "home.png";
                navigationPage.Title = "Trang chủ";


                Children.Add(navigationPage);
                Children.Add(tb);
                Children.Add(menu);
            }


            if (id == 2)
            {
                var trangtaixe = new TaiXeView
                {
                    PushPage = (page) =>
                    {
                        Navigation.PushAsync(page, true);
                    }
                };
                NavigationPage navigationPage = new NavigationPage(trangtaixe);
                navigationPage.IconImageSource = "home.png";
                navigationPage.Title = "Trang chủ";


                Children.Add(navigationPage);
                Children.Add(tb);
                Children.Add(menu);
            }
            if (id == 3)
            {
                var trangquanly = new QuanLy
                {
                    PushPage = (page) =>
                    {
                        Navigation.PushAsync(page, true);
                    }
                };
                NavigationPage navigationPage = new NavigationPage(trangquanly);
                navigationPage.IconImageSource = "home.png";
                navigationPage.Title = "Trang chủ";


                Children.Add(navigationPage);
                Children.Add(tk);
                Children.Add(menu);
            }
            
        }
    }

}
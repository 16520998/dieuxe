
using dieuxe.Models;
using dieuxe.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dieuxe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuanLy : ContentPage,INotifyPropertyChanged
    {

        public Action<ContentPage> PushPage;

        void NavigateToPage(ContentPage page)
        {
            PushPage?.Invoke(page);
        }


        //sửa dòng này
        private List<Xe> _listngunguoi;
        public List<Xe> listngunguoi
        {
            get { return _listngunguoi; }
            set{ 
                _listngunguoi = value;
                OnPropertyChanged("listngunguoi");
            }
        }
        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        //
        QuanLyVM quanlyviewmodel;
        public QuanLy()
        {
            //BindingContext = this;
            listngunguoi = new List<Xe> { new Xe { BienSoXe="ngu" }, new Xe { BienSoXe = "quá" } };
            InitializeComponent();
            //BanDo map = new BanDo();
            //var bindingContext = new QuanLyVM();
            BindingContext = quanlyviewmodel = new QuanLyVM();
            ten.Text = Helpers.Settings.TenLienHe;
            
        }
        protected async  override void OnAppearing()
        {
            base.OnAppearing();
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.Token.ThrowIfCancellationRequested();
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            await quanlyviewmodel.LayDuLieu(cts);
            //cts.Cancel();
            //try
            //{
            //    await KiemTraThoiGian(stopwatch, cts);
            //}
            //catch (OperationCanceledException e)
            //{
            //    Console.WriteLine($"{nameof(OperationCanceledException)} thrown with message: {e.Message}");


            //}
            //finally
            //{
            //    cts.Dispose();
            //}
        }
        private async Task KiemTraThoiGian(System.Diagnostics.Stopwatch stopwatch, CancellationTokenSource cts)
        {
            while (!quanlyviewmodel.LayDuLieuXong)
            {
                //if (cts.Token.IsCancellationRequested)
                //{
                //    cts.Token.ThrowIfCancellationRequested();
                //}
                if (stopwatch.Elapsed.TotalSeconds > 1)
                {
                    Console.WriteLine("time out..");

                    bool action = await DisplayAlert("Đã xảy ra sự cố", "Mất nhiều thời gian để cập nhật dữ liệu.\n Bạn có muốn tải lại trang không?", "Có", "Tiếp tục chờ");
                    
                    if(action && cts != null)
                    {
                        cts.Cancel();
                        cts = new CancellationTokenSource();
                        stopwatch.Restart();
                        quanlyviewmodel.LayDuLieu(cts);
                    }
                }
                if(cts == null)
                {
                    stopwatch.Stop();
                    break;
                }
            }

        }

        private void chitietdangky(object sender, ItemTappedEventArgs e)
        {
            var itemtapped = e.Item as DangKyLichChiTiet;
            chitietngaydangky.Text = itemtapped.NgayDangKy;
            //chitietnhanvien.Text = itemtapped.nhanviendangky;
            chitietsonguoi.Text = itemtapped.SoNguoi.ToString();
            chitietnoidon.Text = itemtapped.NoiDi;
            chitietnoiden.Text = itemtapped.NoiDen;
            chitietgioden.Text = itemtapped.GioDen;
            chitietgiove.Text = itemtapped.GioVe;

            popupcontent.IsVisible = true;
            this.popuplayout.AnchorX = 0.5;
            this.popuplayout.AnchorY = 0.5;

            Animation scaleAnimation = new Animation(
                f => this.popuplayout.Scale = f,
                0.5,
                1,
                Easing.SinInOut);

            Animation fadeAnimation = new Animation(
                f => this.popuplayout.Opacity = f,
                0.2,
                1,
                Easing.SinInOut);

            scaleAnimation.Commit(this.popuplayout, "popupScaleAnimation", 250);
            fadeAnimation.Commit(this.popuplayout, "popupFadeAnimation", 250);
        }

        private void showpopup(object sender, EventArgs e)
        {
            popupcontent.IsVisible = true;
            this.popuplayout.AnchorX = 0.5;
            this.popuplayout.AnchorY = 0.5;

            Animation scaleAnimation = new Animation(
                f => this.popuplayout.Scale = f,
                0.5,
                1,
                Easing.SinInOut);

            Animation fadeAnimation = new Animation(
                f => this.popuplayout.Opacity = f,
                0.2,
                1,
                Easing.SinInOut);

            scaleAnimation.Commit(this.popuplayout, "popupScaleAnimation", 250);
            fadeAnimation.Commit(this.popuplayout, "popupFadeAnimation", 250);
        }

        private void show(object sender, EventArgs e)
        {
            DisplayAlert("vãi", "đêa", "OKK");
        }

        private async void close(object sender, EventArgs e)
        {
            await Task.WhenAny<bool>(this.popuplayout.FadeTo(0, 200, Easing.SinInOut));
            popupcontent.IsVisible = false;
        }

        private void buttonphantich_Clicked(object sender, EventArgs e)
        {
            if (!quanlyviewmodel.LayDuLieuXong)
                return;

            List<DangKyLichChiTiet> dsdangky = danhsachdangky.ItemsSource as List<DangKyLichChiTiet>;
            NavigateToPage(new BanDo(dsdangky, this.BindingContext as QuanLyVM));
        }
    }
}
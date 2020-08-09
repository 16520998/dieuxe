using dieuxe.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using dieuxe.Helpers;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;
using Android.Net.Wifi.Aware;

namespace dieuxe.ViewModels
{
    class ThongKeViewModel:INotifyPropertyChanged
    {
        
        
        private List<ThongKe> _ThongkeCuoiThang { get; set; }
        public List<ThongKe> ThongkeCuoiNam
        {
            get { return _ThongkeCuoiThang; }
            set
            {
                _ThongkeCuoiThang = value;
                OnPropertyChanged();
            }
        }


        private List<ThongKe> _Thanghientai { get; set; }
        public List<ThongKe> Thanghientai
        {
            get { return _Thanghientai; }
            set
            {
                _Thanghientai = value;
                OnPropertyChanged();
            }
        }

        private decimal _TongChiPhi;
        public decimal TongChiPhi
        {
            get
            {
                return _TongChiPhi;
            }
            set { _TongChiPhi = value;
                OnPropertyChanged();
            }
        }
        private int _TongNguoi;
        public int TongNguoi
        {
            get
            {
                return _TongNguoi;
            }
            set
            {
                _TongNguoi = value;
                OnPropertyChanged();
            }
        }

        private bool _ThongKeNam;
        public bool ThongKeNam
        {
            get
            {
                return _ThongKeNam;
            }
            set
            {
                _ThongKeNam = value;
                OnPropertyChanged();
            }
        }

        private bool _isRefresh;
        public bool isRefresh { get { return _isRefresh; } set { _isRefresh = value; OnPropertyChanged(); } }
        private bool _check;
        public bool check { get { return _check; } set { _check = value; OnPropertyChanged(); } }
        public ThongKeViewModel()
        {
            get();
        }
        private async Task get()
        {
            await getdata();

        }

        public ICommand refreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    isRefresh = true;
                    var accessToken = Settings.AccessToken;
                    await GetThongKe("https://apidieuxe.azurewebsites.net/api/thongkecuoithang", accessToken);
                    isRefresh = false;
                });
            }
        }
        private async Task getdata()
        {
            var accessToken = Settings.AccessToken;
            await GetThongKe("https://apidieuxe.azurewebsites.net/api/thongkecuoithang", accessToken);
            if (_ThongkeCuoiThang != null)
            {
                check = true;
            }
        }
        public async Task<List<ThongKe>> GetThongKe(string path, string accessToken)
        //public async void GetUserAsync(string path)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync(path);
            List<ThongKe> list = new List<ThongKe>();
            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var st = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                list = JsonConvert.DeserializeObject<List<ThongKe>>(content, st);
                ThongkeCuoiNam = list;
                string now = DateTime.Now.ToString("MM");
               var T = ThongkeCuoiNam.Where(i => i.Thang==Convert.ToInt32(now)).ToList();
                Thanghientai = T;
                TongNguoi = list.Sum(i => i.TongNguoiDiTrongThang);
                TongChiPhi = list.Sum(i => i.TongChiPhi);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Tải dữ liệu thất bại, hãy kiêm tra két nối mạng", "OK");
                list = null;
                ThongkeCuoiNam = list;
                TongNguoi = 0;
                TongChiPhi=0;
            }
            return list;
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
    }
}

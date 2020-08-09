using dieuxe.Helpers;
using dieuxe.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dieuxe.ViewModels
{
    class DieuXeViewModel : INotifyPropertyChanged
    {
        private string _MaDieuxe { get; set; }
        private string _NoiDi { get; set; }
        private string _GioDen { get; set; }
        private string _GioVe { get; set; }
        private int _TaiXe { get; set; }
        private string _TrangThai { get; set; }
        private string _TuyenDuong { get; set; }
        private string _GhiChu { get; set; }
        private string _BienKiemSoat { get; set; }
        private decimal _ChiPhi { get; set; }
        private string _NgayDangKy { get; set; }
        private string _NgayDi { get; set; }
        private int _SoNguoi { get; set; }
        private List<Dieuxe> _ListChuyenchuaDiHomNay;
        public List<Dieuxe> ListChuyenchuaDiHomNay
        {
            get { return _ListChuyenchuaDiHomNay; }
            set
            {
                if (_ListChuyenchuaDiHomNay != value)
                {
                    _ListChuyenchuaDiHomNay = value;
                    OnPropertyChanged();
                }
            }
        }
        private List<Dieuxe> _ListChuyenDaDiHomNay;
        public List<Dieuxe> ListChuyenDaDiHomNay
        {
            get { return _ListChuyenDaDiHomNay; }
            set
            {
                if (_ListChuyenDaDiHomNay != value)
                {
                    _ListChuyenDaDiHomNay = value;
                    OnPropertyChanged();
                }
            }
        }
        public string MaDieuxe
        {
            get { return _MaDieuxe; }
            set
            {
                if (_MaDieuxe != value)
                {
                    _MaDieuxe = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NoiDi
        {
            get { return _NoiDi; }
            set
            {
                if (_NoiDi != value)
                {
                    _NoiDi = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GioDen
        {
            get { return _GioDen; }
            set
            {
                if (_GioDen != value)
                {
                    _GioDen = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GioVe
        {
            get { return _GioVe; }
            set
            {
                if (_GioVe != value)
                {
                    _GioVe = value;
                    OnPropertyChanged();
                }
            }
        }
        public int TaiXe
        {
            get { return _TaiXe; }
            set
            {
                if (_TaiXe != value)
                {
                    _TaiXe = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TrangThai
        {
            get { return _TrangThai; }
            set
            {
                if (_TrangThai != value)
                {
                    _TrangThai = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TuyenDuong
        {
            get { return _TuyenDuong; }
            set
            {
                if (_TuyenDuong != value)
                {
                    _TuyenDuong = value;
                    OnPropertyChanged();
                }
            }
        }
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                if (_GhiChu != value)
                {
                    _GhiChu = value;
                    OnPropertyChanged();
                }
            }
        }
        public string BienKiemSoat
        {
            get { return _BienKiemSoat; }
            set
            {
                if (_BienKiemSoat != value)
                {
                    _BienKiemSoat = value;
                    OnPropertyChanged();
                }
            }
        }
        public decimal ChiPhi
        {
            get { return _ChiPhi; }
            set
            {
                if (_ChiPhi != value)
                {
                    _ChiPhi = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NgayDangKy
        {
            get { return _NgayDangKy; }
            set
            {
                if (_NgayDangKy != value)
                {
                    _NgayDangKy = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NgayDi
        {
            get { return _NgayDi; }
            set
            {
                if (_NgayDi != value)
                {
                    _NgayDi = value;
                    OnPropertyChanged();
                }
            }
        }
        public int SoNguoi
        {
            get { return _SoNguoi; }
            set
            {
                if (_SoNguoi != value)
                {
                    _SoNguoi = value;
                    OnPropertyChanged();
                }
            }
        }

        public DieuXeViewModel()
        {
            var accessToken = Settings.AccessToken;
            //GetDadiLichTheongay("http://localhost:44399/api/getDadicuaTaixe", accessToken);
            //GetChuaDiLichTheongay("http://localhost:44399/api/getChuadicuaTaixe", accessToken);

        }
        public async void GetDadiLichTheongay(string path, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync(path);

            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ListChuyenDaDiHomNay = JsonConvert.DeserializeObject<List<Dieuxe>>(content);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("error", "không thể get lịch today :( ", "OK");
                ListChuyenDaDiHomNay = null;
            }
        }

        public async void GetChuaDiLichTheongay(string path, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync(path);

            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ListChuyenchuaDiHomNay = JsonConvert.DeserializeObject<List<Dieuxe>>(content);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("error", "không thể get lịch today :( ", "OK");
                ListChuyenchuaDiHomNay = null;
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

    }

}

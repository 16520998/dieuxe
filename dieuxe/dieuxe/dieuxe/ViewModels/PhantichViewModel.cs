using dieuxe.Helpers;
using dieuxe.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace dieuxe.ViewModels
{
    public class PhantichViewModel : INotifyPropertyChanged
    {
        private List<DangKyLichChiTiet> _Listdklct { get; set; }
        public List<DangKyLichChiTiet> Listdklct
        {
            get {return _Listdklct; }
             set
            {
                if (_Listdklct != value)
                {
                    _Listdklct = value;
                    OnPropertyChanged();
                }
            }   
        }
        private List<LienHeUser> _ListTaixe { get; set; }
        public List<LienHeUser> ListTaixe
        {
            get { return _ListTaixe; }
            set
            {
                if (_ListTaixe != value)
                {
                    _ListTaixe = value;
                    OnPropertyChanged();
                }
            }
        }
        public PhantichViewModel()
        {
            //var accessToken = Settings.AccessToken;
            //GetLichdangkytrongngayAsync("https://apidieuxe20200508212151.azurewebsites.net/api/lichchayxehomnay/, accessToken); nhớ sửa !

            // GetTaixeAsync("https://apidieuxe20200508212151.azurewebsites.net/api/lichchayxehomnay/, accessToken); nhớ sửa !

            ListTaixe=new List<LienHeUser> {
                new LienHeUser{TenLienHe = "Quang driver", LoailienHe = 2, SdtLienLac = "0351354589"},
                new LienHeUser{TenLienHe = "Thuận driver", LoailienHe = 2, SdtLienLac = "0351354581"},
                new LienHeUser{TenLienHe = "Tưa driver", LoailienHe = 2, SdtLienLac = "0351354582"},
                new LienHeUser{TenLienHe = "Huy driver", LoailienHe = 2, SdtLienLac = "0351354583"},
                new LienHeUser{TenLienHe = "Cường driver", LoailienHe = 2, SdtLienLac = "0351354584"}
                };
            //chuyenchuadi.Add(new ChuyenXe { madieuxe = "DX1017", nhanviendangky = "Sk02", songuoi = 2, diemdau = "48 tăng nhơn phú", diemcuoi = "điểm cuối chưa biết", tuyenduong = "ytbaAoavjS_@AEFCNJl@BRAVCF{@u@UIo@Oa@QOMIOEQ?SHs@RaAFk@CYe@_Bi@{AkAqDa@}@W[[SaB}@a@Wo@i@]k@z@EfACVGlC{AXUdAiATYJGRK^GtAIp@CrACdB?j@NnAb@d@RbA{CzAsFdAwEp@qDZ}BHa@Na@`@]dBcAREPDd@D~A`@h@Ff@@LEtEyCbBeAhMyI|HmF^WQUqCqEeBuCsBsCyMeSyC}EuG}Jc@o@{CuEuC}DsAgB{BsBUUsDaC{@i@mEkCwC}AaFyC}FaEaCgB{AkAkCeCaBaBUa@Q_@Ku@IOOMOKSCc@BYROZC\\@J_@v@mGdHuN`OoKjLkAtA@IAQIUOQCA", giodi = "8:00 20/2/2020", giove = "8:00 20/2/2020", taixe = "SK14", bienkiemsoat = "51A - 1223", trangthai = "chuahoantat", ghichu = "" });

        }

        public async Task<List<DangKyLichChiTiet>> GetLichdangkytrongngayAsync(string path, string accessToken)// lấy ds lịch đk trong ngày 
        //public async void GetUserAsync(string path)
        {
            var client = new HttpClient();
            //LienHeUser result = new LienHeUser();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessToken);
            HttpResponseMessage response = await client.GetAsync(path);

            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Listdklct = JsonConvert.DeserializeObject <List<DangKyLichChiTiet>>(content);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("error", "Lấy danh sách thất bại ", "OK");
                Listdklct = null;
            }
            return Listdklct;
        }
        public async Task<List<LienHeUser>> GetTaixeAsync(string path, string accessToken)// lấy ds lịch đk trong ngày 
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(accessToken);
            HttpResponseMessage response = await client.GetAsync(path);

            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ListTaixe = JsonConvert.DeserializeObject<List<LienHeUser>>(content);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("error", "Lấy danh sách thất bại ", "OK");
                ListTaixe = null;
            }
            return ListTaixe;
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

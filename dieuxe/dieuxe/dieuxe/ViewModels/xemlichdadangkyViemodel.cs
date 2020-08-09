using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using dieuxe.Helpers;
using dieuxe.Models;
using dieuxe.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace dieuxe.ViewModels
{
   public class xemlichdadangkyViemodel : INotifyPropertyChanged
    {
       
        private List<DangKyLichChiTiet> _listLich { get; set; }
       
        public List<DangKyLichChiTiet> listLich
        {
            get { return _listLich; }
            set
            {
                _listLich = value;
                OnPropertyChanged();
            }
        }



        public List<lich> guilichdieuxe { get; set; }






        public lich _schedule { get; set; }
        
        private LienHeUser _Taixe { get; set; }
        public LienHeUser Taixe
        {
            get { return _Taixe; }
            set
            {
                _Taixe = value;
                
                OnPropertyChanged();
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
        public  xemlichdadangkyViemodel()
        {
            //listLich = lich;
            var accessToken = Settings.AccessToken;
            GetlichAsync(" https://apidieuxe.azurewebsites.net/api/getDangkyChitietUser", accessToken);
            
        }
        public ICommand deleteCommand
        {
            get
            {
                return new Command<DangKyLichChiTiet>(async (lich) =>
                {
                    var accessToken = Settings.AccessToken;
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var response = await client.DeleteAsync("https://apidieuxe.azurewebsites.net/api/tblDangKyliches/" + lich.DangKyLichId);
                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Xóa lịch thành công", "OK");
                        await GetlichAsync(" https://apidieuxe.azurewebsites.net/api/getDangkyChitietUser", accessToken);

                        //foreach(var i in listLich) 
                        //{
                        //    if(lich.DangKyLichChiTietId == i.DangKyLichChiTietId)
                        //        listLich.Remove(i);
                        //} 
                        
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "xóa thất bại :(", "Thử lại");
                    }
                });
            }

        }
       
        
        public async  Task <List<DangKyLichChiTiet>> GetlichAsync(string path, string accessToken)
        //public async void GetUserAsync(string path)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync(path);
            List<DangKyLichChiTiet> list = new List<DangKyLichChiTiet>();
            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var st = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                list = JsonConvert.DeserializeObject<List<DangKyLichChiTiet>>(content,st);
                listLich = list;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Thông báo", "Tải dữ liệu thất bại, hãy kiêm tra két nối mạng", "OK");
                list = null;
                listLich = list;
            }
            return list;
        }

        public ICommand PostCommand
        {
            get
            {
                return new Command <List<lich>> (async (postlich) =>
                {
                    var accessToken = Settings.AccessToken;
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var json = JsonConvert.SerializeObject(postlich);
                    HttpContent content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PutAsync("https://apidieuxe20200508212151.azurewebsites.net/api/guilichdieuxe/", content);
                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Notify?", "cập nhật thành công !", "OK");
                        await Application.Current.MainPage.Navigation.PushAsync(new xemlichdadangky());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Delete failed :(", "Again");
                    }
                });
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

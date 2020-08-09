using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Collections.ObjectModel;
using System.Text;
using System.Runtime.CompilerServices;
using dieuxe.Models;
using dieuxe.Helpers;
using System.Net.Http.Headers;
using Xamarin.Forms;
using System.Windows.Input;
using System.Threading.Tasks;

namespace dieuxe.ViewModels
{
    public class getuserViewModels : INotifyPropertyChanged
    {
        private int _LienHeID { get; set; }
        private string _TenLienHe { get; set; }
        private int _LoailienHe { get; set; }
        private string _SdtLienLac { get; set; }
        private string _BoPhan { get; set; }
        private string _Email { get; set; }

        private LienHeUser _LienHeUser;
        public LienHeUser LienHeUser
        {
            get { return _LienHeUser; }
            set
            {
                _LienHeUser = value;
                OnPropertyChanged();
            }
        }
        public ICommand LogoutCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Settings.AccessToken = "";

                });

            }
        }
        public  getuserViewModels()
        {
            var accessToken = Settings.AccessToken;
            
        }
        public async Task<LienHeUser> GetUserProfileAsync(string path, string accessToken)
        //public async void GetUserAsync(string path)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync(path);
           

            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                LienHeUser  = JsonConvert.DeserializeObject<LienHeUser>(content);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Thông báo", "bạn nên đăng nhập :( ", "OK");
               LienHeUser  = null;
            }
            return LienHeUser;
        }

        public int LienHeID
        {
            get { return _LienHeID; }
            set
            {
                _LienHeID = value;
                OnPropertyChanged();
            }
        }
        public string TenLienHe
        {
            get { return _TenLienHe; }
            set
            {
                _TenLienHe = value;
                OnPropertyChanged();
            }
        }
        public int LoailienHe
        {
            get { return _LoailienHe; }
            set
            {
                _LoailienHe = value;
                OnPropertyChanged();
            }
        }
        public string SdtLienLac
        {
            get { return _SdtLienLac; }
            set
            {
                _SdtLienLac = value;
                OnPropertyChanged();
            }
        }
        public string BoPhan
        {
            get { return _BoPhan; }
            set
            {
                _BoPhan = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                OnPropertyChanged();
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

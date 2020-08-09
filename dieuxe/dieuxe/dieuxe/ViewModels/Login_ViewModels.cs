using dieuxe.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using dieuxe.Models;
using dieuxe.Views;
using Xamarin.Essentials;
using System.Text.RegularExpressions;

namespace dieuxe.ViewModels
{
    public class Login_ViewModels : INotifyPropertyChanged
    {
        public getuserViewModels get;
        private string _tennv { get; set; }
        private string _Email { get; set; }
        private string _MatKhau { get; set; }
        private string _SdtLienLac { get; set; }
        private int _LienHeId { get; set; }

        public ICommand ForgotCommand
        {
            get
            {
                return new Command(async () =>
                {
                            await Application.Current.MainPage.Navigation.PushAsync(new ForgotPwPage()); 
                });
            } 
        }
        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    
                    if (String.IsNullOrEmpty(password))
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Bạn chưa nhập mật khẩu!", "OK");
                    }
                    else { 

                    var keyValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("username",email),
                        new KeyValuePair<string, string>("password",password),
                        new KeyValuePair<string, string>("grant_type","password"),
                    };
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://apidieuxe.azurewebsites.net/token");
                    request.Content = new FormUrlEncodedContent(keyValues);
                    var client = new HttpClient();
                    var response = await client.SendAsync(request);
                    var jwt = await response.Content.ReadAsStringAsync();// wait for response access token
                    JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);
                    var accessToken = jwtDynamic.Value<string>("access_token");
                    Settings.AccessToken = accessToken;
                    Debug.WriteLine(jwt);
                    //response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        
                        Settings.email = email;
                        Settings.password = password;
                        get = new getuserViewModels();
                        LienHeUser info = await get.GetUserProfileAsync("https://apidieuxe.azurewebsites.net/api/profile", Settings.AccessToken);
                        Settings.LienHeID = info.LienHeID.ToString();
                        Settings.LoailienHe = info.LoailienHe.ToString();
                        Settings.SdtLienLac = info.SdtLienLac;
                        Settings.TenLienHe = info.TenLienHe;
                        Settings.BoPhan = info.BoPhan;
                        Settings.Email = info.Email;
                        
                        int loainv = Convert.ToInt32(Settings.LoailienHe);
                            if (loainv == 1)//nhân viên=> trang nhân viên
                            {
                                Settings.chucvu = "nhân viên";
                            }
                            if (loainv == 2)
                            {
                                Settings.chucvu = "Tài xế";

                            }
                             if (loainv == 3)// người điều xe
                            {
                                Settings.chucvu = "Người điều xe";
                            }
                            Application.Current.MainPage = new NavigationPage(new MainTabbedPage(loainv));
                        }
                        else
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "tài khoản hoặc mật khẩu không đúng ", "OK");
                    }
                    }
                });
            }

        }
        public Login_ViewModels()
        {
            email = Settings.email;
            password = Settings.password;
        }
        public string username
        {
            get { return _tennv; }
            set
            {
                _tennv = value;
                OnPropertyChanged();
            }
        }
        public string email
        {
            get { return _Email; }
            set
            {
                _Email = value;
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
        public string password
        {
            get { return _MatKhau; }
            set
            {
                _MatKhau = value;
                OnPropertyChanged();
            }
        }
        public int LienHeId
        {
            get { return _LienHeId; }
            set
            {
                _LienHeId = value;
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

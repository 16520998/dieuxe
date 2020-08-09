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
using System.Windows.Input;
using Xamarin.Forms;

namespace dieuxe.ViewModels
{
    class ForgotPasswordViewModel : INotifyPropertyChanged
    {

        private string _email { get; set; }
        public string email { get { return _email; } set { _email = value;OnPropertyChanged(); } }

        private string _newPW { get; set; }
        public string newPW { get { return _newPW; } set { _newPW = value; OnPropertyChanged(); } }
        private string _pw { get; set; }
        public string pw { get { return _pw; } set { _pw = value; OnPropertyChanged(); } }
        public ICommand ForgotCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (String.IsNullOrEmpty(email))
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Bạn chưa nhập email!", "OK");
                    }
                    else
                    {
                        var client = new HttpClient();
                        HttpResponseMessage response = await client.GetAsync("https://localhost:44328/forgotpassword/?" + email);

                        System.Diagnostics.Debug.WriteLine(response);
                    if (response.IsSuccessStatusCode)
                        {
                            await Application.Current.MainPage.DisplayAlert("Thông báo", "Một Email đã được gửi vào"+ email, "OK");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Thông báo", "email không tồn tại ", "OK");
                        }
                    }
                });
            }

        }
        public ICommand ChangePasswordCommand
        {
            get
            {
                return new Command(async () =>
                {
                if (pw != Settings.password)
                {
                    await Application.Current.MainPage.DisplayAlert("Thông báo", "Mật khẩu hiện tại không đúng!", "OK");
                }
                else
                {
                    Users user = new Users()
                    {
                       
                        MatKhau=newPW
                    };

                    try
                    {
                        var accessToken = Settings.AccessToken;
                        var client = new HttpClient();
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                        string jsonData = JsonConvert.SerializeObject(user);
                        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync("https://apidieuxe.azurewebsites.net/api/ChangePassword", content);
                        response.EnsureSuccessStatusCode();
                        if (response.IsSuccessStatusCode)
                        {
                            await Application.Current.MainPage.DisplayAlert("Thông báo", "Đổi mật khẩu thành công", "OK");

                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Thông báo", "mật khẩu hiện tại không đúng", "Again");
                        }

                    }
                    catch
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "không thể kết nối với server ", "OK");
                    }
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

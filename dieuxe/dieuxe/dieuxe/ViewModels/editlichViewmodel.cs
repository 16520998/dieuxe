using dieuxe.Helpers;
using dieuxe.Models;
using dieuxe.Views;
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
    class editlichViewmodel
    {
        public lich Lich { get; set; }
        
        public ICommand deleteCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accessToken = Settings.AccessToken;
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var response = await client.DeleteAsync("https://apidieuxe20200508212151.azurewebsites.net/api/lichdangki/" + Lich.sodangky);
                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Notify?", "deleted success!", "OK");
                        await Application.Current.MainPage.Navigation.PushModalAsync(new xemlichdadangky());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Delete failed :(", "Again");
                    }
                });
            }

        }

        public ICommand SuaCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accessToken = Settings.AccessToken;
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var json = JsonConvert.SerializeObject(Lich);
                    HttpContent content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PutAsync("https://apidieuxe20200508212151.azurewebsites.net/api/lichdangki/" + Lich.sodangky, content);
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

        
        
    }   
}

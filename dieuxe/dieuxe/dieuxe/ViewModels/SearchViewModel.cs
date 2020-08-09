using dieuxe.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static dieuxe.Models.placesearchdetail;

namespace dieuxe.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private static readonly string APIkey = "AIzaSyB5W0x1aKzeLeeX2KeFeDk09WnJ53xFQQg";
        private const string APIkey1 = "AIzaSyBkWiDHc4rH47SLkGKdtbhNHR6EgqNfzrs";
        private readonly string geocodinglocation = "https://maps.googleapis.com/maps/api/geocode/json?key={0}&latlng={1},{2}";
        private readonly string searchtextQuery = "https://maps.googleapis.com/maps/api/place/autocomplete/json?key={0}&input={1}&components=country:vn";
        private static HttpClient _client;
        public static HttpClient client => _client ?? (_client = new HttpClient());

        private Temperatures _temperatures;
        private ObservableCollection<AddressInfo> _addresses;
        public ObservableCollection<AddressInfo> Addresses
        {
            get => _addresses ?? (_addresses = new ObservableCollection<AddressInfo>());
            set
            {
                if (_addresses != value)
                {
                    _addresses = value;
                    OnPropertyChanged();
                }
            }
        }
        public Temperatures temperatures
        {
            get
            {
                return _temperatures;
            }
            set
            {
                _temperatures = value;
                OnPropertyChanged();
            }
        }
        private string _description { get; set; }
        public string description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }
        private string _searchvalue { get; set; }
        public string searchvalue
        {
            get { return _searchvalue; }
            set
            {
                if (_searchvalue != value)
                {
                    _searchvalue = value;
                    OnPropertyChanged();
                }
            }
        }

        private List<prediction> _listplace { get; set; }
        public List<prediction> listplace
        {
            get { return _listplace; }
            set
            {
                _listplace = value;
                OnPropertyChanged();
            }
        }
        public async Task GetPlacesPredictionsAsync()
        {

            // TODO: Add throttle logic, Google begins denying requests if too many are made in a short amount of time

            CancellationToken cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(searchtextQuery, APIkey, WebUtility.UrlEncode(_searchvalue))))
            { //Be sure to UrlEncode the search term they enter

                using (HttpResponseMessage message = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false))
                {
                    if (message.IsSuccessStatusCode)
                    {
                        string json = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                        Temperatures predictionList = await Task.Run(() => JsonConvert.DeserializeObject<Temperatures>(json)).ConfigureAwait(false);

                        if (predictionList.status == "OK")
                        {

                            Addresses.Clear();

                            if (predictionList.predictions.Count > 0)
                            {
                                foreach (prediction prediction in predictionList.predictions)
                                {
                                    Addresses.Add(new AddressInfo { Address = prediction.description });
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(predictionList.status);
                        }
                    }
                }
            }
        }

        public async Task<String> GetAddressAsync(double latitude, double lng)
        {
            var address = "";
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(geocodinglocation, APIkey, latitude, lng)))
            { //Be sure to UrlEncode the search term they enter

                using (HttpResponseMessage message = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false))
                {
                    if (message.IsSuccessStatusCode)
                    {
                        string json = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                        GeocodingResult.ketqua listketqua = await Task.Run(() => JsonConvert.DeserializeObject<GeocodingResult.ketqua>(json)).ConfigureAwait(false);

                        if (listketqua.status == "OK")
                        {

                            if (listketqua.results.Count > 0)//trả về kq list result của mã hóa
                            {
                                address = listketqua.results[0].formatted_address;
                            }
                        }
                        else
                        {
                            return address = null;
                            Console.WriteLine(listketqua.status);
                        }
                    }
                }
            }
            return address;
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

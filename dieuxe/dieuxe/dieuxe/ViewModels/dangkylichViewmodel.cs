using dieuxe.Helpers;
using dieuxe.Models;
using dieuxe.Views;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using static dieuxe.Models.placesearchdetail;

namespace dieuxe.ViewModels
{
    public class dangkylichViewmodel : INotifyPropertyChanged
    {
        public xemlichdadangkyViemodel xl;
        private string _SdtLienLac { get; set; }
        private string _TenNguoiDi { get; set; }
        private DateTime _ThoiGianDen { get; set; }
        private DateTime _ThoiGianVe { get; set; }
        private string _NgayDen { get; set; }
        private TimeSpan _GioDen { get; set; }
        private string _NgayVe { get; set; }
        private TimeSpan _GioVe { get; set; }
        private string _NoiDi { get; set; }
        private string _NoiDen { get; set; }
        private string _Ghichu { get; set; }
        private string _ToaDoDi { get; set; }
        private string _ToaDoDen { get; set; }
        private int _SoNguoi { get; set; }
        public List<lich> lich1 { get; set; }
        public DangKyLichChiTiet Lich { get; set; }
       
        public ICommand dangkylichCommand
        {
            get
            {


                return new Command(async () =>
                {
                    NgayDen = ThoiGianDen.Date.ToString("yyyy'-'MM'-'dd");
                    var Gioden = GioDen;
                    NgayVe = ThoiGianVe.Date.ToString("yyyy'-'MM'-'dd");
                    if (String.IsNullOrEmpty(TenNguoiDi)){
                        await Application.Current.MainPage.DisplayAlert("Error", "Bạn chưa nhập tên người đi ", "OK");
                    }
                    else if (SoNguoi==0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Bạn chưa nhập số người đi ", "OK");
                    }
                    else if (String.IsNullOrEmpty(NoiDi)) { 
                         await Application.Current.MainPage.DisplayAlert("Error", "Bạn chưa chọn nơi đi ", "OK");
                    }
                    else if (String.IsNullOrEmpty(NoiDen))
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Bạn chưa chọn nơi đến ", "OK");
                    }
                    else if (String.IsNullOrEmpty(TenNguoiDi))
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Bạn chưa Nhập tên người đi ", "OK");
                    }
                    else if (String.IsNullOrEmpty(SdtLienLac))
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "bạn chưa nhập sdt :(", "OK");
                    }
                    else if (SdtLienLac.Length!=10)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "xin hãy nhập đúng sdt :(", "OK");
                    }
                    else {
                    //GioVe= ThoiGianVe.ToString("HH:mm:ss");
                    ToaDoDi = await mahoadiachi(NoiDi);
                    ToaDoDen = await mahoadiachi(NoiDen);
                    var keyValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("SdtLienLac",SdtLienLac),
                        new KeyValuePair<string, string>("TenNguoiDi",TenNguoiDi),
                        new KeyValuePair<string, string>("ThoiGianDen",ThoiGianDen.ToString()),
                        new KeyValuePair<string, string>("ThoiGianVe",ThoiGianVe.ToString()),
                        new KeyValuePair<string, string>("NgayDen",NgayDen),
                        new KeyValuePair<string, string>("GioDen",GioDen.ToString()),
                        new KeyValuePair<string, string>("NgayVe",NgayVe),
                        new KeyValuePair<string, string>("GioVe",GioVe.ToString()),
                        new KeyValuePair<string, string>("NoiDi",NoiDi),
                        new KeyValuePair<string, string>("NoiDen",NoiDen),
                        new KeyValuePair<string, string>("Ghichu",Ghichu),
                        new KeyValuePair<string, string>("ToaDoDi",ToaDoDi),
                        new KeyValuePair<string, string>("ToaDoDen",ToaDoDen),
                        new KeyValuePair<string,string>("SoNguoi",SoNguoi.ToString())
                    };
                    Debug.WriteLine("điểm đi:" + ToaDoDi + "Điểm đến:" + ToaDoDen);
                    var accessToken = Settings.AccessToken;
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://apidieuxe.azurewebsites.net/api/Dangkylich");
                    request.Content = new FormUrlEncodedContent(keyValues);
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Notify?", "đăng ký thành công", "OK");
                        // await Application.Current.MainPage.Navigation.PushModalAsync(new dangkythongtin());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "đăng ký thất bại :(", "Again");
                    }
                    }
                });
            }
        }
        public dangkylichViewmodel()
        {
            
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
        public string TenNguoiDi
        {
            get { return _TenNguoiDi; }
            set
            {
                _TenNguoiDi = value;
                OnPropertyChanged();
            }
        }
        public DateTime ThoiGianDen
        {
            get { return _ThoiGianDen; }
            set
            {
                _ThoiGianDen = value;
                OnPropertyChanged();
            }
        }
        public DateTime ThoiGianVe
        {
            get { return _ThoiGianVe; }
            set
            {
                _ThoiGianVe = value;
                OnPropertyChanged();
            }
        }
        public string NgayDen
        {
            get { return _NgayDen; }
            set
            {
                _NgayDen = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan GioDen
        {
            get { return _GioDen; }
            set
            {
                _GioDen = value;
                OnPropertyChanged();
            }
        }
        public string NgayVe
        {
            get { return _NgayVe; }
            set
            {
                _NgayVe = value;
                OnPropertyChanged();
            }
        }
        public TimeSpan GioVe
        {
            get { return _GioVe; }
            set
            {
                _GioVe = value;
                OnPropertyChanged();
            }
        }
        public string NoiDi
        {
            get { return _NoiDi; }
            set
            {
                _NoiDi = value;
                OnPropertyChanged();
            }
        }
        public string NoiDen
        {
            get { return _NoiDen; }
            set
            {
                _NoiDen = value;
                OnPropertyChanged();
            }
        }
        public string Ghichu
        {
            get { return _Ghichu; }
            set
            {
                _Ghichu = value;
                OnPropertyChanged();
            }
        }
        public string ToaDoDi
        {
            get { return _ToaDoDi; }
            set
            {
                _ToaDoDi = value;
                OnPropertyChanged();
            }
        }
        public string ToaDoDen
        {
            get { return _ToaDoDen; }
            set
            {
                _ToaDoDen = value;
                OnPropertyChanged();
            }
        }
        public int SoNguoi
        {
            get { return _SoNguoi
;
            }
            set
            {
                _SoNguoi = value;
                OnPropertyChanged();
            }
        }


        public ICommand SuaCommand// done
        {
            get
            {
                return new Command(async () =>
                {
                    var accessToken = Settings.AccessToken;
                    var client = new HttpClient();
                    Lich.NoiDi = NoiDi;
                    Lich.NoiDen = NoiDen;
                    Lich.SoNguoi = SoNguoi;
                    Lich.GioDen = GioDen.ToString();
                    Lich.GioVe = GioVe.ToString();
                    Lich.NgayDen =ThoiGianDen.Date.ToString("yyyy'-'MM'-'dd");
                    Lich.NgayVe = ThoiGianVe.Date.ToString("yyyy'-'MM'-'dd");
                    Lich.ToaDoDi = await mahoadiachi(NoiDi);
                    Lich.ToaDoDen = await mahoadiachi(NoiDen);
                    Lich.Ghichu = Ghichu;
                    Lich.SdtLienLac = SdtLienLac;
                    Lich.TenNguoiDi = TenNguoiDi;
                    Lich.ThoiGianDen = ThoiGianDen.ToString();
                    Lich.ThoiGianVe = ThoiGianVe.ToString();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var json = JsonConvert.SerializeObject(Lich);
                    HttpContent content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PutAsync("https://apidieuxe.azurewebsites.net/api/PutDangkylichhientai?id=" + Lich.DangKyLichChiTietId, content);
                    if (response.IsSuccessStatusCode)
                    {
                        xl = new xemlichdadangkyViemodel();
                        await xl.GetlichAsync(" https://apidieuxe.azurewebsites.net/api/getDangkyChitietUser", accessToken);
                       
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "cập nhật thành công !", "OK");
                        
                        await Application.Current.MainPage.Navigation.PopAsync();
                        //await Navigation.PopAsync();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "cập nhật thất bại, hãy kiểm tra lại kết nối :(", "Thử lại");
                    }
                });
            }
        }


        private static readonly string APIkey = "AIzaSyDdwwJvAyOQWpSPj8pUxnDH8Whe9X-BxhA";
        private readonly string geocodinglocation = "https://maps.googleapis.com/maps/api/geocode/json?key={0}&latlng={1},{2}";
        private readonly string searchtextQuery = "https://maps.googleapis.com/maps/api/place/autocomplete/json?key={0}&input={1}&components=country:vn";
        private static HttpClient _client;
        public static HttpClient client => _client ?? (_client = new HttpClient());
        public async Task<string> mahoadiachi(string address)
        {
            string toado = null;
            var locations = await Geocoding.GetLocationsAsync(address);
            var location = locations?.FirstOrDefault();
            if (location == null)
            {
                Debug.WriteLine("Error", "cannot find location", "OK");
                location = null;
            }

            Console.WriteLine(location.Latitude+","+location.Longitude);
            toado = location.Latitude + "," + location.Longitude;
            return toado;
        }
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
        private ObservableCollection<Addressdi> _addressesdi;
        public ObservableCollection<Addressdi> Addressesdi
        {
            get => _addressesdi ?? (_addressesdi = new ObservableCollection<Addressdi>());
            set
            {
                if (_addressesdi != value)
                {
                    _addressesdi = value;
                    OnPropertyChanged();
                }
            }
        }
        private ObservableCollection<Addressdden> _addressesdden;
        public ObservableCollection<Addressdden> Addressesdden
        {
            get => _addressesdden ?? (_addressesdden = new ObservableCollection<Addressdden>());
            set
            {
                if (_addressesdden != value)
                {
                    _addressesdden = value;
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
        public string tukhoatimkiem { get; set; }// từ khóa tìm kiếm
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
        public async Task GetPlacesPredictionsAsync(string type = null)
        {

            if (type == "noiden")
            {
                tukhoatimkiem = NoiDen;
            }
            else if (type == "noidi")
            { tukhoatimkiem = NoiDi; }
            else tukhoatimkiem = searchvalue;
            // TODO: Add throttle logic, Google begins denying requests if too many are made in a short amount of time

            CancellationToken cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(searchtextQuery, APIkey, WebUtility.UrlEncode(tukhoatimkiem))))
            { //Be sure to UrlEncode the search term they enter

                using (HttpResponseMessage message = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false))
                {
                    if (message.IsSuccessStatusCode)
                    {
                        string json = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                        Temperatures predictionList = await Task.Run(() => JsonConvert.DeserializeObject<Temperatures>(json)).ConfigureAwait(false);

                        if (predictionList.status == "OK")
                        {
                            if (type == "noiden")
                            {
                                Addressesdden.Clear();

                                if (predictionList.predictions.Count > 0)
                                {
                                    foreach (prediction prediction in predictionList.predictions)
                                    {
                                        Addressesdden.Add(new Addressdden { Address = prediction.description });
                                    }
                                }
                            }
                            else if (type == "noidi")
                            {
                                Addressesdi.Clear();

                                if (predictionList.predictions.Count > 0)
                                {
                                    foreach (prediction prediction in predictionList.predictions)
                                    {
                                        Addressesdi.Add(new Addressdi { Address = prediction.description });
                                    }
                                }
                            }
                            else
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

                        }
                        else
                        {
                            Addresses.Clear();
                            Addressesdden.Clear();
                            Addressesdi.Clear();
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
    }
}

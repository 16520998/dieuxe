using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;
using System.Net.Http;
using dieuxe.Models;
using dieuxe.Helpers;
using Polyline = Xamarin.Forms.Maps.Polyline;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Threading;
using System.Net.Http.Headers;

namespace dieuxe.ViewModels
{
    public class Point
    {
        public Coordinate location { get; set; }
        public string diachi { get; set; }
        public double distance { get; set; }
        public string thoigianxeden_luotdi { get; set; }
        public string thoigianxeden_luotve { get; set; }

        public List<point_detail> dsdiemcungtoado { get; set; }
    }
    public class point_detail
    {
        public int id { get; set; }
        public int songuoi { get; set; }
        public string thoigianden { get; set; }
        public string thoigianve { get; set; }
        public string[] khungthoigianden { get; set; }
        public string[] khungthoigianve { get; set; }
    }

    public class ChuyenDi
    {
        public List<Point> dsDiem { get; set; }
        public string DiaChiCuoiCung { get; set; }
        public int tongsonguoi { get; set; }
        public Xe xedi { get; set; }
        public List<int> listDangKyLichChiTietId { get; set; }
    }
    class Xe_v2
    {
        public Xe ThongTinXe { get; set; }
        public bool trangthai { get; set; }
        public int songuoihientai { get; set; }
    }
    public class QuanLyVM : INotifyPropertyChanged
    {
        List<point_detail> dsLuotDi;
        List<point_detail> DsLuotDi
        {
            get { return dsLuotDi; }
            set
            {
                dsLuotDi = value;
                OnPropertyChanged("DsLuotDi");
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

        List<DangKyLichChiTiet> lichdangky;
        public List<DangKyLichChiTiet> DanhSachDangKy
        {
            get { return lichdangky; }
            set
            {
                lichdangky = value;
                OnPropertyChanged("DanhSachDangKy");
            }
        }

        List<LienHeUser> dstaixe;
        public List<LienHeUser> DsTaiXe
        {
            get { return dstaixe; }
            set
            {
                dstaixe = value;
                OnPropertyChanged("DsTaiXe");
            }
        }

        ObservableCollection<ChuyenDi> DsChuyenDi;
        public ObservableCollection<ChuyenDi> DanhSachChuyenDi
        {
            get { return DsChuyenDi; }
            set
            {
                DsChuyenDi = value;
                OnPropertyChanged("DanhSachChuyenDi");
            }
        }
        List<Dieuxe> _DsDieuXe;
        public List<Dieuxe> DsDieuXe
        {
            get { return _DsDieuXe; }
            set
            {
                _DsDieuXe = value;
                OnPropertyChanged("DsDieuXe");
            }
        }

        int sokq;
        public int SoKq
        {
            get { return sokq; }
            set
            {
                sokq = value;
                OnPropertyChanged("SoKq");
            }
        }
        int soldk;
        public int SoLdk
        {
            get { return soldk; }
            set
            {
                soldk = value;
                OnPropertyChanged("SoLdk");
            }
        }
        public int ListviewKetQuaRowHeight { get; set; }
        int _ChieuCaoListviewKetQua;
        public int ChieuCaoListviewKetQua
        {
            get { return _ChieuCaoListviewKetQua; }
            set
            {
                _ChieuCaoListviewKetQua = value;
                OnPropertyChanged("ChieuCaoListviewKetQua");
            }
        }

        List<Xe> xes;
        public List<Xe> Xes
        {
            get { return xes; }
            set { xes = value; }
        }

        bool laydulieuxong;
        public bool LayDuLieuXong
        {
            get { return laydulieuxong; }
            set
            {
                laydulieuxong = value;
                OnPropertyChanged("LayDuLieuXong");
            }
        }
        bool koCoLichDK;
        public bool KoCoLichDK
        {
            get { return koCoLichDK; }
            set
            {
                koCoLichDK = value;
                OnPropertyChanged("KoCoLichDK");
            }
        }

        public ICommand phantichcum { get; private set; }
        public ICommand test { get; private set; }
        public ICommand PostCommand
        {
            get
            {
                return new Command<List<Dieuxe>>(async (postdieuxe) =>
                {
                    var accessToken = Settings.AccessToken;
                    var client = new HttpClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    var json = JsonConvert.SerializeObject(postdieuxe);
                    HttpContent content = new StringContent(json);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await client.PostAsync("https://apidieuxe.azurewebsites.net/api/PostListlichdieuxe/", content);
                    if (response.IsSuccessStatusCode)
                    {
                        await Application.Current.MainPage.DisplayAlert("Thông báo", "Đã xong!", "OK");
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Lỗi", "Đã xảy ra lỗi 😞", "Again");
                    }
                });
            }
        }

        //string stringWayPoints;
        //public string StringWayPoints
        //{
        //    get { return stringWayPoints; }
        //    set { stringWayPoints = value; }
        //}

        //Map map;

        public QuanLyVM()
        {
            dsLuotDi = new List<point_detail> { new point_detail { id = 1, songuoi = 2, thoigianden = "DS", thoigianve = "##" } };
            //this.map = map;
            MapFunction.Mapkey = "AIzaSyDdwwJvAyOQWpSPj8pUxnDH8Whe9X-BxhA";
            phantichcum = new Command(phantich);
            this.test = new Command(testfunction);

            DsChuyenDi = new ObservableCollection<ChuyenDi>();
            ListviewKetQuaRowHeight = 70;
            laydulieuxong = false;
            koCoLichDK = true;
            _DsDieuXe = new List<Dieuxe>();
            //lichdangky = new List<lich>() {
            //    new lich { sodangky = 0, ngaydangky = "25/03/2020", nhanviendangky = "SK10", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.845966, 106.765224", gioden = "08:00 28/03/2020", giove = "16:00 28/03/2020" },
            //    new lich { sodangky = 1, ngaydangky = "25/03/2020", nhanviendangky = "SK06", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.838085, 106.634272", gioden = "08:00 27/03/2020", giove = "15:00 27/03/2020" },
            //    new lich { sodangky = 2, ngaydangky = "18/03/2020", nhanviendangky = "SK06", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.769082, 106.702199", gioden = "08:00 20/03/2020", giove = "15:00 20/03/2020" },
            //    new lich { sodangky = 3, ngaydangky = "15/03/2020", nhanviendangky = "SK05", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.769082, 106.702199", gioden = "08:00 18/03/2020", giove = "16:00 18/03/2020" },
            //    new lich { sodangky = 4, ngaydangky = "15/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.832282, 106.778043", noiden = "10.868034, 106.780647", gioden = "08:00 19/03/2020", giove = "15:00 19/03/2020" },
            //    new lich { sodangky = 5, ngaydangky = "16/03/2020", nhanviendangky = "SK07", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.838781, 106.672548", gioden = "08:00 18/03/2020", giove = "17:00 18/03/2020" },
            //    new lich { sodangky = 6, ngaydangky = "18/03/2020", nhanviendangky = "SK06", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.769082, 106.702199", gioden = "08:00 20/03/2020", giove = "15:00 20/03/2020" },
            //    new lich { sodangky = 7, ngaydangky = "19/03/2020", nhanviendangky = "SK05", songuoi = 2, noidi = "10.832282, 106.778043", noiden = "10.835409, 106.635244", gioden = "08:00 23/03/2020", giove = "15:00 23/03/2020" },
            //    new lich { sodangky = 8, ngaydangky = "21/03/2020", nhanviendangky = "SK09", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.884010, 106.586954", gioden = "08:00 24/03/2020", giove = "16:00 24/03/2020" },
            //    new lich { sodangky = 9, ngaydangky = "24/03/2020", nhanviendangky = "SK07", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.787637, 106.686105", gioden = "08:00 27/03/2020", giove = "17:00 27/03/2020" },
            //    new lich { sodangky = 10, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.832282, 106.778043", noiden = "10.803820, 106.689993", gioden = "08:00 27/03/2020", giove = "16:00 27/03/2020" },
            //    new lich { sodangky = 11, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.803109, 106.737720", noiden = "10.842808, 106.615312", gioden = "08:00 27/03/2020", giove = "16:00 27/03/2020" },
            //    new lich { sodangky = 12, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.886754, 106.759661", noiden = "10.836570, 106.654286", gioden = "08:00 27/03/2020", giove = "16:00 27/03/2020" },
            //    new lich { sodangky = 13, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.774183, 106.722131", noiden = "10.813318, 106.578647", gioden = "08:00 27/03/2020", giove = "16:00 27/03/2020" },
            //    new lich { sodangky = 14, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.848119, 106.718481", noiden = "10.806229, 106.627437", gioden = "08:00 27/03/2020", giove = "16:00 27/03/2020" },
            //    new lich { sodangky = 15, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.795682, 106.675471", noiden = "10.832169, 106.621990", gioden = "08:00 27/03/2020", giove = "16:00 27/03/2020" }
            //};

            //lichdangky = new List<lich>() {
            //    new lich { sodangky = 0, ngaydangky = "25/03/2020", nhanviendangky = "SK10", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.845966, 106.765224", gioden = "08:00 20/03/2020", giove = "16:00 20/03/2020" },//
            //    new lich { sodangky = 1, ngaydangky = "25/03/2020", nhanviendangky = "SK06", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.838085, 106.634272", gioden = "08:00 20/03/2020", giove = "15:00 20/03/2020" },///
            //    new lich { sodangky = 2, ngaydangky = "18/03/2020", nhanviendangky = "SK06", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.769082, 106.702199", gioden = "08:00 20/03/2020", giove = "15:00 20/03/2020" },///
            //    new lich { sodangky = 3, ngaydangky = "15/03/2020", nhanviendangky = "SK05", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.769082, 106.702199", gioden = "08:00 20/03/2020", giove = "16:00 20/03/2020" },///
            //    new lich { sodangky = 4, ngaydangky = "15/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.832282, 106.778043", noiden = "10.868034, 106.780647", gioden = "09:00 20/03/2020", giove = "15:00 20/03/2020" },//
            //    new lich { sodangky = 5, ngaydangky = "16/03/2020", nhanviendangky = "SK07", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.838781, 106.672548", gioden = "09:00 20/03/2020", giove = "17:00 20/03/2020" },///
            //    new lich { sodangky = 6, ngaydangky = "18/03/2020", nhanviendangky = "SK06", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.769082, 106.702199", gioden = "08:00 20/03/2020", giove = "15:00 20/03/2020" },///
            //    new lich { sodangky = 7, ngaydangky = "19/03/2020", nhanviendangky = "SK05", songuoi = 2, noidi = "10.832282, 106.778043", noiden = "10.835409, 106.635244", gioden = "08:00 20/03/2020", giove = "15:00 20/03/2020" },///
            //    new lich { sodangky = 8, ngaydangky = "21/03/2020", nhanviendangky = "SK09", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.884010, 106.586954", gioden = "08:00 20/03/2020", giove = "16:00 20/03/2020" },
            //    new lich { sodangky = 9, ngaydangky = "24/03/2020", nhanviendangky = "SK07", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.787637, 106.686105", gioden = "08:00 20/03/2020", giove = "16:00 20/03/2020" },///
            //    new lich { sodangky = 10, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.832282, 106.778043", noiden = "10.803820, 106.689993", gioden = "09:00 20/03/2020", giove = "16:00 20/03/2020" },///
            //    new lich { sodangky = 11, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.803109, 106.737720", noiden = "10.842808, 106.615312", gioden = "08:00 20/03/2020", giove = "16:00 20/03/2020" },
            //    new lich { sodangky = 12, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.886754, 106.759661", noiden = "10.836570, 106.654286", gioden = "08:00 20/03/2020", giove = "16:00 20/03/2020" },
            //    new lich { sodangky = 13, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.774183, 106.722131", noiden = "10.813318, 106.578647", gioden = "08:00 20/03/2020", giove = "16:00 20/03/2020" },
            //    new lich { sodangky = 14, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.848119, 106.718481", noiden = "10.806229, 106.627437", gioden = "08:00 20/03/2020", giove = "16:00 20/03/2020" },
            //    new lich { sodangky = 15, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.795682, 106.675471", noiden = "10.832169, 106.621990", gioden = "08:00 20/03/2020", giove = "16:00 20/03/2020" }
            //};


            lichdangky = new List<DangKyLichChiTiet>() {
                new DangKyLichChiTiet {DangKyLichChiTietId= 1, DangKyLichId= 1,
                    TenNguoiDi= "Tưa",GioDen ="08:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.830361, 106.669945",ToaDoDen= "110.861636, 106.649946",SdtLienLac= "0123456789"},
                new DangKyLichChiTiet {DangKyLichChiTietId= 2, DangKyLichId= 2,
                    TenNguoiDi= "Quang,Nhân",GioDen ="08:00:00",GioVe="17:30:00",
                    SoNguoi= 2,
                    ToaDoDi= "10.804101, 106.635247",ToaDoDen= "10.861920, 106.649738",SdtLienLac= "0123456789"},
                new DangKyLichChiTiet {DangKyLichChiTietId= 3, DangKyLichId= 3,
                    TenNguoiDi= "Tưa1",GioDen ="08:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832282, 106.778043",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                 new DangKyLichChiTiet {DangKyLichChiTietId= 4, DangKyLichId= 4,
                    TenNguoiDi= "Tưa2",GioDen ="08:00:00",GioVe="15:20:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832282, 106.778043",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                  new DangKyLichChiTiet {DangKyLichChiTietId= 5, DangKyLichId= 5,
                    TenNguoiDi= "Tưa3",GioDen ="08:00:00",GioVe="17:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832282, 106.778043",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                   new DangKyLichChiTiet {DangKyLichChiTietId= 6, DangKyLichId= 6,
                    TenNguoiDi= "Tưa4",GioDen ="07:30:00",GioVe="16:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832282, 106.778043",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                    new DangKyLichChiTiet {DangKyLichChiTietId= 7, DangKyLichId= 7,
                    TenNguoiDi= "Tưa5",GioDen ="08:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832282, 106.778043",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                     new DangKyLichChiTiet {DangKyLichChiTietId= 8, DangKyLichId= 8,
                    TenNguoiDi= "Tưa6",GioDen ="08:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.806229, 106.627437",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                      new DangKyLichChiTiet {DangKyLichChiTietId= 9, DangKyLichId= 9,
                    TenNguoiDi= "Tưa7",GioDen ="09:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.803109, 106.737720",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},

                      new DangKyLichChiTiet {DangKyLichChiTietId= 10, DangKyLichId= 10,
                    TenNguoiDi= "Tưa8",GioDen ="07:30:00",GioVe="16:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832282, 106.778043",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                    new DangKyLichChiTiet {DangKyLichChiTietId= 11, DangKyLichId= 11,
                    TenNguoiDi= "Tưa9",GioDen ="08:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.829977, 106.804610",ToaDoDen= "10.873287, 106.765227",SdtLienLac= "0123456789"},
                     new DangKyLichChiTiet {DangKyLichChiTietId= 12, DangKyLichId= 12,
                    TenNguoiDi= "Tưa10",GioDen ="07:30:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.853905, 106.753322",ToaDoDen= "10.867609, 106.803870",SdtLienLac= "0123456789"},
                      new DangKyLichChiTiet {DangKyLichChiTietId= 13, DangKyLichId= 13,
                    TenNguoiDi= "Tưa11",GioDen ="09:00:00",GioVe="16:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.803109, 106.737720",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},


                    new DangKyLichChiTiet {DangKyLichChiTietId= 14, DangKyLichId= 14,
                    TenNguoiDi= "Tưa12",GioDen ="07:30:00",GioVe="16:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832961, 106.570370",ToaDoDen= "10.842575, 106.657216",SdtLienLac= "0123456789"},
                    new DangKyLichChiTiet {DangKyLichChiTietId= 15, DangKyLichId= 15,
                    TenNguoiDi= "Tưa13",GioDen ="10:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832961, 106.570370",ToaDoDen= "10.842575, 106.657216",SdtLienLac= "0123456789"},
                     new DangKyLichChiTiet {DangKyLichChiTietId= 16, DangKyLichId= 16,
                    TenNguoiDi= "Tưa14",GioDen ="07:30:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.853905, 106.753322",ToaDoDen= "10.867609, 106.803870",SdtLienLac= "0123456789"},
                      new DangKyLichChiTiet {DangKyLichChiTietId= 17, DangKyLichId= 17,
                    TenNguoiDi= "Tưa15",GioDen ="08:00:00",GioVe="16:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.813889, 106.633011",ToaDoDen= "10.883940, 106.586989",SdtLienLac= "0123456789"},


                    new DangKyLichChiTiet {DangKyLichChiTietId= 18, DangKyLichId= 18,
                    TenNguoiDi= "Tưa16",GioDen ="07:30:00",GioVe="16:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832282, 106.778043",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                    new DangKyLichChiTiet {DangKyLichChiTietId= 19, DangKyLichId= 19,
                    TenNguoiDi= "Tưa17",GioDen ="08:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.829977, 106.804610",ToaDoDen= "10.873287, 106.765227",SdtLienLac= "0123456789"},
                     new DangKyLichChiTiet {DangKyLichChiTietId= 20, DangKyLichId= 20,
                    TenNguoiDi= "Tưa18",GioDen ="07:30:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.853905, 106.753322",ToaDoDen= "10.867609, 106.803870",SdtLienLac= "0123456789"},
                      new DangKyLichChiTiet {DangKyLichChiTietId= 21, DangKyLichId= 21,
                    TenNguoiDi= "Tưa19",GioDen ="09:00:00",GioVe="16:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.803109, 106.737720",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},


                         new DangKyLichChiTiet {DangKyLichChiTietId= 45, DangKyLichId= 45,
                    TenNguoiDi= "Tưa20",GioDen ="07:30:00",GioVe="16:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832282, 106.778043",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                    new DangKyLichChiTiet {DangKyLichChiTietId= 22, DangKyLichId= 22,
                    TenNguoiDi= "Tưa22",GioDen ="08:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827305, 106.769399",ToaDoDen= "10.873287, 106.765227",SdtLienLac= "0123456789"},
                     new DangKyLichChiTiet {DangKyLichChiTietId= 23, DangKyLichId= 23,
                    TenNguoiDi= "Tưa23",GioDen ="07:30:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832872, 106.769190",ToaDoDen= "10.867609, 106.803870",SdtLienLac= "0123456789"},
                      new DangKyLichChiTiet {DangKyLichChiTietId= 24, DangKyLichId= 24,
                    TenNguoiDi= "Tưa24",GioDen ="09:00:00",GioVe="16:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827305, 106.769399",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},


                         new DangKyLichChiTiet {DangKyLichChiTietId= 25, DangKyLichId= 25,
                    TenNguoiDi= "Tưa25",GioDen ="07:30:00",GioVe="16:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827305, 106.769399",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                    new DangKyLichChiTiet {DangKyLichChiTietId= 26, DangKyLichId= 26,
                    TenNguoiDi= "Tưa26",GioDen ="08:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832872, 106.769190",ToaDoDen= "10.873287, 106.765227",SdtLienLac= "0123456789"},
                     new DangKyLichChiTiet {DangKyLichChiTietId= 27, DangKyLichId= 27,
                    TenNguoiDi= "Tưa27",GioDen ="07:30:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827305, 106.769399",ToaDoDen= "10.867609, 106.803870",SdtLienLac= "0123456789"},
                      new DangKyLichChiTiet {DangKyLichChiTietId= 28, DangKyLichId= 28,
                    TenNguoiDi= "Tưa28",GioDen ="09:00:00",GioVe="16:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827305, 106.769399",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},

                        new DangKyLichChiTiet {DangKyLichChiTietId=49, DangKyLichId= 49,
                    TenNguoiDi= "Tưa49",GioDen ="07:30:00",GioVe="16:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.842045, 106.747063",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                    new DangKyLichChiTiet {DangKyLichChiTietId= 50, DangKyLichId= 50,
                    TenNguoiDi= "Tưa50",GioDen ="08:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.815954, 106.764112",ToaDoDen= "10.873287, 106.765227",SdtLienLac= "0123456789"},
                     new DangKyLichChiTiet {DangKyLichChiTietId= 51, DangKyLichId= 51,
                    TenNguoiDi= "Tưa51",GioDen ="07:30:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827305, 106.769399",ToaDoDen= "10.791517, 106.801051",SdtLienLac= "0123456789"},
                      new DangKyLichChiTiet {DangKyLichChiTietId= 52, DangKyLichId= 52,
                    TenNguoiDi= "Tưa52",GioDen ="09:00:00",GioVe="16:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.831888, 106.776421",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},


                      //
                       new DangKyLichChiTiet {DangKyLichChiTietId= 29, DangKyLichId= 29,
                    TenNguoiDi= "Tưa29",GioDen ="07:30:00",GioVe="16:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.832282, 106.778043",ToaDoDen= "10.845966, 106.765224",SdtLienLac= "0123456789"},
                    new DangKyLichChiTiet {DangKyLichChiTietId= 30, DangKyLichId= 30,
                    TenNguoiDi= "Tưa30",GioDen ="08:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.829977, 106.804610",ToaDoDen= "10.873287, 106.765227",SdtLienLac= "0123456789"},
                     new DangKyLichChiTiet {DangKyLichChiTietId= 31, DangKyLichId= 31,
                    TenNguoiDi= "Tưa31",GioDen ="07:30:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.853905, 106.753322",ToaDoDen= "10.867609, 106.803870",SdtLienLac= "0123456789"},
                      new DangKyLichChiTiet {DangKyLichChiTietId= 32, DangKyLichId= 32,
                    TenNguoiDi= "Tưa32",GioDen ="09:00:00",GioVe="16:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.858501, 106.679623",SdtLienLac= "0123456789"},


                         new DangKyLichChiTiet {DangKyLichChiTietId= 34, DangKyLichId= 34,
                    TenNguoiDi= "Tưa33",GioDen ="07:30:00",GioVe="18:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.835322, 106.664054",SdtLienLac= "0123456789"},
                    new DangKyLichChiTiet {DangKyLichChiTietId= 33, DangKyLichId= 33,
                    TenNguoiDi= "Tưa34",GioDen ="08:00:00",GioVe="18:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.827238, 106.678829",SdtLienLac= "0123456789"},
                     new DangKyLichChiTiet {DangKyLichChiTietId= 35, DangKyLichId= 35,
                    TenNguoiDi= "Tưa35",GioDen ="07:30:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.800517, 106.667186",SdtLienLac= "0123456789"},
                      new DangKyLichChiTiet {DangKyLichChiTietId= 36, DangKyLichId= 36,
                    TenNguoiDi= "Tưa36",GioDen ="09:00:00",GioVe="18:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.841124, 106.809471",SdtLienLac= "0123456789"},


                         new DangKyLichChiTiet {DangKyLichChiTietId= 37, DangKyLichId= 37,
                    TenNguoiDi= "Tưa37",GioDen ="07:30:00",GioVe="16:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.839854, 106.796532",SdtLienLac= "0123456789"},
                    new DangKyLichChiTiet {DangKyLichChiTietId= 38, DangKyLichId= 38,
                    TenNguoiDi= "Tưa38",GioDen ="08:00:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.832175, 106.798271",SdtLienLac= "0123456789"},
                     new DangKyLichChiTiet {DangKyLichChiTietId= 39, DangKyLichId= 39,
                    TenNguoiDi= "Tưa39",GioDen ="07:30:00",GioVe="15:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.826282, 106.799551",SdtLienLac= "0123456789"},
                      new DangKyLichChiTiet {DangKyLichChiTietId= 40, DangKyLichId= 40,
                    TenNguoiDi= "Tưa40",GioDen ="09:00:00",GioVe="16:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.819116, 106.777467",SdtLienLac= "0123456789"},

                        new DangKyLichChiTiet {DangKyLichChiTietId= 41, DangKyLichId= 41,
                    TenNguoiDi= "Tưa41",GioDen ="07:30:00",GioVe="16:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.817672, 106.775825",SdtLienLac= "0123456789"},
                    new DangKyLichChiTiet {DangKyLichChiTietId= 42, DangKyLichId= 42,
                    TenNguoiDi= "Tưa42",GioDen ="08:00:00",GioVe="16:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.813815, 106.779162",SdtLienLac= "0123456789"},
                     new DangKyLichChiTiet {DangKyLichChiTietId= 43, DangKyLichId= 43,
                    TenNguoiDi= "Tưa43",GioDen ="07:30:00",GioVe="15:00:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.832378, 106.787292",SdtLienLac= "0123456789"},
                      new DangKyLichChiTiet {DangKyLichChiTietId= 44, DangKyLichId= 44,
                    TenNguoiDi= "Tưa44",GioDen ="09:00:00",GioVe="16:30:00",
                    SoNguoi= 1,
                    ToaDoDi= "10.827339, 106.769288",ToaDoDen= "10.871222, 106.825411",SdtLienLac= "0123456789"},
            };


            //xes = new List<Xe>()
            //{
            //    new Xe { bienkiemsoat = "xe1", sochongoi = 4, taixe = "SK10", vitri=""},
            //    new Xe { bienkiemsoat = "xe2", sochongoi = 4, taixe = "SK11", vitri=""},
            //    new Xe { bienkiemsoat = "xe3", sochongoi = 6, taixe = "SK12", vitri=""},
            //    new Xe { bienkiemsoat = "xe4", sochongoi = 6, taixe = "SK13", vitri=""},
            //    new Xe { bienkiemsoat = "xe5", sochongoi = 8, taixe = "SK14", vitri=""},
            //    new Xe { bienkiemsoat = "xe6", sochongoi = 6, taixe = "SK15", vitri=""},
            //    new Xe { bienkiemsoat = "xe7", sochongoi = 8, taixe = "SK16", vitri=""},
            //    new Xe { bienkiemsoat = "xe8", sochongoi = 14, taixe = "SK17", vitri=""},
            //    new Xe { bienkiemsoat = "xe9", sochongoi = 14, taixe = "SK18", vitri=""}
            //};
        }

        public async Task LayDuLieu(CancellationTokenSource cts)
        {
            await LayDsLich(cts.Token);

            await LayDsXe(cts.Token);

            await LayDsTaiXe(cts.Token);

            if (lichdangky != null && xes != null && dstaixe != null)
            {
                LayDuLieuXong = true;
                if (lichdangky.Count > 0)
                    KoCoLichDK = false;
                cts = null;
            }
        }

        async Task LayDsLich(CancellationToken ct)
        {
            string path = "https://apidieuxe.azurewebsites.net/api/DsLichNgayMai";
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync(path, ct);

            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                DanhSachDangKy = JsonConvert.DeserializeObject<List<DangKyLichChiTiet>>(content);
            }
            else
            {
                //await Application.Current.MainPage.DisplayAlert("error", "you should be login first :( ", "OK");
                lichdangky = null;
            }
        }
        async Task LayDsXe(CancellationToken ct)
        {
            string path = "https://apidieuxe.azurewebsites.net/api/getchitietxe";
            var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync(path, ct);

            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                xes = JsonConvert.DeserializeObject<List<Xe>>(content);
            }
            else
            {
                //await Application.Current.MainPage.DisplayAlert("error", "you should be login first :( ", "OK");
                xes = null;
            }
        }
        async Task LayDsTaiXe(CancellationToken ct)
        {
            string path = "https://apidieuxe.azurewebsites.net/api/getTaiXe";
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
            HttpResponseMessage response = await client.GetAsync(path, ct);

            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                DsTaiXe = JsonConvert.DeserializeObject<List<LienHeUser>>(content);
            }
            else
            {
                //await Application.Current.MainPage.DisplayAlert("error", "you should be login first :( ", "OK");
                dstaixe = null;
            }
        }

        private async void testfunction()
        {
            //string u = "https://maps.googleapis.com/maps/api/directions/json?origin=10.832282,106.778043&destination=10.83657,106.654286&waypoints=10.845966,106.765224|10.868034,106.780647|10.886754,106.759661&mode=driving&key=AIzaSyB5W0x1aKzeLeeX2KeFeDk09WnJ53xFQQg";
            //string v = await mapfunction.GetJsonAsync(u);
            //MapDirectionResponse m = JsonConvert.DeserializeObject<MapDirectionResponse>(v);
            //var listpoint1 = MapFunction.Decode("wkcaAmrkiS?tAdBETDDJ?rABHNLnBHdD@bBA~BCAQQgD@{@ByA`@{Bd@kBf@{Cj@aDFu@?q@?[");
            ////var listpoint = mapfunction.GetPolylinePoints(m);
            //var listpoint2 = MapFunction.Decode("sweaAkmjjSUVw@SqA_@a@QsCkAeCiAoDyBmCeBqBqAs@_@kB}@yBu@qC}@gIaCyEsAuN_EeLaDeAQq@AgAB}@Lc@J_C|@kBv@e@H]A_@Ca@L]TK\\?XHPVNTHXXzBzF|BpFlFpM|BtF|GxObAjCzCbHlA`Dd@fBTlBHvA@zAEhAK`BIvAH?@?{@hNSjCq@lKcAvOsA~Sa@zHMzFWfJe@vOs@|WQ~HMzEItDG|@GnCGpBMvDKdECfACBEHCVQvCErBCdCErCCjFDnHFrL@nG?`LP@?~DO@?vEFdHDzB@zDL?@p@CvCCzCSpK_@nSKxDMdMIfD?pCFt@Dh@PfAj@tBf@hAfCxFpNt[tBxEd@nA`E~IzB~EvJnThC`GvE|KpBzEh@tAFXRn@bAbCvBpFpF`OfIhUdApCnAjCnAlBtAfBbCtC|BvCJf@f@f@rD`EbAjAv@|@AND^JRRLTD\\CVONWBO?O?I?EJCbCqAlKsFxLyGzJuFvUsMtQaKbGgDh@a@zBiBjG{GnDcExDcErBuBpAu@|@_@pAa@fCk@rCi@dAYd\\{GhJoBFTzDy@v@vHd@xGZjEPvAn@dCf@`BTl@HPrDKpACzCGdA@vAEhACjBOvCQtBS@I");
            //var listpoint3 = MapFunction.Decode("ytbaAoavjS_@AIVN`AAVCF\\`@z@lAb@f@P^Lx@Rt@pCpDbCvC~EvF`@h@^ZbBbAh@p@lAbCx@fBXlC?v@SbAVfDcELu@DoAPiB^mBl@iBb@wBTkAPiAn@w@f@w@RgBNcEMe@Bi@Le@ToA`Au@p@a@h@{@~Ae@n@QIuHqDgCkAU?a@FgB^aDnAeCfAq@z@gDxF_AxA{@hAeDhByBdAmCgEoAsBo@cBk@oAg@g@cAAcADcBHm@MwKeF{DeBuBgARMj@e@P[La@sEsA}DcAcGiB[QcBuAo@a@[K@e@?wAF}@XqCKs@kAeDIu@w@Sg@]cAcB}CoGqCoFkC|@_CbAgBb@YFw@@qAJmBJELw@fCYhAm@fHKzBGh@CN_IuC_RsGyK{DcJyDcA]b@_Bz@iDfCkLbCwK`@gBOE\\gBZiCRoEJ{FLaLW?AN@pDOrIGbDKzBKtAYvBqB`J{BjK{EjSeEhRqHz[}@`EcAvDk@|BkCnLuDhPeAtG{BtSm@bG@pCHnAj@`D^|A\\|AR`AdDzNnCtMf@`Cb@~AhB~HrBhJj@hEb@pDNzBb@lGdAbIbAzDfBvEhBlEnHpRjCrFx@pB|@pB`@hBtB~FjD`JK\\?XHPVNTBx@UV[Vi@~@i@bBw@bAc@t@UjAOlA?nAJpHtBvYfI`MrDrEhBrAr@vG`EpChBfBx@lAf@rCfAhCv@zBTxWEbBBtAPhBl@dJbHNNNKe@_@aFuDuAgAqAo@aAWeAOaECqI@aGBcA@mBMk@GQCPSBCCBQRc@Iu@UeA_@sCkAwAm@uC_BuBsAoFkDaBw@iBw@eDeAaI_CqLgDiL_DmF_BeCk@s@G_CHaARaE`B{@Zi@@]Ea@Fc@VKRE^P^t@^`@x@pCdHtGtObAjClGdOnBpEzDfJzApDp@vBThARbCD~BMtCMlBJ?eBhXkAxQcCv`@e@tO_@~MgApb@a@~PSpGa@rNKRQ|CS~M@pH@zBDdKBhU?|@P@?~DO@A~D@l@LjK@zDL??|DCrAOvIQpK[zNKlKMrGCbBBtAJlAN|@b@fBxBbFjP`_@zBdFdBbErFxLhFjL`FzKpIdS`BdEXbAt@hBdFzMzKpZfBbF`A|B|ApCn@~@nDhE~BzCTVJf@bAdAbErEhApABNB`@HTPNTFZ?XKT[@I@U?SZM|HeEzJiFjFwChCuAzPsJfWsN~NmIpCwB~EiFxEsFjDuDxB}B`Ao@`Bw@hD_ArE}@vA_@zTqEzK_CtCm@FTzDy@p@~GN`B~@lMNbApAhE^~@pFMrCGdDCnDQvGe@NC@I");
            ////System.Console.WriteLine("kc: " + m.routes[0].legs[0].distance.value);
            //Polyline polyline1 = new Polyline
            //{
            //    StrokeWidth = 8,
            //    StrokeColor = Color.FromHex("#000001F"),
            //    //FillColor = Color.FromHex("#881BA1E2"),
            //    Geopath = { }
            //};
            //foreach (Coordinate point in listpoint1)
            //{
            //    polyline1.Geopath.Add(new Position(point.Latitude, point.Longitude));
            //}
            //map.MapElements.Add(polyline1);

            //Polyline polyline2 = new Polyline
            //{
            //    StrokeWidth = 10,
            //    StrokeColor = Color.FromHex("#1BA1E2"),
            //    //FillColor = Color.FromHex("#881BA1E2"),
            //    Geopath = { }
            //};
            //foreach (Coordinate point in listpoint2)
            //{
            //    polyline2.Geopath.Add(new Position(point.Latitude, point.Longitude));
            //}
            //map.MapElements.Add(polyline2);

            //Polyline polyline3 = new Polyline
            //{
            //    StrokeWidth = 9,
            //    StrokeColor = Color.FromHex("#00FF01"),
            //    //FillColor = Color.FromHex("#881BA1E2"),
            //    Geopath = { }
            //};
            //foreach (Coordinate point in listpoint3)
            //{
            //    polyline3.Geopath.Add(new Position(point.Latitude, point.Longitude));
            //}
            //map.MapElements.Add(polyline3);

        }
        private async void phantich()// bị lỗi khi lui lại thì phân tích sẽ thêm 1 list nữa (ex: từ 5 thành 10) 
        {
            if (!laydulieuxong)
                return;

            SoLdk = lichdangky.Count;

            const double eps = 5;
            const int minPts = 2;

            //data
            //List<lich> 

            ObservableCollection<ChuyenDi> _DsChuyenDi = new ObservableCollection<ChuyenDi>();
            int[] status = new int[lichdangky.Count];   //mặc định tất cả các lịch đăng ký chưa được xét

            List<Coordinate> Original_point = new List<Coordinate>();
            List<Coordinate> Destination_point = new List<Coordinate>();
            foreach (var dangky in lichdangky)
            {
                Coordinate point1 = new Coordinate();
                Coordinate point2 = new Coordinate();
                double[] toadonoidi = CoordinatesToArray(dangky.ToaDoDi);
                point1.Latitude = toadonoidi[0];
                point1.Longitude = toadonoidi[1];
                Original_point.Add(point1);

                double[] toadonoiden = CoordinatesToArray(dangky.ToaDoDen);
                point2.Latitude = toadonoiden[0];
                point2.Longitude = toadonoiden[1];
                Destination_point.Add(point2);

            }

            //lấy ma trận khoảng cách
            List<Coordinate> danhsachdiem = new List<Coordinate>(Original_point);
            danhsachdiem.AddRange(Destination_point);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            //List<List<double>> distancematrix = await mapfunction.GetDistanceMatrix(danhsachdiem, danhsachdiem);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("thời gian thực hiện: {0}", elapsedMs);
            //List<List<double>> durationmatrix = await mapfunction.GetDurationMatrix(danhsachdiem, danhsachdiem);
            List<Xe_v2> dsXe = new List<Xe_v2>();
            List<List<double>> distancematrix = new List<List<double>>()
            {
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,7053,8941,11643,12053,15267,3831,20137,13294,13294,6099,16029,13294,20219,31737,13857,12345,24802,18197,29673,23819,25374},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,7053,8941,11643,12053,15267,3831,20137,13294,13294,6099,16029,13294,20219,31737,13857,12345,24802,18197,29673,23819,25374},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,7053,8941,11643,12053,15267,3831,20137,13294,13294,6099,16029,13294,20219,31737,13857,12345,24802,18197,29673,23819,25374},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,7053,8941,11643,12053,15267,3831,20137,13294,13294,6099,16029,13294,20219,31737,13857,12345,24802,18197,29673,23819,25374},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,7053,8941,11643,12053,15267,3831,20137,13294,13294,6099,16029,13294,20219,31737,13857,12345,24802,18197,29673,23819,25374},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,7053,8941,11643,12053,15267,3831,20137,13294,13294,6099,16029,13294,20219,31737,13857,12345,24802,18197,29673,23819,25374},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,7053,8941,11643,12053,15267,3831,20137,13294,13294,6099,16029,13294,20219,31737,13857,12345,24802,18197,29673,23819,25374},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,7053,8941,11643,12053,15267,3831,20137,13294,13294,6099,16029,13294,20219,31737,13857,12345,24802,18197,29673,23819,25374},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,7053,8941,11643,12053,15267,3831,20137,13294,13294,6099,16029,13294,20219,31737,13857,12345,24802,18197,29673,23819,25374},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,7053,8941,11643,12053,15267,3831,20137,13294,13294,6099,16029,13294,20219,31737,13857,12345,24802,18197,29673,23819,25374},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,7053,8941,11643,12053,15267,3831,20137,13294,13294,6099,16029,13294,20219,31737,13857,12345,24802,18197,29673,23819,25374},
                new List<double>(){7859,7859,7859,7859,7859,7859,7859,7859,7859,7859,7859,0,14440,5091,8663,8715,8097,15201,7708,7708,11832,11093,7708,15283,26138,7305,5793,18250,13261,21669,17267,17764},
                new List<double>(){8968,8968,8968,8968,8968,8968,8968,8968,8968,8968,8968,13793,0,17383,9490,17607,6536,19445,18367,18367,4379,13856,18367,19526,25881,16906,14770,20038,17124,23817,23305,19518},
                new List<double>(){13113,13113,13113,13113,13113,13113,13113,13113,13113,13113,13113,6386,19694,0,10606,7371,13351,16308,2878,2878,17086,13677,2878,15705,24794,6130,7736,16906,15846,29323,15923,16420},
                new List<double>(){9486,9486,9486,9486,9486,9486,9486,9486,9486,9486,9486,11270,8351,12119,0,12320,7272,16232,13103,13103,8627,10644,13103,16314,22668,13232,9483,16826,13581,20605,20093,16306},
                new List<double>(){16201,16201,16201,16201,16201,16201,16201,16201,16201,16201,16201,11465,17985,8997,9932,0,14439,8938,4964,4964,17302,6528,4964,8334,17423,2199,2913,9535,8696,12954,8552,9049},
                new List<double>(){3831,3831,3831,3831,3831,3831,3831,3831,3831,3831,3831,7240,6508,11830,10199,15454,0,18282,13481,13481,4355,14174,13481,18364,27452,14044,12532,21609,16342,25388,21017,21089},
                new List<double>(){19539,19539,19539,19539,19539,19539,19539,19539,19539,19539,19539,17318,18723,16260,12817,8836,17324,0,13040,13040,18998,5416,13040,598,12677,10275,10071,4789,3732,8855,4596,4303},
                new List<double>(){13754,13754,13754,13754,13754,13754,13754,13754,13754,13754,13754,8077,18829,4285,10767,4493,13993,13430,0,0,17728,9809,0,12827,21916,3301,5525,14028,11977,17447,13045,13542},
                new List<double>(){13754,13754,13754,13754,13754,13754,13754,13754,13754,13754,13754,8077,18829,4285,10767,4493,13993,13430,0,0,17728,9809,0,12827,21916,3301,5525,14028,11977,17447,13045,13542},
                new List<double>(){5224,5224,5224,5224,5224,5224,5224,5224,5224,5224,5224,10049,5641,14638,12229,18262,3479,22183,16290,16290,0,16595,16290,22265,28619,16853,15341,22776,19862,26556,26044,22256},
                new List<double>(){15633,15633,15633,15633,15633,15633,15633,15633,15633,15633,15633,12891,14979,11833,8911,6874,13418,5207,10550,10550,15255,0,10550,5289,14885,7785,6409,9480,3267,14767,7942,8994},
                new List<double>(){13754,13754,13754,13754,13754,13754,13754,13754,13754,13754,13754,8077,18829,4285,10767,4493,13993,13430,0,0,17728,9809,0,12827,21916,3301,5525,14028,11977,17447,13045,13542},
                new List<double>(){19621,19621,19621,19621,19621,19621,19621,19621,19621,19621,19621,17400,18804,16469,12899,8233,17406,598,12437,12437,19080,5498,12437,0,12074,9671,10153,4185,3814,8251,3992,3699},
                new List<double>(){27524,27524,27524,27524,27524,27524,27524,27524,27524,27524,27524,25678,24597,23210,22260,14974,24555,9848,19177,19177,24873,13192,19177,9758,0,16412,16826,7466,11508,10016,10733,6946},
                new List<double>(){13613,13613,13613,13613,13613,13613,13613,13613,13613,13613,13613,8877,17288,6448,9226,1567,13852,10505,2765,2765,17587,6883,2765,9902,18991,0,3940,11102,9051,14521,10119,10617},
                new List<double>(){12561,12561,12561,12561,12561,12561,12561,12561,12561,12561,12561,7825,16238,6766,7812,2489,12800,10533,5466,5466,15555,6424,5466,10072,19161,3400,0,11273,8593,14691,10289,10787},
                new List<double>(){23176,23176,23176,23176,23176,23176,23176,23176,23176,23176,23176,21330,20249,18862,17912,10626,20207,5500,14829,14829,20525,8844,14829,5410,7888,12064,12478,0,7160,6891,6385,2598},
                new List<double>(){16864,16864,16864,16864,16864,16864,16864,16864,16864,16864,16864,14643,16739,13585,10142,7861,14649,4322,11537,11537,17014,2741,11537,4404,14000,8772,7396,8927,0,12346,7020,8441},
                new List<double>(){27671,27671,27671,27671,27671,27671,27671,27671,27671,27671,27671,22125,24744,19657,22407,11420,24701,9995,15624,15624,25020,13339,15624,8770,11883,12859,13272,7627,11655,0,5612,7107},
                new List<double>(){21249,21249,21249,21249,21249,21249,21249,21249,21249,21249,21249,16513,24916,14045,14678,5808,19185,5385,10012,10012,25192,8924,10012,4781,13870,7247,7660,5982,7239,6023,0,5496},
                new List<double>(){23469,23469,23469,23469,23469,23469,23469,23469,23469,23469,23469,18733,20612,16265,18275,8028,20569,3416,12232,12232,20887,9208,12232,2813,9566,9466,9880,1677,7524,7246,3787,0}
            };

            List<List<double>> durationmatrix = new List<List<double>>()
            {
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,902,1312,1404,1484,1701,625,2600,1661,1661,888,1986,1661,2585,3555,1589,1501,2871,2447,3399,2854,2847},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,902,1312,1404,1484,1701,625,2600,1661,1661,888,1986,1661,2585,3555,1589,1501,2871,2447,3399,2854,2847},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,902,1312,1404,1484,1701,625,2600,1661,1661,888,1986,1661,2585,3555,1589,1501,2871,2447,3399,2854,2847},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,902,1312,1404,1484,1701,625,2600,1661,1661,888,1986,1661,2585,3555,1589,1501,2871,2447,3399,2854,2847},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,902,1312,1404,1484,1701,625,2600,1661,1661,888,1986,1661,2585,3555,1589,1501,2871,2447,3399,2854,2847},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,902,1312,1404,1484,1701,625,2600,1661,1661,888,1986,1661,2585,3555,1589,1501,2871,2447,3399,2854,2847},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,902,1312,1404,1484,1701,625,2600,1661,1661,888,1986,1661,2585,3555,1589,1501,2871,2447,3399,2854,2847},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,902,1312,1404,1484,1701,625,2600,1661,1661,888,1986,1661,2585,3555,1589,1501,2871,2447,3399,2854,2847},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,902,1312,1404,1484,1701,625,2600,1661,1661,888,1986,1661,2585,3555,1589,1501,2871,2447,3399,2854,2847},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,902,1312,1404,1484,1701,625,2600,1661,1661,888,1986,1661,2585,3555,1589,1501,2871,2447,3399,2854,2847},
                new List<double>(){0,0,0,0,0,0,0,0,0,0,0,902,1312,1404,1484,1701,625,2600,1661,1661,888,1986,1661,2585,3555,1589,1501,2871,2447,3399,2854,2847},
                new List<double>(){1040,1040,1040,1040,1040,1040,1040,1040,1040,1040,1040,0,1810,666,991,963,977,2134,982,982,1376,1520,982,2119,2997,851,763,2133,1982,2721,2116,2116},
                new List<double>(){1354,1354,1354,1354,1354,1354,1354,1354,1354,1354,1354,1669,0,2067,1042,2033,1002,2015,2342,2342,553,1526,2342,2001,2586,2085,1825,1926,2168,2430,2384,1879},
                new List<double>(){1410,1410,1410,1410,1410,1410,1410,1410,1410,1410,1410,880,2180,0,1368,1150,1348,2396,333,333,1746,1876,333,2272,3184,1045,1139,2320,2338,2956,2302,2303},
                new List<double>(){1330,1330,1330,1330,1330,1330,1330,1330,1330,1330,1330,1348,937,1592,0,1527,982,1745,1867,1867,854,1255,1867,1730,2316,1658,1320,1656,1863,2160,2113,1608},
                new List<double>(){1808,1808,1808,1808,1808,1808,1808,1808,1808,1808,1808,1289,2066,1275,1226,0,1733,1247,928,928,1929,953,928,1123,2034,347,541,1170,1414,1757,1152,1153},
                new List<double>(){622,622,622,622,622,622,622,622,622,622,622,815,948,1318,1140,1614,0,2256,1574,1574,619,1642,1574,2241,2992,1502,1414,2332,2104,2836,2579,2284},
                new List<double>(){2610,2610,2610,2610,2610,2610,2610,2610,2610,2610,2610,2321,2025,2322,1754,1253,2262,0,2027,2027,1943,873,2027,129,1497,1445,1518,634,626,1324,716,616},
                new List<double>(){1621,1621,1621,1621,1621,1621,1621,1621,1621,1621,1621,1119,2306,509,1439,817,1558,2064,0,0,1956,1572,0,1940,2851,680,978,1987,2033,2574,1969,1970},
                new List<double>(){1621,1621,1621,1621,1621,1621,1621,1621,1621,1621,1621,1119,2306,509,1439,817,1558,2064,0,0,1956,1572,0,1940,2851,680,978,1987,2033,2574,1969,1970},
                new List<double>(){829,829,829,829,829,829,829,829,829,829,829,1143,712,1645,1306,1942,574,2280,1902,1902,0,1790,1902,2265,2851,1829,1742,2191,2432,2695,2648,2143},
                new List<double>(){2038,2038,2038,2038,2038,2038,2038,2038,2038,2038,2038,1758,1699,1759,1183,992,1691,788,1705,1705,1616,0,1705,774,1958,1123,983,1279,642,1848,1111,1261},
                new List<double>(){1621,1621,1621,1621,1621,1621,1621,1621,1621,1621,1621,1119,2306,509,1439,817,1558,2064,0,0,1956,1572,0,1940,2851,680,978,1987,2033,2574,1969,1970},
                new List<double>(){2586,2586,2586,2586,2586,2586,2586,2586,2586,2586,2586,2297,2001,2244,1730,1124,2238,123,1898,1898,1918,848,1898,0,1368,1316,1494,504,601,1195,587,487},
                new List<double>(){3193,3193,3193,3193,3193,3193,3193,3193,3193,3193,3193,2963,2479,2949,2249,1828,2711,1206,2602,2602,2397,1674,2602,1180,0,2020,2166,834,1427,1309,1291,787},
                new List<double>(){1487,1487,1487,1487,1487,1487,1487,1487,1487,1487,1487,968,2067,949,1200,263,1424,1510,582,582,1822,1018,582,1385,2297,0,693,1433,1479,2020,1415,1416},
                new List<double>(){1527,1527,1527,1527,1527,1527,1527,1527,1527,1527,1527,1008,1952,1009,1131,431,1464,1523,1090,1090,1815,909,1090,1411,2322,562,0,1459,1371,2046,1441,1441},
                new List<double>(){2728,2728,2728,2728,2728,2728,2728,2728,2728,2728,2728,2498,2015,2484,1785,1364,2246,741,2137,2137,1932,1210,2137,716,864,1556,1701,0,963,916,827,322},
                new List<double>(){2266,2266,2266,2266,2266,2266,2266,2266,2266,2266,2266,1977,1961,1978,1411,1184,1918,809,1897,1897,1878,529,1897,794,1977,1315,1174,1202,0,1787,1040,1185},
                new List<double>(){3262,3262,3262,3262,3262,3262,3262,3262,3262,3262,3262,2875,2549,2861,2319,1741,2780,1275,2514,2514,2466,1744,2514,1245,1540,1932,2078,944,1497,0,869,896},
                new List<double>(){2525,2525,2525,2525,2525,2525,2525,2525,2525,2525,2525,2006,2593,1992,1838,871,2345,832,1645,1645,2510,1266,1645,707,1619,1063,1209,755,1019,889,0,738},
                new List<double>(){2695,2695,2695,2695,2695,2695,2695,2695,2695,2695,2695,2176,2011,2162,1781,1042,2243,518,1815,1815,1929,1228,1815,394,1038,1233,1379,174,981,991,505,0},
            };
            foreach (var xe in Xes)
            {
                dsXe.Add(new Xe_v2 { ThongTinXe = xe, trangthai = false });
            }
            dsXe.Sort((x, y) => x.ThongTinXe.SoCho.CompareTo(y.ThongTinXe.SoCho));

            if (lichdangky.Count > 1)
            {
                DBSCAN Original = new DBSCAN(Original_point, eps, minPts);
                DBSCAN Destination = new DBSCAN(Destination_point, eps, minPts);

                List<List<int>> OrgCluster = Original.Clusters;
                List<int> OrgNoise = Original.Noise;

                List<List<int>> DesCluster = Destination.Clusters;
                List<int> DesNoise = Destination.Noise;

                //foreach (var item in OrgCluster)
                //{
                //    System.Console.Write("\nCụm đi: ");
                //    foreach (var p in item)
                //    {
                //        System.Console.Write(p + ", ");
                //    }
                //}
                //foreach (var item in DesCluster)
                //{
                //    System.Console.Write("\nCụm đến: ");
                //    foreach (var p in item)
                //    {
                //        System.Console.Write(p + ", ");
                //    }
                //}

                //foreach (int on in OrgNoise)      //here
                //{
                //    Pin pin = new Pin
                //    {
                //        Position = new Position(Original_point[on].Latitude, Original_point[on].Longitude),
                //        Label = "ID ORG noise: " + on
                //    };
                //    map.Pins.Add(pin);
                //}
                //foreach (int dn in DesNoise)
                //{
                //    Pin pin = new Pin
                //    {
                //        Position = new Position(Destination_point[dn].Latitude, Destination_point[dn].Longitude),
                //        Label = "ID DES noise: " + dn
                //    };
                //    map.Pins.Add(pin);
                //}

                //phân theo tọa độ
                List<List<Point>> dsChuyenDiTheoDiaDiem = new List<List<Point>>();

                for (int k = 0; k < OrgCluster.Count; k++)
                {
                    List<int> Ocluster = OrgCluster[k];
                    for (int l = 0; l < DesCluster.Count; l++)
                    {
                        List<int> Dcluster = DesCluster[l];
                        int countMatch = 0;
                        List<int> id = new List<int>();

                        foreach (int Opoint in Ocluster)
                        {
                            foreach (int Dpoint in Dcluster)
                            {
                                if (Opoint == Dpoint)
                                {
                                    id.Add(Opoint);
                                    countMatch++;
                                }
                            }
                        }

                        if (countMatch >= 2)
                        {
                            List<Point> chuyendi = new List<Point>();
                            Coordinate org = new Coordinate(Original_point[id[0]].Latitude, Original_point[id[0]].Longitude);

                            chuyendi.Add(new Point
                            {
                                location = org,
                                distance = 0.0,
                                dsdiemcungtoado = new List<point_detail>()
                                {
                                    new point_detail
                                    {
                                        id = id[0]
                                    }
                                }
                            });

                            foreach (int IdPoint in id)    //orgpoint
                            {
                                status[IdPoint] = 1;   //đánh dấu lịch đăng ký này là đã xét

                                Coordinate newlocation = new Coordinate(Original_point[IdPoint].Latitude, Original_point[IdPoint].Longitude);
                                bool isExist = false;
                                foreach (var point in chuyendi)
                                {
                                    if (newlocation.IsSame(point.location))
                                    {
                                        isExist = true;
                                        point.dsdiemcungtoado.Add(new point_detail { id = IdPoint });
                                        break;
                                    }
                                }
                                if (isExist)
                                    continue;
                                chuyendi.Add(new Point
                                {
                                    location = newlocation,
                                    distance = 0,
                                    dsdiemcungtoado = new List<point_detail>()
                                    {
                                        new point_detail
                                        {
                                            id = IdPoint
                                        }
                                    }
                                });
                            }

                            foreach (int IdPoint in id)    //despoint
                            {
                                Coordinate newlocation = new Coordinate(Destination_point[IdPoint].Latitude, Destination_point[IdPoint].Longitude);

                                // thiết lập khung thời gian cho các điểm Destination  
                                string[] khungthoigianden = laykhungthoigianden(lichdangky[IdPoint].GioDen, durationmatrix[IdPoint][getDestinationID(IdPoint)]);
                                string[] khungthoigianve = laykhungthoigianve(lichdangky[IdPoint].GioVe, durationmatrix[IdPoint][getDestinationID(IdPoint)]);

                                bool isExist = false;
                                foreach (var point in chuyendi)
                                {
                                    if (newlocation.IsSame(point.location))
                                    {
                                        isExist = true;
                                        point.dsdiemcungtoado.Add(new point_detail
                                        {
                                            id = getDestinationID(IdPoint),
                                            khungthoigianden = khungthoigianden,
                                            khungthoigianve = khungthoigianve
                                        });
                                        break;
                                    }
                                }
                                if (isExist)
                                    continue;
                                chuyendi.Add(new Point
                                {
                                    location = newlocation,
                                    distance = 0,
                                    dsdiemcungtoado = new List<point_detail>()
                                    {
                                        new point_detail
                                        {
                                            id = getDestinationID(IdPoint),
                                            khungthoigianden = khungthoigianden,
                                            khungthoigianve = khungthoigianve
                                        }
                                    }
                                });
                                //double dist = await mapfunction.GetDistanceAsync(org, newlocation);
                                //for (int i = 0; i < list.Count; i++)
                                //{
                                //    if (dist > list[i].distance)
                                //    {
                                //        if (i == list.Count - 1)
                                //        {
                                //            list.Add(new
                                //            {
                                //                location = newlocation,
                                //                distance = dist,
                                //                id = despoint + lichdangky.Count
                                //            });
                                //            break;
                                //        }
                                //        else
                                //        {
                                //            continue;
                                //        }
                                //    }
                                //    list.Add(list[list.Count - 1]);
                                //    for (int j = list.Count - 2; j > i; j--)
                                //    {
                                //        list[j] = list[j - 1];
                                //    }
                                //    list[i] = new
                                //    {
                                //        location = new Coordinate(Destination_point[despoint].Latitude, Destination_point[despoint].Longitude),
                                //        distance = dist,
                                //        id = despoint + lichdangky.Count
                                //    };
                                //    break;
                                //}
                            }

                            //sắp xép danh sách điểm để tìm điểm khởi hành
                            TimDiemKhoiHanh(chuyendi);

                            // Lấy khoảng cách từ điểm đầu tiên đến các điểm
                            List<double> distances = new List<double>();
                            foreach (var point in chuyendi)
                            {
                                distances.Add(distancematrix[chuyendi[0].dsdiemcungtoado[0].id][point.dsdiemcungtoado[0].id]);
                            }

                            //sx list theo khoảng cách
                            for (int i = 1; i < chuyendi.Count - 1; i++)
                            {
                                double mindistance = distances[i];
                                int index = i;
                                for (int j = i + 1; j < chuyendi.Count; j++)
                                {
                                    double distance = distances[j];
                                    if (distance < mindistance)
                                    {
                                        mindistance = distance;
                                        index = j;
                                    }
                                }
                                if (index != i)
                                {
                                    var temp = chuyendi[i];
                                    chuyendi[i] = chuyendi[index];
                                    chuyendi[index] = temp;

                                    distances[index] = distances[i];
                                    distances[i] = mindistance;
                                }
                            }

                            //System.Console.WriteLine("listcount: "+list.Count);
                            //for (int i = 0; i < list.Count; i++)
                            //{
                            //    System.Console.WriteLine("id point: {0}", list[i].id);
                            //    System.Console.WriteLine("kc: {0}", list[i].distance);
                            //}

                            //int stt = 0;
                            //foreach (var item in list)
                            //{
                            //    Pin pin = new Pin
                            //    {
                            //        Position = new Position(item.location.Latitude, item.location.Longitude),
                            //        Label = "stt: " + stt
                            //    };
                            //    map.Pins.Add(pin);
                            //    stt++;

                            //}
                            var watch1 = System.Diagnostics.Stopwatch.StartNew();
                            List<List<Point>> dschuyendicon = new List<List<Point>>() { chuyendi };
                            List<int> OriginIDList = new List<int>();
                            List<int> DestinationIDList = new List<int>();
                            for (int i = 0; i < chuyendi.Count; i++)
                            {
                                for (int j = 0; j < chuyendi[i].dsdiemcungtoado.Count; j++)
                                {
                                    var pointdetail = chuyendi[i].dsdiemcungtoado[j];
                                    if (pointIdIsDestination(pointdetail))
                                    {
                                        bool DaCoOriginID = false;
                                        foreach (var OrgId in OriginIDList)
                                        {
                                            if (getOriginID(pointdetail.id) == OrgId)
                                                DaCoOriginID = true;
                                        }
                                        if (!DaCoOriginID)
                                        {
                                            DestinationIDList.Add(pointdetail.id);
                                            //xóa khỏi chuyendi gốc
                                            dschuyendicon[0][i].dsdiemcungtoado.RemoveAt(j);

                                            //thêm vào list mới
                                            if (dschuyendicon.Count == 1)
                                            {
                                                dschuyendicon.Add(new List<Point>());
                                            }
                                            dschuyendicon[1].Add(new Point
                                            {
                                                location = chuyendi[i].location,
                                                dsdiemcungtoado = new List<point_detail>()
                                                    {
                                                        new point_detail
                                                        {
                                                            id = pointdetail.id,
                                                            khungthoigianden = pointdetail.khungthoigianden,
                                                            khungthoigianve = pointdetail.khungthoigianve
                                                        }
                                                    }
                                            });
                                        }
                                    }
                                    else
                                    {
                                        bool DaCoDestinationID = false;
                                        foreach (var DesID in DestinationIDList)
                                        {
                                            if (getDestinationID(pointdetail.id) == DesID)
                                            {
                                                DaCoDestinationID = true;
                                                //xóa khỏi chuyendi gốc
                                                dschuyendicon[0][i].dsdiemcungtoado.RemoveAt(j);

                                                //thêm vào list mới
                                                dschuyendicon[1].Add(new Point
                                                {
                                                    location = chuyendi[i].location,
                                                    dsdiemcungtoado = new List<point_detail>()
                                                    {
                                                        new point_detail
                                                        {
                                                            id = pointdetail.id
                                                        }
                                                    }
                                                });
                                                break;
                                            }
                                        }
                                        if (!DaCoDestinationID)
                                            OriginIDList.Add(pointdetail.id);
                                    }
                                }
                            }
                            for (int i = 0; i < dschuyendicon[0].Count; i++)
                            {
                                if (dschuyendicon[0][i].dsdiemcungtoado.Count == 0)
                                    dschuyendicon[0].RemoveAt(i);
                            }
                            watch1.Stop();
                            var elapsedMs1 = watch1.ElapsedMilliseconds;
                            Console.WriteLine("thời gian thực hiện sắp xếp: {0}", elapsedMs1);

                            //sắp xếp tiếp theo phương hướng
                            foreach (var chuyen in dschuyendicon)
                            {
                                sx(chuyen);

                                Coordinate origin = chuyen[0].location, destination_c1 = chuyen[chuyen.Count - 1].location, destination_c2 = chuyen[chuyen.Count - 2].location;

                                List<Coordinate> waypoints_c1 = new List<Coordinate>(), waypoints_c2 = new List<Coordinate>();
                                for (int i = 1; i < chuyen.Count - 1; i++)
                                {
                                    waypoints_c1.Add(chuyen[i].location);
                                }

                                for (int i = 1; i < chuyen.Count; i++)
                                {
                                    if (i == (chuyen.Count - 2))
                                        continue;
                                    waypoints_c2.Add(chuyen[i].location);
                                }

                                double dist1 = await MapFunction.GetDistanceAsync(origin, destination_c1, waypoints_c1), dist2 = await MapFunction.GetDistanceAsync(origin, destination_c2, waypoints_c2);

                                if (dist1 > dist2)
                                {
                                    int length = chuyen.Count;
                                    var temp = chuyen[length - 2];
                                    chuyen[length - 2] = chuyen[length - 1];
                                    chuyen[length - 1] = temp;
                                }

                                dsChuyenDiTheoDiaDiem.Add(chuyen);

                                //lấy thông tin chuyến đi sau cùng
                                //var property_list = get_ORG_DES_WAY(chuyen);
                                //Coordinate destination = property_list.destination;
                                //List<Coordinate> waypoints = property_list.waypoints;

                                //MapDirectionResponse mapDirectionResponse = await mapfunction.GetDirectionJsonAsync(origin, destination, waypoints);
                                //if (mapDirectionResponse.routes.Count > 0)
                                //{
                                //    var listpoint = mapfunction.GetPolylinePoints(mapDirectionResponse);
                                //    //List<Position> geopath = new List<Position>();
                                //    //foreach (Coordinate point in listpoint)
                                //    //{
                                //    //    geopath.Add(new Position(point.Latitude, point.Longitude));
                                //    //}
                                //    Polyline polyline = new Polyline
                                //    {
                                //        StrokeWidth = 8,
                                //        StrokeColor = Color.FromHex("#1BA1E2"),
                                //        //FillColor = Color.FromHex("#881BA1E2"),
                                //        Geopath = { }
                                //    };
                                //    foreach (Coordinate point in listpoint)
                                //    {
                                //        polyline.Geopath.Add(new Position(point.Latitude, point.Longitude));
                                //    }

                                //    map.MapElements.Add(polyline);
                                //    //Polyline dd = (Polyline)map.MapElements.ElementAt(1);
                                //    //double d = 0;
                                //    //foreach (Leg leg in mapDirectionResponse.routes[0].legs)
                                //    //{
                                //    //    d += leg.distance.value;
                                //    //}
                                //    //System.Console.WriteLine("tổng quãng đường: {0}", d);
                                //}
                            }
                        }
                    }
                }

                Console.WriteLine("số kq trước: {0}", _DsChuyenDi.Count);

                //phân theo thời gian
                var watch2 = System.Diagnostics.Stopwatch.StartNew();

                Parallel.ForEach(dsChuyenDiTheoDiaDiem, locthoigian);
                //locthoigian(dsChuyenDiTheoDiaDiem[1]);

                watch2.Stop();
                var elapsedMs2 = watch2.Elapsed;
                Console.WriteLine("thời gian phân cụm thời gian: {0}", elapsedMs2);


                //for (int i = 0; i < lichdangky.Count; i++)
                //{
                //    Console.WriteLine("lịch đk {0}", i);
                //    var d = laykhungthoigianden(lichdangky[i].gioden, durationmatrix[i][i + lichdangky.Count]);
                //    var v = laykhungthoigianve(lichdangky[i].giove, durationmatrix[i][i + lichdangky.Count]);
                //    Console.WriteLine("khung tg đến [{0}, {1}], khung tg về [{2}, {3}]", d[0], d[1], v[0], v[1]);
                //}

                //thêm các chuyến còn lại
                for (int id = 0; id < lichdangky.Count; id++)
                {
                    if (status[id] != 0)  // đã xét
                        continue;

                    themchuyendivaodanhsach(_DsChuyenDi, getDestinationID(id));
                }

                SoKq = _DsChuyenDi.Count;
                ChieuCaoListviewKetQua = sokq * ListviewKetQuaRowHeight;


                string ngaydicongtac = lichdangky.FirstOrDefault().NgayDen;
                for (int i = 0; i < _DsChuyenDi.Count; i++)
                {
                    var chuyen = _DsChuyenDi[i];
                    chuyen.DiaChiCuoiCung = chuyen.dsDiem.Last().diachi;
                    List<Coordinate> listPoint = new List<Coordinate>();

                    foreach (var point in chuyen.dsDiem)
                    {
                        listPoint.Add(point.location);
                    }

                    var property_list = QuanLyVM.get_ORG_DES_WAY(chuyen.dsDiem);
                    Coordinate origin = property_list.origin;
                    Coordinate destination = property_list.destination;
                    List<Coordinate> waypoints = property_list.waypoints;

                    string stringdsdiemdung = MapFunction.Encode(listPoint);
                    var waypoints2 = new List<Coordinate>(waypoints);
                    waypoints2.Reverse();
                    string stringtuyenduongdi = await MapFunction.GetPolyline(origin, destination, waypoints);
                    string stringtuyenduongve = await MapFunction.GetPolyline(destination, origin, waypoints2);
                    _DsDieuXe.Add(new Dieuxe
                    {
                        NoiDi = chuyen.dsDiem[0].diachi,
                        NoiDen = chuyen.DiaChiCuoiCung,
                        GioDi = chuyen.dsDiem[0].thoigianxeden_luotdi,
                        GioVe = chuyen.dsDiem[0].thoigianxeden_luotve,
                        TrangThai = 0,
                        TuyenDuongDi = stringtuyenduongdi,
                        TuyenDuongVe = stringtuyenduongve,
                        BienKiemSoat = chuyen.xedi.BienSoXe,
                        NgayDi = ngaydicongtac,
                        SoNguoi = chuyen.tongsonguoi,
                        DsDiemDung = stringdsdiemdung,
                        listDangKyLichChiTietId = chuyen.listDangKyLichChiTietId,
                        dsTaixe = new List<LienHeUser>(dstaixe),
                        IndexDieuXe = i + 1
                    });
                }
                DsDieuXe = new List<Dieuxe>(_DsDieuXe);

                foreach (var d in _DsChuyenDi)
                {
                    DsChuyenDi.Add(d);
                }


                //DsChuyenDi.Remove(DsChuyenDi.Last());

                Console.WriteLine("số kq sau: {0}", _DsChuyenDi.Count);


                //phần này của Views, ViewModel ko có phận sự
                //int stt = 0;
                //string[] color = new string[] { "#1BA1E2", "#4287F5", "#ED2F1a", "#EBF20F", "#16823E", "#022326", "#AB4849", "#414f03", "#C8EDB4" };
                //foreach (var chuyen in DsChuyenDi)
                //{
                //    stt++;
                //    var property_list = get_ORG_DES_WAY(chuyen.dsDiem);
                //    Coordinate origin = property_list.origin;
                //    Coordinate destination = property_list.destination;
                //    List<Coordinate> waypoints = property_list.waypoints;

                //    Pin pin = new Pin
                //    {
                //        Position = new Position(origin.Latitude, origin.Longitude),
                //        Label = "org chuyến đi: " + stt
                //    };
                //    Pin pin1 = new Pin
                //    {
                //        Position = new Position(destination.Latitude, destination.Longitude),
                //        Label = "destination chuyến đi: " + stt
                //    };
                //    map.Pins.Add(pin);
                //    map.Pins.Add(pin1);

                //    MapDirectionResponse mapDirectionResponse = await mapfunction.GetDirectionJsonAsync(origin, destination, waypoints);
                //    if (mapDirectionResponse.routes.Count > 0)
                //    {
                //        var listpoint = mapfunction.GetPolylinePoints(mapDirectionResponse);
                //        //List<Position> geopath = new List<Position>();
                //        //foreach (Coordinate point in listpoint)
                //        //{
                //        //    geopath.Add(new Position(point.Latitude, point.Longitude));
                //        //}
                //        Polyline polyline = new Polyline
                //        {
                //            StrokeWidth = 8,
                //            StrokeColor = Color.FromHex(color[stt]),
                //            //FillColor = Color.FromHex("#881BA1E2"),
                //            Geopath = { }
                //        };
                //        foreach (Coordinate point in listpoint)
                //        {
                //            polyline.Geopath.Add(new Position(point.Latitude, point.Longitude));
                //        }

                //        map.MapElements.Add(polyline);
                //        //Polyline dd = (Polyline)map.MapElements.ElementAt(1);
                //        //double d = 0;
                //        //foreach (Leg leg in mapDirectionResponse.routes[0].legs)
                //        //{
                //        //    d += leg.distance.value;
                //        //}
                //        //System.Console.WriteLine("tổng quãng đường: {0}", d);
                //    }
                //}

                ////lấy thông tin chuyến đi sau cùng
                //var property_list = get_ORG_DES_WAY(list);
                //Coordinate destination = property_list.destination;
                //List<Coordinate> waypoints = property_list.waypoints;

                //MapDirectionResponse mapDirectionResponse = await mapfunction.GetDirectionJsonAsync(origin, destination, waypoints);
                //if (mapDirectionResponse.routes.Count > 0)
                //{
                //    var listpoint = mapfunction.GetPolylinePoints(mapDirectionResponse);
                //    //List<Position> geopath = new List<Position>();
                //    //foreach (Coordinate point in listpoint)
                //    //{
                //    //    geopath.Add(new Position(point.Latitude, point.Longitude));
                //    //}
                //    Polyline polyline = new Polyline
                //    {
                //        StrokeWidth = 8,
                //        StrokeColor = Color.FromHex("#1BA1E2"),
                //        //FillColor = Color.FromHex("#881BA1E2"),
                //        Geopath = { }
                //    };
                //    foreach (Coordinate point in listpoint)
                //    {
                //        polyline.Geopath.Add(new Position(point.Latitude, point.Longitude));
                //    }

                //    map.MapElements.Add(polyline);
                //    double d = 0;
                //    foreach (Leg leg in mapDirectionResponse.routes[0].legs)
                //    {
                //        d += leg.distance.value;
                //    }
                //    System.Console.WriteLine("tổng quãng đường: {0}", d);
                //}


                System.Console.WriteLine("tổng số request: " + MapFunction.CountRequest);
            }

            void TimDiemKhoiHanh(List<Point> DsDiem)
            {
                int datacount = lichdangky.Count;
                for (int i = 0; i < DsDiem.Count; i++)
                {
                    if (pointIdIsDestination(DsDiem[i + 1].dsdiemcungtoado[0]))
                        break;
                    //giả sử các điểm tại vị trí i = 1, 2, 3 là p1->p2->p3
                    Coordinate p1 = new Coordinate(DsDiem[i].location.Latitude, DsDiem[i].location.Longitude);
                    Coordinate p2 = new Coordinate(DsDiem[i + 1].location.Latitude, DsDiem[i + 1].location.Longitude);
                    Coordinate p3 = new Coordinate(DsDiem[i + 2].location.Latitude, DsDiem[i + 2].location.Longitude);

                    Vector vector21 = MapFunction.layVector(p2, p1), vector23 = MapFunction.layVector(p2, p3), vector12 = MapFunction.layVector(p1, p2), vector13 = MapFunction.layVector(p1, p3);
                    double goc2 = MapFunction.timgoc(vector21, vector23), goc1 = MapFunction.timgoc(vector12, vector13);
                    double khoangcachP1P2 = distancematrix[DsDiem[i].dsdiemcungtoado[0].id][DsDiem[i + 1].dsdiemcungtoado[0].id];
                    double khoangcachP1P3 = distancematrix[DsDiem[i].dsdiemcungtoado[0].id][DsDiem[i + 2].dsdiemcungtoado[0].id];
                    if (goc1 > goc2 && khoangcachP1P2 < khoangcachP1P3)
                    {
                        DsDiem.Reverse(0, i + 1);
                    }
                }
            }

            void sx(List<Point> chuyendi)
            {
                System.Console.WriteLine("hàm sắp xếp");
                int sodiem = chuyendi.Count;
                int datacount = lichdangky.Count;
                for (int i = 0; i <= sodiem - 3; i++)
                {
                    //giả sử các điểm tại vị trí i = 1, 2, 3 là p1->p2->p3
                    Coordinate p1 = new Coordinate(chuyendi[i].location.Latitude, chuyendi[i].location.Longitude);
                    Coordinate p2 = new Coordinate(chuyendi[i + 1].location.Latitude, chuyendi[i + 1].location.Longitude);
                    Coordinate p3 = new Coordinate(chuyendi[i + 2].location.Latitude, chuyendi[i + 2].location.Longitude);
                    if (pointIdIsOrigrin(chuyendi[i + 1].dsdiemcungtoado[0]))
                        continue;
                    if (!thutu3diem(p1, p2, p3))
                    {   //nếu góc tại điểm p3 lớn hơn góc tại điểm p2 thì p3 nằm giữa p1 vs p2
                        if (i == sodiem - 3)
                        {
                            var temp = chuyendi[i + 1];
                            chuyendi[i + 1] = chuyendi[i + 2];
                            chuyendi[i + 2] = temp;
                            break;
                        }
                        //có 2 trường hợp: 1->3->2 hoặc 2->3->1
                        Coordinate p4 = new Coordinate(chuyendi[i + 3].location.Latitude, chuyendi[i + 3].location.Longitude);
                        //TH 1->3->2, xét p3, p2, p4
                        Vector vector23 = MapFunction.layVector(p2, p3), vector24 = MapFunction.layVector(p2, p4);
                        double anpha = MapFunction.timgoc(vector23, vector24);
                        //TH 2->3->1, xét p3, p1, p4
                        Vector vector13 = MapFunction.layVector(p1, p3), vector14 = MapFunction.layVector(p1, p4);
                        double beta = MapFunction.timgoc(vector13, vector14);
                        if (anpha >= beta)
                        {//chọn TH1
                            var temp = chuyendi[i + 1];
                            chuyendi[i + 1] = chuyendi[i + 2];
                            chuyendi[i + 2] = temp;
                        }
                        else
                        {  //TH2
                            var temp = chuyendi[i];
                            chuyendi[i] = chuyendi[i + 1];
                            chuyendi[i + 1] = chuyendi[i + 2];
                            chuyendi[i + 2] = temp;
                        }
                    }
                }
            }

            bool thutu3diem(Coordinate p1, Coordinate p2, Coordinate p3)
            {
                Vector vector21 = MapFunction.layVector(p2, p1), vector23 = MapFunction.layVector(p2, p3);
                double anpha = MapFunction.timgoc(vector21, vector23); //góc tại điểm p2
                Vector vector31 = MapFunction.layVector(p3, p1), vector32 = MapFunction.layVector(p3, p2);
                double beta = MapFunction.timgoc(vector31, vector32); //góc tại điểm p3
                if (beta > anpha)
                    return false;
                else
                    return true;
            }



            int TimGocLonNhat(Coordinate p1, Coordinate p2, Coordinate p3)
            {
                double goc1 = MapFunction.timgoc(MapFunction.layVector(p1, p2), MapFunction.layVector(p1, p3));
                double goc2 = MapFunction.timgoc(MapFunction.layVector(p2, p1), MapFunction.layVector(p2, p3));
                double goc3 = MapFunction.timgoc(MapFunction.layVector(p3, p1), MapFunction.layVector(p3, p2));
                return goc1 >= goc2 ? (goc1 >= goc3 ? 1 : 3) : (goc2 >= goc3 ? 2 : 3);
            }

            double[] CoordinatesToArray(string coor)
            {
                string[] result = { "", "" };
                char[] charArray = coor.ToCharArray();
                var k = 0;
                for (var i = 0; i < charArray.Length; i++)
                {
                    if (charArray[i] != ',' && charArray[i] != ' ')
                        result[k] += charArray[i];
                    else
                        k = 1;
                }
                return new double[] { Convert.ToDouble(result[0]), Convert.ToDouble(result[1]) };
            }

            async Task addPointToList(int idPoint, Coordinate point, List<Point> chuyendi)
            {
                System.Console.WriteLine("add function");
                Coordinate orgpoint = chuyendi[0].location;
                double dist = await MapFunction.GetDistanceAsync(orgpoint, point);
                for (int i = 1; i < chuyendi.Count; i++)
                {
                    if (dist > chuyendi[i].distance)
                        if (i == chuyendi.Count - 1)
                        {
                            chuyendi.Add(new Point
                            {
                                location = point,
                                distance = dist,
                                //id = idPoint
                            });
                            break;
                        }
                        else
                            continue;

                    chuyendi.Add(chuyendi[chuyendi.Count - 1]);
                    for (int j = chuyendi.Count - 2; j > i; j--)
                    {
                        chuyendi[j] = chuyendi[j - 1];
                    }
                    chuyendi[i] = new Point
                    {
                        location = point,
                        distance = dist,
                        //id = idPoint
                    };
                    break;
                }
            }

            //thời gian
            void locthoigian(List<Point> chuyendibandau)
            {
                ObservableCollection<ChuyenDi> dschuyendi = new ObservableCollection<ChuyenDi>();
                if (chuyendibandau.Count < 2)
                    return;

                foreach (Point point in chuyendibandau)
                {
                    if (pointContaintAllOrigrin(point))
                        continue;
                    foreach (var point_Detail in point.dsdiemcungtoado)
                    {
                        if (pointIdIsOrigrin(point_Detail))
                            continue;
                        themchuyendivaodanhsach(dschuyendi, point_Detail.id, chuyendibandau);

                        //for (int i = 0; i < dschuyendi.Count; i++)
                        //{
                        //    Console.WriteLine("chuyến đi {0}, số điểm: {1}", i, dschuyendi[i].dsDiem.Count);
                        //    string giờ = "";
                        //    for (int j = 0; j < dschuyendi[i].dsDiem.Count; j++)
                        //    {
                        //        string points = "";
                        //        giờ += "\nđiểm " + j + ": giờ đến đi " + dschuyendi[i].dsDiem[j].thoigianxeden_luotdi + " giờ đến về " + dschuyendi[i].dsDiem[j].thoigianxeden_luotve;
                        //        foreach (var item in dschuyendi[i].dsDiem[j].dsdiemcungtoado)
                        //        {
                        //            points += "id: "+item.id+", " ;
                        //        }
                        //        Console.WriteLine(giờ);
                        //        Console.WriteLine(points);

                        //    }
                        //}
                    }
                }
                //for (int i = 0; i < dschuyendi.Count; i++)
                //{
                //    Console.WriteLine("chuyến đi {0}, số điểm: {1}", i, dschuyendi[i].dsDiem.Count);
                //    for (int j = 0; j < dschuyendi[i].dsDiem.Count; j++)
                //    {
                //        Console.WriteLine("\nđiểm " + j + ": giờ đến về " + dschuyendi[i].dsDiem[j].thoigianxeden_luotve);
                //        foreach (var item in dschuyendi[i].dsDiem[j].dsdiemcungtoado)
                //        {
                //            Console.WriteLine("id: " + item.id + ", ");
                //        }
                //    }
                //}
                foreach (var item in dschuyendi)
                    _DsChuyenDi.Add(item);

                //int listcount = list.Count;
                //for (int i = 0; i < listcount; i++)
                //{
                //    Point point = list[i];

                //    if (point.id >= listcount)
                //        break;

                //    if(i != 0)
                //    {
                //        chuyendi1.Add(point);
                //        chuyendi1[chuyendi1.Count - 1].DsDiemTaiDay.Clear();

                //        double quangduongrieng = distancematrix[point.id][point.id + listcount];
                //        int thoigianlechtoida = ThoiGianLechToiDa(quangduongrieng);

                //    }

                //    //thời gian chờ tối đa của một người = 7 * căn bậc 2 (quãng đường)
                //    //double quangduongrieng = point.id < listcount ? distancematrix[point.id][point.id + listcount] : distancematrix[point.id - listcount][point.id];
                //    //int thoigianchotoida = (int)Math.Round(7 * Math.Sqrt(quangduongrieng));

                //}

            }

            void themchuyendivaodanhsach(ObservableCollection<ChuyenDi> dschuyendi, int DestinationID, List<Point> chuyendibandau = null)    //chuyendibandau = null -> chuyến đi đang thêm chưa được gom cụm
            {
                if (DestinationID < lichdangky.Count)
                    throw new ArgumentException("Giá trị đối số không hợp lệ", "DestinationID");

                bool themchuyendithanhcong = false;

                int OriginID = getOriginID(DestinationID);
                int DangKyLichChiTietId = lichdangky[OriginID].DangKyLichChiTietId;
                Coordinate OrgLocation = Original_point[OriginID];
                Coordinate DesLocation = Destination_point[OriginID];
                int songuoidi = lichdangky[OriginID].SoNguoi;
                var d = lichdangky[OriginID].GioDen;
                Console.WriteLine("\nid dk: {0}", getOriginID(DestinationID));
                string[] khungthoigianden = laykhungthoigianden(lichdangky[OriginID].GioDen, durationmatrix[OriginID][DestinationID]);
                string[] khungthoigianve = laykhungthoigianve(lichdangky[OriginID].GioVe, durationmatrix[OriginID][DestinationID]);    //fff

                Point orgPoint = new Point();
                orgPoint.location = OrgLocation; //
                orgPoint.diachi = lichdangky[OriginID].NoiDi;
                orgPoint.dsdiemcungtoado = new List<point_detail>()
                {
                    new point_detail{ id = OriginID, songuoi = songuoidi }
                };

                Point desPoint = new Point();
                desPoint.location = DesLocation;
                desPoint.diachi = lichdangky[OriginID].NoiDen;
                desPoint.thoigianxeden_luotdi = khungthoigianden[1];
                desPoint.thoigianxeden_luotve = khungthoigianve[0];
                desPoint.dsdiemcungtoado = new List<point_detail>()
                {
                    new point_detail
                    {
                        id = DestinationID,
                        songuoi = songuoidi,
                        thoigianden = lichdangky[OriginID].GioDen,
                        thoigianve = lichdangky[OriginID].GioVe,
                        khungthoigianden = khungthoigianden,
                        khungthoigianve = khungthoigianve
                    }
                };

                var ChuyenDiThoaDk = new { vitrichuyendi = 0, xedi = new Xe_v2(), chuyendimoi = new ChuyenDi(), thoigianlangphi = 0.0 };    //vị trí của chuyendi, đoạn đường dài thêm, thời gian dư thừa thêm
                var dsChuyenDiThoaDk = new[] { ChuyenDiThoaDk }.ToList();
                dsChuyenDiThoaDk.Clear();

                for (int index = 0; index < dschuyendi.Count; index++)    ////xét từng chuyến đi
                {
                    ChuyenDi chuyendi = dschuyendi[index];
                    Xe_v2 xehientai = null;
                    foreach (var xe in dsXe)
                    {
                        if (xe.ThongTinXe.Equals(chuyendi.xedi))
                        {
                            xehientai = xe;
                            break;
                        }
                    }
                    if (xehientai == null)  /*  || xehientai.ThongTinXe.sochongoi < xehientai.songuoihientai + songuoidi    */
                    {
                        Console.WriteLine("xe not found");
                        continue;
                    }
                    ChuyenDi copyOfChuyendi = new ChuyenDi
                    {
                        dsDiem = new List<Point>(chuyendi.dsDiem),
                        tongsonguoi = chuyendi.tongsonguoi,
                        xedi = chuyendi.xedi
                    };
                    int vitriOrgTrongChuyenDi = 0;
                    int vitriDesTrongChuyenDi = 0;

                    double tongthoigian_truoc = 0;
                    double tongthoigian_sau = 0;

                    for (int i = 0; i < copyOfChuyendi.dsDiem.Count; i++)
                    {
                        double tgxd_luotdi = gio_stringToDouble(copyOfChuyendi.dsDiem[i].thoigianxeden_luotdi);
                        double tgxd_luotve = gio_stringToDouble(copyOfChuyendi.dsDiem[i].thoigianxeden_luotve);

                        foreach (var pointdetail in copyOfChuyendi.dsDiem[i].dsdiemcungtoado)
                        {
                            if (pointIdIsOrigrin(pointdetail))
                            {
                                tongthoigian_truoc += gio_stringToDouble(lichdangky[pointdetail.id].GioDen) - tgxd_luotdi;
                                tongthoigian_truoc += tgxd_luotve - gio_stringToDouble(lichdangky[pointdetail.id].GioVe);
                            }
                        }
                    }


                    double doanduongdaithem = 0;
                    double tongthoigianduthuathem = 0;
                    bool DaCoToaDoOrigin = false;
                    bool DaCoToaDoDestination = false;
                    bool OrgCuoiDanhSach = false;
                    bool DesCuoiDanhSach = false;
                    //thêm điểm origin và destination vào list
                    if (chuyendibandau != null)  //thêm điểm dựa vào chuyến đi ban đầu
                    {
                        int Index = 0;
                        bool OriginAdded = false, DestinationAdded = false;
                        for (int i = 0; i < chuyendibandau.Count; i++)
                        {
                            if (OriginAdded && DestinationAdded)
                                break;
                            bool issame = false;
                            if (Index < copyOfChuyendi.dsDiem.Count && chuyendibandau[i].location.IsSame(copyOfChuyendi.dsDiem[Index].location))
                                issame = true;
                            foreach (var item in chuyendibandau[i].dsdiemcungtoado)
                            {
                                //điểm Origin
                                if (item.id == OriginID)
                                {
                                    vitriOrgTrongChuyenDi = Index;
                                    if (issame)
                                    {   //location đã trùng -> thêm id vào dsdiemcungtoado
                                        DaCoToaDoOrigin = true;
                                        copyOfChuyendi.dsDiem[Index].dsdiemcungtoado.Add(orgPoint.dsdiemcungtoado[0]);
                                        OriginAdded = true;
                                    }
                                    else
                                    {   //chèn điểm vào trước vị trí Index
                                        if (Index == copyOfChuyendi.dsDiem.Count)
                                        {
                                            OrgCuoiDanhSach = true;
                                            //orgPoint.thoigianxeden_luotdi = gio_doubleToString(gio_stringToDouble(copyOfChuyendi.dsDiem[Index - 1].thoigianxeden_luotdi) +durationmatrix[copyOfChuyendi.dsDiem[Index-1].dsdiemcungtoado[0].id][OriginID]);
                                            copyOfChuyendi.dsDiem.Add(orgPoint);
                                        }
                                        else
                                            copyOfChuyendi.dsDiem.Insert(Index, orgPoint);
                                        doanduongdaithem += Index == 0 ? distancematrix[OriginID][chuyendi.dsDiem[0].dsdiemcungtoado[0].id] :
                                                                         distancematrix[Index - 1][Index] + distancematrix[Index][Index + 1] - distancematrix[Index - 1][Index + 1];
                                        Index++;
                                        OriginAdded = true;
                                    }
                                }
                                //điểm Destination
                                if (item.id == DestinationID)
                                {
                                    vitriDesTrongChuyenDi = Index;
                                    if (issame)
                                    {   //location đã trùng -> thêm id vào dsdiemcungtoado
                                        DaCoToaDoDestination = true;
                                        copyOfChuyendi.dsDiem[Index].dsdiemcungtoado.Add(desPoint.dsdiemcungtoado[0]);
                                        if (Index == copyOfChuyendi.dsDiem.Count - 1)
                                            DesCuoiDanhSach = true;
                                        DestinationAdded = true;
                                    }
                                    else
                                    {   //chèn điểm vào trước vị trí Index
                                        if (Index == copyOfChuyendi.dsDiem.Count)
                                        {
                                            copyOfChuyendi.dsDiem.Add(desPoint);
                                            DesCuoiDanhSach = true;
                                        }
                                        else
                                            copyOfChuyendi.dsDiem.Insert(Index, desPoint);
                                        //check
                                        doanduongdaithem += Index == copyOfChuyendi.dsDiem.Count ? distancematrix[chuyendi.dsDiem[Index - 2].dsdiemcungtoado[0].id][DestinationID] :
                                                                         distancematrix[Index - 1][Index] + distancematrix[Index][Index + 1] - distancematrix[Index - 1][Index + 1];
                                        Index++;
                                        DestinationAdded = true;
                                    }
                                }
                            }
                            if (issame && (!OriginAdded || !DestinationAdded))
                                Index++;
                        }

                    }
                    else     // thêm điểm thủ công (khi thêm phải xét khía cạnh địa điểm, phương hướng)
                    {
                        bool diNguocChieu = false;
                        for (int i = 0; i < copyOfChuyendi.dsDiem.Count - 1; i++)
                        {
                            if (orgPoint.location.IsSame(copyOfChuyendi.dsDiem[i].location))
                            {
                                DaCoToaDoOrigin = true;
                                vitriOrgTrongChuyenDi = i;
                                copyOfChuyendi.dsDiem[i].dsdiemcungtoado.Add(new point_detail { id = OriginID, songuoi = songuoidi });
                                break;
                            }
                            if (orgPoint.location.IsSame(copyOfChuyendi.dsDiem[i + 1].location))
                                continue;
                            int vitricogoclonnhat = TimGocLonNhat(copyOfChuyendi.dsDiem[i].location, orgPoint.location, copyOfChuyendi.dsDiem[i + 1].location);
                            if (vitricogoclonnhat == 3)
                            {
                                if (i + 1 == copyOfChuyendi.dsDiem.Count - 1)
                                {
                                    OrgCuoiDanhSach = true;
                                    copyOfChuyendi.dsDiem.Add(orgPoint);
                                    vitriOrgTrongChuyenDi = i + 2;
                                    break;
                                }
                                continue;
                            }
                            if (vitricogoclonnhat == 1)
                            {
                                vitriOrgTrongChuyenDi = i;
                                copyOfChuyendi.dsDiem.Insert(i, orgPoint);
                                break;
                            }
                            if (vitricogoclonnhat == 2)
                            {
                                vitriOrgTrongChuyenDi = i + 1;
                                copyOfChuyendi.dsDiem.Insert(i + 1, orgPoint);
                                break;
                            }
                        }
                        for (int i = 0; i < copyOfChuyendi.dsDiem.Count - 1; i++)
                        {
                            if (desPoint.location.IsSame(copyOfChuyendi.dsDiem[i].location))
                            {
                                if (i < vitriOrgTrongChuyenDi)
                                {
                                    diNguocChieu = true;
                                    break;
                                }
                                DaCoToaDoDestination = true;
                                vitriDesTrongChuyenDi = i;
                                copyOfChuyendi.dsDiem[i].dsdiemcungtoado.Add(new point_detail { id = DestinationID, songuoi = songuoidi, thoigianden = lichdangky[OriginID].GioDen, thoigianve = lichdangky[OriginID].GioVe, khungthoigianden = khungthoigianden, khungthoigianve = khungthoigianve });

                                if (i == copyOfChuyendi.dsDiem.Count - 1)
                                    DesCuoiDanhSach = true;
                                break;
                            }
                            if (desPoint.location.IsSame(copyOfChuyendi.dsDiem[i + 1].location))
                                continue;
                            int vitricogoclonnhat = TimGocLonNhat(copyOfChuyendi.dsDiem[i].location, desPoint.location, copyOfChuyendi.dsDiem[i + 1].location);
                            if (vitricogoclonnhat == 3)
                            {
                                if (i + 1 == copyOfChuyendi.dsDiem.Count - 1)
                                {
                                    copyOfChuyendi.dsDiem.Add(desPoint);
                                    DesCuoiDanhSach = true;
                                    vitriDesTrongChuyenDi = copyOfChuyendi.dsDiem.Count - 1;
                                    break;
                                }
                                continue;
                            }
                            if (vitricogoclonnhat == 1)
                            {
                                if (i <= vitriOrgTrongChuyenDi)
                                {
                                    diNguocChieu = true;
                                    break;
                                }
                                copyOfChuyendi.dsDiem.Insert(i, desPoint);
                                vitriDesTrongChuyenDi = i;
                                break;
                            }
                            if (vitricogoclonnhat == 2)
                            {
                                if (i + 1 < vitriOrgTrongChuyenDi)
                                {
                                    diNguocChieu = true;
                                    break;
                                }
                                copyOfChuyendi.dsDiem.Insert(i + 1, desPoint);
                                vitriDesTrongChuyenDi = i + 1;
                                break;
                            }
                        }
                        if (diNguocChieu)
                        {
                            Console.WriteLine("đi ngc chiều.");
                            continue;
                        }
                    }

                    int sodiem = copyOfChuyendi.dsDiem.Count;

                    ////lấy thời gian đi của mọi người đi xe
                    //double tgtruoc = 0, tgsau = 0;    // tổng thời gian trên chuyến đi của tất cả nhân viên
                    //int songuoitrenxe_truoc = 0, songuoitrenxe_sau = 0;
                    //int iddiemtruoc = 0;

                    //for (int ind = 0; ind < sodiem; ind++)
                    //{
                    //    if (ind == vitriOrgTrongChuyenDi || ind == vitriDesTrongChuyenDi)
                    //    {
                    //        if (copyOfChuyendi.dsDiem[ind].dsdiemcungtoado.Count == 1)    //điểm chứa origin chưa tồn tại từ trước
                    //        {
                    //            iddiemtruoc = tgtruoc == 0 ? copyOfChuyendi.dsDiem[ind + 1].dsdiemcungtoado[0].id : copyOfChuyendi.dsDiem[ind - 1].dsdiemcungtoado[0].id;
                    //            tgsau += ind == 0 ? 0 : songuoitrenxe_sau * durationmatrix[copyOfChuyendi.dsDiem[ind - 1].dsdiemcungtoado[0].id][copyOfChuyendi.dsDiem[ind].dsdiemcungtoado[0].id];
                    //            songuoitrenxe_sau += ind == vitriOrgTrongChuyenDi ? 1 : -1;
                    //        }
                    //        else
                    //        {
                    //            tgtruoc += ind == 0 ? 0 : songuoitrenxe_truoc * durationmatrix[iddiemtruoc][copyOfChuyendi.dsDiem[ind].dsdiemcungtoado[0].id];
                    //            tgsau += ind == 0 ? 0 : songuoitrenxe_sau * durationmatrix[copyOfChuyendi.dsDiem[ind - 1].dsdiemcungtoado[0].id][copyOfChuyendi.dsDiem[ind].dsdiemcungtoado[0].id];
                    //            foreach (var pointdetail in copyOfChuyendi.dsDiem[ind].dsdiemcungtoado)
                    //            {
                    //                songuoitrenxe_truoc += pointIdIsOrigrin(pointdetail) ? (pointdetail.id == OriginID ? 0 : 1) : (pointdetail.id == DestinationID ? 0 : -1);
                    //                songuoitrenxe_sau += pointIdIsOrigrin(pointdetail) ? 1 : -1;
                    //            }

                    //            iddiemtruoc = copyOfChuyendi.dsDiem[ind].dsdiemcungtoado[0].id;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        tgtruoc += ind == 0 ? 0 : songuoitrenxe_truoc * durationmatrix[iddiemtruoc][copyOfChuyendi.dsDiem[ind].dsdiemcungtoado[0].id];
                    //        tgsau += ind == 0 ? 0 : songuoitrenxe_sau * durationmatrix[copyOfChuyendi.dsDiem[ind - 1].dsdiemcungtoado[0].id][copyOfChuyendi.dsDiem[ind].dsdiemcungtoado[0].id];
                    //        foreach (var pointdetail in copyOfChuyendi.dsDiem[ind].dsdiemcungtoado)
                    //        {
                    //            songuoitrenxe_truoc += pointIdIsOrigrin(pointdetail) ? 1 : -1;
                    //            songuoitrenxe_sau += pointIdIsOrigrin(pointdetail) ? 1 : -1;
                    //        }

                    //        iddiemtruoc = copyOfChuyendi.dsDiem[ind].dsdiemcungtoado[0].id;
                    //    }

                    //    //foreach (var pointdetail in copyOfChuyendi.dsDiem[ind].dsdiemcungtoado)
                    //    //{
                    //    //    foreach (var item in dsnguoichuaden)
                    //    //    {
                    //    //        dsthoigiandi[item] += durationmatrix[ind - 1][ind];
                    //    //    }
                    //    //    if (pointIdIsOrigrin(pointdetail))  //origin
                    //    //    {
                    //    //        dsnguoichuaden.Add(pointdetail.id);
                    //    //        dsthoigiandi.Add(pointdetail.id, 0.0);
                    //    //    }
                    //    //    else    //destination
                    //    //    {
                    //    //        for (int i = 0; i < dsnguoichuaden.Count; i++)
                    //    //        {
                    //    //            if (dsnguoichuaden[i] == getOriginID(pointdetail.id))
                    //    //                dsnguoichuaden.RemoveAt(i);
                    //    //        }
                    //    //    }
                    //    //}

                    //}
                    //Console.WriteLine("tg trước: {0}, tg sau: {1}", tgtruoc, tgsau);
                    //double delta = tgsau - tgtruoc;

                    bool dieukien1 = true;  //dieukien1: chuyến đi này khi thêm vào không ảnh hưởng giờ giấc lượt đi của những chuyến khác trong list
                    bool dieukien2 = true;  //dieukien2: chuyến đi này khi thêm vào không ảnh hưởng giờ giấc lượt về của những chuyến khác trong list

                    var TaskList = new List<Task>();

                    //xét điều kiện 1       //KIỂM TRA LẠI ĐK 1
                    //if (!(DaCoToaDoOrigin && DaCoToaDoDestination) && vitriOrgTrongChuyenDi != 0 && !OrgCuoiDanhSach)
                    //{
                    int idTruocOrg = vitriOrgTrongChuyenDi == 0 ? -1 : copyOfChuyendi.dsDiem[vitriOrgTrongChuyenDi - 1].dsdiemcungtoado[0].id;
                    int idSauOrg = copyOfChuyendi.dsDiem[vitriOrgTrongChuyenDi + 1].dsdiemcungtoado[0].id;
                    int idTruocDes = copyOfChuyendi.dsDiem[vitriDesTrongChuyenDi - 1].dsdiemcungtoado[0].id;
                    int idSauDes = vitriDesTrongChuyenDi == sodiem - 1 ? -1 : copyOfChuyendi.dsDiem[vitriDesTrongChuyenDi + 1].dsdiemcungtoado[0].id;
                    double thoigianthembot_Org = (DaCoToaDoOrigin || vitriOrgTrongChuyenDi == 0 || vitriOrgTrongChuyenDi == sodiem - 2) ? 0 : (double)(durationmatrix[idTruocOrg][OriginID] + durationmatrix[OriginID][idSauOrg] - durationmatrix[idTruocOrg][idSauOrg]) / (60 * 60);
                    double thoigianthembot_Des = (DaCoToaDoDestination || vitriDesTrongChuyenDi == 1 || vitriDesTrongChuyenDi == sodiem - 1) ? 0 : (double)(durationmatrix[idTruocDes][DestinationID] + durationmatrix[DestinationID][idSauDes] - durationmatrix[idTruocDes][idSauDes]) / (60 * 60);

                    bool timthayvitriOrg = false;

                    double tgxd = 0, mintime = 0, maxtime = 0;
                    int vitri = -1;
                    for (int i = 0; i < sodiem; i++)
                    {
                        bool breakRequest = false;

                        if (i < vitriDesTrongChuyenDi)
                        {
                            foreach (var pointdetail in copyOfChuyendi.dsDiem[i].dsdiemcungtoado)
                            {
                                if (pointIdIsOrigrin(pointdetail))
                                    continue;
                                tgxd = gio_stringToDouble(copyOfChuyendi.dsDiem[i].thoigianxeden_luotve);
                                mintime = gio_stringToDouble(pointdetail.khungthoigianve[0]);
                                maxtime = gio_stringToDouble(pointdetail.khungthoigianve[1]);

                                vitri = i;
                            }
                        }

                        if (i == vitriOrgTrongChuyenDi)
                        {
                            timthayvitriOrg = true;
                            continue;   //bỏ điểm hiện tại, xét điểm tiếp theo
                        }
                        //if (i == vitriDesTrongChuyenDi)
                        //{
                        //    timthayvitriDes = true;
                        //    continue;   //bỏ điểm hiện tại, xét điểm tiếp theo
                        //}
                        //if (timthayvitriDes)
                        //{
                        //    continue;
                        //}
                        if (!timthayvitriOrg)
                        {
                            //cập nhật thời gian xe đến (lượt về) (vì khi thêm điểm origin làm thời gian về của các điểm destination trước điểm origin này bị trễ hoặc sớm)
                            double tgxd_truockhithem = gio_stringToDouble(copyOfChuyendi.dsDiem[i].thoigianxeden_luotve);
                            double tgxd_saukhithem = tgxd_truockhithem + thoigianthembot_Org + thoigianthembot_Des;
                            copyOfChuyendi.dsDiem[i].thoigianxeden_luotve = gio_doubleToString(tgxd_saukhithem);
                        }
                        else
                        {
                            //if (DaCoToaDoOrigin)
                            //    continue;
                            //xét đk giờ lượt đi
                            foreach (var pointdetail in copyOfChuyendi.dsDiem[i].dsdiemcungtoado)
                            {
                                /*dán đk  "&& pointdetail.id != DestinationID"  */  // đã xử lý
                                if (pointIdIsDestination(pointdetail))  //tìm kiếm điểm Destination trong các điểm nằm sau vị trí điểm Origin
                                {
                                    double tgxd_truockhithem = DesCuoiDanhSach ? 0 : gio_stringToDouble(copyOfChuyendi.dsDiem[i].thoigianxeden_luotdi);
                                    double tgxd_saukhithem = DesCuoiDanhSach ? 0 : tgxd_truockhithem + thoigianthembot_Org;
                                    tgxd_saukhithem += DesCuoiDanhSach ? 0 : vitriDesTrongChuyenDi == 1 ? 0 : i > vitriDesTrongChuyenDi ? thoigianthembot_Des : 0;
                                    double thoigianduoi = DesCuoiDanhSach ? 0 : gio_stringToDouble(pointdetail.khungthoigianden[0]);
                                    double thoigiantren = DesCuoiDanhSach ? 0 : gio_stringToDouble(pointdetail.khungthoigianden[1]);

                                    // nếu sau khi thêm mà tgxd điểm này thấp hơn thời gian dưới thì tgxuatphat sẽ sớm hơn để đáp ứng nó
                                    // nếu tgxd điểm này lớn hơn thời gian trên thì tgxuatphat sẽ phải muộn hơn
                                    // nếu không thì không thay đổi thời gian xuất phát
                                    double tgxuatphatThaydoi1 = 0;
                                    if (DesCuoiDanhSach)
                                    {
                                        double tgxd_diemcuoi = copyOfChuyendi.dsDiem[sodiem - 1].dsdiemcungtoado.Count > 1 ?
                                            gio_stringToDouble(copyOfChuyendi.dsDiem[sodiem - 1].thoigianxeden_luotdi)
                                            : gio_stringToDouble(copyOfChuyendi.dsDiem[sodiem - 2].thoigianxeden_luotdi) + (double)(durationmatrix[copyOfChuyendi.dsDiem[sodiem - 2].dsdiemcungtoado[0].id][DestinationID]) / (60 * 60);

                                        tgxuatphatThaydoi1 = gio_stringToDouble(khungthoigianden[0]) > tgxd_diemcuoi ? gio_stringToDouble(khungthoigianden[0]) - tgxd_diemcuoi
                                                : (gio_stringToDouble(khungthoigianden[1]) < tgxd_diemcuoi ? gio_stringToDouble(khungthoigianden[1]) - tgxd_diemcuoi : 0);
                                    }
                                    else
                                        tgxuatphatThaydoi1 = thoigianduoi - tgxd_saukhithem > 0 ? thoigianduoi - tgxd_saukhithem : (thoigiantren - tgxd_saukhithem < 0 ? thoigiantren - tgxd_saukhithem : 0);

                                    double thoigianthembot1 = 0;
                                    double thoigianthembot2 = thoigianthembot_Org + thoigianthembot_Des;

                                    double thoigianxecho = gio_stringToDouble(khungthoigianve[1]) - gio_stringToDouble(khungthoigianve[0]); //thoigianxecho bằng thời gian lệch tối đa của chuyến đi
                                    tgxd += thoigianthembot_Des;
                                    /**/
                                    tgxd += vitriOrgTrongChuyenDi == sodiem - 2 ? 0 : vitri < vitriOrgTrongChuyenDi ? thoigianthembot_Org : 0;

                                    double tgxuatphatThaydoi2 = 0;
                                    if (DesCuoiDanhSach)
                                    {
                                        double tgxd_diemcuoi = copyOfChuyendi.dsDiem[sodiem - 1].dsdiemcungtoado.Count > 1 ? //điểm destination đã tồn tại
                                            gio_stringToDouble(copyOfChuyendi.dsDiem[sodiem - 1].thoigianxeden_luotve)
                                            : gio_stringToDouble(copyOfChuyendi.dsDiem[sodiem - 2].thoigianxeden_luotve) - (double)(durationmatrix[copyOfChuyendi.dsDiem[sodiem - 2].dsdiemcungtoado[0].id][DestinationID]) / (60 * 60);
                                        /*fdfdf*/
                                        tgxuatphatThaydoi2 = tgxd_diemcuoi < gio_stringToDouble(khungthoigianve[0]) ? gio_stringToDouble(khungthoigianve[0]) - tgxd_diemcuoi : 0;
                                        if (tgxuatphatThaydoi2 == 0)
                                            if (copyOfChuyendi.dsDiem[sodiem - 1].dsdiemcungtoado.Count == 1)
                                                copyOfChuyendi.dsDiem[sodiem - 2].thoigianxeden_luotve = gio_doubleToString(gio_stringToDouble(khungthoigianve[0]) + (double)durationmatrix[DestinationID][copyOfChuyendi.dsDiem[sodiem - 2].dsdiemcungtoado[0].id] / (60 * 60));
                                        //else
                                        //    copyOfChuyendi.dsDiem[sodiem - 1].thoigianxeden_luotve = khungthoigianve[0];
                                    }
                                    else
                                    {
                                        if (vitri == -1)     //destination này là des cuối cùng trong list (theo hướng đi về)
                                        {
                                            double tgxd_diemcuoi = copyOfChuyendi.dsDiem[1].dsdiemcungtoado.Count > 1 ? //điểm destination đã tồn tại
                                                gio_stringToDouble(copyOfChuyendi.dsDiem[1].thoigianxeden_luotve)
                                                : gio_stringToDouble(copyOfChuyendi.dsDiem[2].thoigianxeden_luotve) + (double)(durationmatrix[DestinationID][copyOfChuyendi.dsDiem[2].dsdiemcungtoado[0].id]) / (60 * 60);

                                            tgxuatphatThaydoi2 = tgxd_diemcuoi > gio_stringToDouble(khungthoigianve[1]) ? gio_stringToDouble(khungthoigianve[1]) - tgxd_diemcuoi : 0;
                                        }
                                        else
                                            tgxuatphatThaydoi2 = mintime - tgxd > 0 ? mintime - tgxd : (maxtime - tgxd < 0 ? maxtime - tgxd : 0);
                                    }

                                    double tgdagiulai = 0;
                                    double tgxpthaydoiconlai = tgxuatphatThaydoi2;
                                    int taskcapnhatgiove = 0;
                                    for (int j = 0; j < sodiem; j++)  //lặp lại chuyến đi để xét dk1 và lưu task cho dk2
                                    {
                                        if ((j == vitriOrgTrongChuyenDi && !DaCoToaDoOrigin) || (j == vitriDesTrongChuyenDi && !DaCoToaDoDestination))
                                        {
                                            if (j == vitriOrgTrongChuyenDi)
                                            {
                                                double tgxd_luotdi = j != 0 ? gio_stringToDouble(copyOfChuyendi.dsDiem[j - 1].thoigianxeden_luotdi) + (double)(durationmatrix[copyOfChuyendi.dsDiem[j - 1].dsdiemcungtoado[0].id][OriginID]) / (60 * 60) :
                                                    (j + 1 == vitriDesTrongChuyenDi && !DaCoToaDoDestination) ?
                                                        gio_stringToDouble(copyOfChuyendi.dsDiem[j + 2].thoigianxeden_luotdi) - (double)(durationmatrix[DestinationID][copyOfChuyendi.dsDiem[i + 2].dsdiemcungtoado[0].id] + durationmatrix[OriginID][DestinationID]) / (60 * 60)
                                                        : gio_stringToDouble(copyOfChuyendi.dsDiem[j + 1].thoigianxeden_luotdi) - (double)(durationmatrix[OriginID][copyOfChuyendi.dsDiem[j + 1].dsdiemcungtoado[0].id]) / (60 * 60);
                                                copyOfChuyendi.dsDiem[j].thoigianxeden_luotdi = gio_doubleToString(tgxd_luotdi);

                                                double tgxd_luotve = j == sodiem - 2 ?
                                                    gio_stringToDouble(copyOfChuyendi.dsDiem[sodiem - 3].thoigianxeden_luotve) - (double)(durationmatrix[copyOfChuyendi.dsDiem[sodiem - 3].dsdiemcungtoado[0].id][OriginID]) / (60 * 60)
                                                    : gio_stringToDouble(copyOfChuyendi.dsDiem[j + 1].thoigianxeden_luotve) + (double)(durationmatrix[copyOfChuyendi.dsDiem[j + 1].dsdiemcungtoado[0].id][OriginID]) / (60 * 60);
                                                copyOfChuyendi.dsDiem[j].thoigianxeden_luotve = gio_doubleToString(tgxd_luotve);

                                                thoigianthembot2 -= thoigianthembot_Org;

                                                continue;
                                            }
                                            if (j == vitriDesTrongChuyenDi)
                                            {
                                                double tgxd_luotdi = gio_stringToDouble(copyOfChuyendi.dsDiem[j - 1].thoigianxeden_luotdi) + (double)(durationmatrix[copyOfChuyendi.dsDiem[j - 1].dsdiemcungtoado[0].id][DestinationID]) / (60 * 60);
                                                //double min = gio_stringToDouble(khungthoigianden[0]);
                                                //double max = gio_stringToDouble(khungthoigianden[1]);
                                                //if(tgxd_luotdi < min || tgxd_luotdi > max)

                                                copyOfChuyendi.dsDiem[j].thoigianxeden_luotdi = gio_doubleToString(tgxd_luotdi);

                                                thoigianthembot2 -= thoigianthembot_Des;

                                                continue;
                                            }
                                        }

                                        double tgxd_luotdi_truockhithem = gio_stringToDouble(copyOfChuyendi.dsDiem[j].thoigianxeden_luotdi);
                                        double tgxd_luotdi_saukhithem = tgxd_luotdi_truockhithem + tgxuatphatThaydoi1 + thoigianthembot1;
                                        copyOfChuyendi.dsDiem[j].thoigianxeden_luotdi = gio_doubleToString(tgxd_luotdi_saukhithem);

                                        double tgxd_luotve_truockhithem = gio_stringToDouble(copyOfChuyendi.dsDiem[j].thoigianxeden_luotve);
                                        double tgxd_luotve_saukhithem = tgxd_luotve_truockhithem + tgxuatphatThaydoi2 + thoigianthembot2;
                                        //copyOfChuyendi.dsDiem[j].thoigianxeden_luotve = gio_doubleToString(tgxd_luotve_saukhithem);
                                        int ind = j;
                                        foreach (point_detail pointdetail1 in copyOfChuyendi.dsDiem[j].dsdiemcungtoado)
                                        {
                                            if (pointIdIsDestination(pointdetail1))
                                            {
                                                //Console.WriteLine("lượt đi. iD: {0}, khung thời gian về: [{1}, {2}]", pointdetail1.id, pointdetail1.khungthoigianve[0], pointdetail1.khungthoigianve[1]);
                                                tongthoigianduthuathem += tgxd_luotdi_truockhithem - tgxd_luotdi_saukhithem;     //
                                                tongthoigianduthuathem += tgxd_luotve_saukhithem - tgxd_luotve_truockhithem;

                                                //check ddk lượt đi
                                                double min = gio_stringToDouble(pointdetail1.khungthoigianden[0]);
                                                double max = gio_stringToDouble(pointdetail1.khungthoigianden[1]);
                                                if (tgxd_luotdi_saukhithem < min || tgxd_luotdi_saukhithem > max)   //không thỏa
                                                {
                                                    dieukien1 = false;
                                                    breakRequest = true;
                                                    break;
                                                }
                                            }

                                            /////// lưu task điều kiện thời gian lượt về
                                            var task = new Task(() =>
                                            {
                                                if (ind != taskcapnhatgiove)
                                                {
                                                    //if (pointIdIsDestination(pointdetail1))
                                                    //{
                                                    //    thoigianxedoi += tgxd_luotve_saukhithem < gio_stringToDouble(pointdetail1.khungthoigianve[0]) ?
                                                    //        gio_stringToDouble(pointdetail1.khungthoigianve[0]) - tgxd_luotve_saukhithem : 0;
                                                    //}

                                                    //tgxd_luotve_saukhithem += thoigianxedoi;

                                                    tgxd_luotve_saukhithem -= tgdagiulai;

                                                    double thoigianxedoi = (pointIdIsOrigrin(pointdetail1) || gio_stringToDouble(pointdetail1.khungthoigianve[0]) <= tgxd_luotve_saukhithem) ? 0 :
                                                        gio_stringToDouble(pointdetail1.khungthoigianve[0]) - tgxd_luotve_saukhithem;

                                                    tgdagiulai += thoigianxedoi <= tgxpthaydoiconlai ? thoigianxedoi : tgxpthaydoiconlai;
                                                    tgxpthaydoiconlai -= thoigianxedoi <= tgxpthaydoiconlai ? thoigianxedoi : tgxpthaydoiconlai;

                                                    copyOfChuyendi.dsDiem[ind].thoigianxeden_luotve = gio_doubleToString(tgxd_luotve_saukhithem);
                                                    taskcapnhatgiove = ind;
                                                }

                                                //Console.WriteLine("task {0} đag thực thi", ind);
                                                //// có 2 đk: tg đến của xe phải nhỏ hơn khungthoigianve[1], tg xe chờ phải nhỏ hơn tg chờ nhỏ nhất của những người trên xe
                                                //Console.WriteLine("lượt về. iD: {0}, khung thời gian về: [{1}, {2}]", pointdetail1.id, pointdetail1.khungthoigianve[0], pointdetail1.khungthoigianve[1]);

                                                if (pointIdIsDestination(pointdetail1))
                                                {
                                                    if (tgxd_luotve_saukhithem > gio_stringToDouble(pointdetail1.khungthoigianve[1]) || gio_stringToDouble(pointdetail1.khungthoigianve[0]) - tgxd_luotve_saukhithem > thoigianxecho)
                                                    {
                                                        dieukien2 = false;
                                                    }

                                                    //nếu điểm này thỏa thì cập nhật thoigianxecho
                                                    double thoigianlechtoida = gio_stringToDouble(pointdetail1.khungthoigianve[1]) - gio_stringToDouble(pointdetail1.khungthoigianve[0]);
                                                    thoigianxecho = thoigianlechtoida < thoigianxecho ? thoigianlechtoida : thoigianxecho;
                                                }

                                            });
                                            TaskList.Add(task);
                                            // end task

                                        }
                                        if (breakRequest)
                                            break;
                                        if (j == vitriOrgTrongChuyenDi)
                                            thoigianthembot1 += thoigianthembot_Org;
                                        if (j == vitriDesTrongChuyenDi)
                                            thoigianthembot1 += thoigianthembot_Des;
                                    }
                                    //đến đây thì tất cả điểm đã thỏa mãn giờ thay đổi (tgxuatphatthaydoi) => thoát ra và điều kiện 1 thỏa
                                    breakRequest = true;
                                    break;  //chỉ xét 1 lần, thỏa hay không đều thoát
                                }
                            }
                        }
                        if (breakRequest)
                            break;
                    }
                    //}
                    if (!dieukien1)
                    {
                        Console.WriteLine("ID {0}, chuyến đi {1}: k thỏa đk 1", OriginID, index);
                        continue;   //xét chuyến đi khác, nếu không có chuyến đi nào thỏa mãn thì nhảy đến (1)
                    }
                    Console.WriteLine("ID {0}, chuyến đi {1}: thỏa đk 1", OriginID, index);

                    //xét tiếp điều kiện 2
                    for (int i = TaskList.Count - 1; i >= 0; i--)
                    {
                        TaskList[i].RunSynchronously();
                        if (!dieukien2)
                            break;
                    }
                    //else     //tasklist.Count == 0 khi dk1 không được xét
                    //{
                    //    double thoigianxecho = gio_stringToDouble(khungthoigianve[1]) - gio_stringToDouble(khungthoigianve[0]);
                    //    double tgxuatphatThaydoi2 = gio_stringToDouble(desPoint.dsdiemcungtoado[0].khungthoigianve[0]) - gio_stringToDouble(copyOfChuyendi.dsDiem[sodiem - 2].thoigianxeden_luotve);

                    //    for (int i = sodiem - 2; i >= 1; i--)
                    //    {
                    //        double tgxd_luotve = gio_stringToDouble(copyOfChuyendi.dsDiem[i].thoigianxeden_luotve) + tgxuatphatThaydoi2;
                    //        foreach (var pointdetail in copyOfChuyendi.dsDiem[i].dsdiemcungtoado)
                    //        {
                    //            if (pointIdIsDestination(pointdetail))
                    //            {
                    //                if (tgxd_luotve > gio_stringToDouble(pointdetail.khungthoigianve[1]) || gio_stringToDouble(pointdetail.khungthoigianve[0]) - tgxd_luotve > thoigianxecho)
                    //                {
                    //                    dieukien2 = false;
                    //                }
                    //                //nếu điểm này thỏa thì cập nhật thoigianxecho
                    //                double thoigianlechtoida = gio_stringToDouble(pointdetail.khungthoigianve[1]) - gio_stringToDouble(pointdetail.khungthoigianve[0]);
                    //                thoigianxecho = thoigianlechtoida < thoigianxecho ? thoigianlechtoida : thoigianxecho;

                    //            }
                    //        }
                    //    }
                    //}
                    if (!dieukien2)
                    {
                        Console.WriteLine("ID {0}, chuyến đi {1}: k thỏa đk 2", OriginID, index);
                        continue;   //xét chuyến đi khác, nếu không có chuyến đi nào thỏa mãn thì nhảy đến (1)
                    }

                    Console.WriteLine("ID {0}, chuyến đi {1}: thỏa đk 2", OriginID, index);


                    ////xét tiếp điều kiện 3
                    //double tgxd_diemcuoi = gio_stringToDouble(copyOfChuyendi.dsDiem[sodiem - 2].thoigianxeden_luotdi);
                    //double tgdi_tudiemcuoi = durationmatrix[copyOfChuyendi.dsDiem[sodiem - 2].dsdiemcungtoado[0].id][DestinationID];
                    //double thoigianxeden = tgxd_diemcuoi + tgdi_tudiemcuoi;
                    //double minn = gio_stringToDouble(khungthoigianden[0]);
                    //double maxx = gio_stringToDouble(khungthoigianden[1]);
                    //if (thoigianxeden < minn || thoigianxeden > maxx)
                    //    dieukien3 = false;

                    //if (!dieukien3)
                    //{
                    //    Console.WriteLine("ID {0}, chuyến đi {1}: k thỏa đk 3", OriginID, index);
                    //    continue;   //xét chuyến đi khác, nếu không có chuyến đi nào thỏa mãn thì nhảy đến (1)
                    //}
                    //Console.WriteLine("ID {0}, chuyến đi {1}: thỏa đk 3", OriginID, index);

                    for (int i = 0; i < sodiem; i++)
                    {
                        double tgxd_luotdi = gio_stringToDouble(copyOfChuyendi.dsDiem[i].thoigianxeden_luotdi);
                        double tgxd_luotve = gio_stringToDouble(copyOfChuyendi.dsDiem[i].thoigianxeden_luotve);

                        foreach (var pointdetail in copyOfChuyendi.dsDiem[i].dsdiemcungtoado)
                        {
                            if (pointIdIsOrigrin(pointdetail))
                            {
                                tongthoigian_sau += gio_stringToDouble(lichdangky[pointdetail.id].GioDen) - tgxd_luotdi;
                                tongthoigian_sau += tgxd_luotve - gio_stringToDouble(lichdangky[pointdetail.id].GioVe);
                            }
                        }
                    }
                    double delta = tongthoigian_sau - tongthoigian_truoc;
                    copyOfChuyendi.tongsonguoi += songuoidi;
                    //double tongthoigianlangphi = delta + tongthoigianduthuathem;
                    dsChuyenDiThoaDk.Add(new { vitrichuyendi = index, xedi = xehientai, chuyendimoi = copyOfChuyendi, thoigianlangphi = delta });
                }
                if (dschuyendi.Count > 0 && dsChuyenDiThoaDk.Count > 0)
                {
                    dsChuyenDiThoaDk.Sort((x, y) => x.thoigianlangphi.CompareTo(y.thoigianlangphi));

                    for (int i = 0; i < dsChuyenDiThoaDk.Count; i++)  //chọn trường hợp tốt nhất
                    {
                        var listDangKyLichChiTietId = dschuyendi[dsChuyenDiThoaDk[i].vitrichuyendi].listDangKyLichChiTietId;
                        listDangKyLichChiTietId.Add(DangKyLichChiTietId);

                        int songuoihientai = dsChuyenDiThoaDk[i].xedi.songuoihientai;
                        if (songuoihientai + songuoidi > dsChuyenDiThoaDk[i].xedi.ThongTinXe.SoCho)
                        {
                            Xe xe = TimXe(songuoihientai + songuoidi);
                            if (xe == null)
                                continue;
                            dsChuyenDiThoaDk[i].xedi.trangthai = false;
                            dschuyendi[dsChuyenDiThoaDk[i].vitrichuyendi] = new ChuyenDi
                            {
                                dsDiem = new List<Point>(dsChuyenDiThoaDk[i].chuyendimoi.dsDiem),
                                tongsonguoi = dsChuyenDiThoaDk[i].chuyendimoi.tongsonguoi,
                                xedi = xe,
                                listDangKyLichChiTietId = listDangKyLichChiTietId
                            };
                            themchuyendithanhcong = true;
                            break;
                        }
                        else
                        {
                            dschuyendi[dsChuyenDiThoaDk[i].vitrichuyendi] = new ChuyenDi
                            {
                                dsDiem = new List<Point>(dsChuyenDiThoaDk[i].chuyendimoi.dsDiem),
                                tongsonguoi = dsChuyenDiThoaDk[i].chuyendimoi.tongsonguoi,
                                xedi = dsChuyenDiThoaDk[i].chuyendimoi.xedi,
                                listDangKyLichChiTietId = listDangKyLichChiTietId
                            };
                            dsChuyenDiThoaDk[i].xedi.songuoihientai += songuoidi;
                            themchuyendithanhcong = true;
                        }

                        //string giờ = "";
                        //for (int j = 0; j < dschuyendi[dsChuyenDiThoaDk[i].vitrichuyendi].dsDiem.Count; j++)
                        //{
                        //    string points = "";
                        //    giờ += "\nđiểm " + j + ": giờ đến đi " + dschuyendi[dsChuyenDiThoaDk[i].vitrichuyendi].dsDiem[j].thoigianxeden_luotdi + " giờ đến về " + dschuyendi[dsChuyenDiThoaDk[i].vitrichuyendi].dsDiem[j].thoigianxeden_luotve;
                        //    foreach (var item in dschuyendi[dsChuyenDiThoaDk[i].vitrichuyendi].dsDiem[j].dsdiemcungtoado)
                        //    {
                        //        points += "id: " + item.id + ", ";
                        //    }
                        //    Console.WriteLine(giờ);
                        //    Console.WriteLine(points);

                        //}
                    }
                }
                if (!themchuyendithanhcong)     //tạo chuyến đi khác        (1)
                {
                    dschuyendi.Add(new ChuyenDi
                    {
                        dsDiem = new List<Point>() { orgPoint, desPoint },
                        tongsonguoi = songuoidi,
                        xedi = TimXe(songuoidi),
                        listDangKyLichChiTietId = new List<int>() { DangKyLichChiTietId }
                    });
                    double giodi = (double)(durationmatrix[OriginID][DestinationID]) / (60 * 60);
                    dschuyendi[dschuyendi.Count - 1].dsDiem[1].thoigianxeden_luotdi = desPoint.dsdiemcungtoado[0].khungthoigianden[1];
                    dschuyendi[dschuyendi.Count - 1].dsDiem[0].thoigianxeden_luotdi = gio_doubleToString(gio_stringToDouble(desPoint.dsdiemcungtoado[0].khungthoigianden[1]) - giodi);

                    dschuyendi[dschuyendi.Count - 1].dsDiem[1].thoigianxeden_luotve = desPoint.dsdiemcungtoado[0].khungthoigianve[0];
                    dschuyendi[dschuyendi.Count - 1].dsDiem[0].thoigianxeden_luotve = gio_doubleToString(gio_stringToDouble(desPoint.dsdiemcungtoado[0].khungthoigianve[0]) + giodi);
                }

                for (int i = 0; i < dschuyendi[0].dsDiem.Count; i++)
                {
                    Console.WriteLine(dschuyendi[0].dsDiem[i].thoigianxeden_luotve);
                }
            }

            double gio_stringToDouble(string gio)
            {
                string[] hhmm = { "", "" };
                double hh, mm;

                char[] charArray = gio.ToCharArray();
                var k = 0;
                for (var i = 0; i < charArray.Length; i++)
                {
                    if (k == 2)
                        break;

                    if (charArray[i] != ':')
                        hhmm[k] += charArray[i];
                    else
                        k++;
                }
                bool hhsuccess = Double.TryParse(hhmm[0], out hh);
                bool mmsuccess = Double.TryParse(hhmm[1], out mm);
                if (!hhsuccess)
                    throw new Exception("Attempted conversion of " + hhmm[0] + " failed");
                if (!mmsuccess)
                    throw new Exception("Attempted conversion of " + hhmm[1] + " failed");

                return hh + mm / 60;
            }

            string gio_doubleToString(double gio)
            {
                int hh = (Int32)gio;
                int mm = (Int32)Math.Round((gio - hh) * 60);

                return hh + ":" + mm;
            }

            string[] laykhungthoigianden(string gioden, double thoigiandi)
            {
                int khoangchenhlech = ThoiGianLechToiDa(thoigiandi);
                double doublegioden = gio_stringToDouble(gioden);
                int hh = (Int32)doublegioden;
                int mm = (Int32)Math.Round((doublegioden - hh) * 60);

                mm -= khoangchenhlech % 60;
                if (mm < 0)
                {
                    mm = 60 + mm;
                    hh--;
                }
                hh -= khoangchenhlech / 60;
                if (hh < 0)
                {
                    hh = 24 + hh;
                    //trừ bớt ngày
                }

                string thoigianduoi = hh.ToString() + ":" + mm.ToString();
                string thoigiantren = gioden;

                string[] khungthoigian = { thoigianduoi, thoigiantren };
                return khungthoigian;
            }

            string[] laykhungthoigianve(string giove, double thoigiandi)
            {
                int khoangchenhlech = ThoiGianLechToiDa(thoigiandi);
                string[] hhmm = { "", "" };
                int hh, mm;

                char[] charArray = giove.ToCharArray();
                var k = 0;
                for (var i = 0; i < charArray.Length; i++)
                {
                    if (k == 2)
                        break;

                    if (charArray[i] != ':')
                        hhmm[k] += charArray[i];
                    else
                        k++;
                }
                hh = Int32.Parse(hhmm[0]);
                mm = Int32.Parse(hhmm[1]);

                mm += khoangchenhlech % 60;
                if (mm >= 60)
                {
                    mm = mm % 60;
                    hh++;
                }
                hh += khoangchenhlech / 60;
                if (hh >= 24)
                {
                    hh -= 24;
                    //cộng thêm ngày
                }

                string thoigianduoi = giove;
                string thoigiantren = hh.ToString() + ":" + mm.ToString();

                string[] khungthoigian = { thoigianduoi, thoigiantren };
                return khungthoigian;
            }

            int ThoiGianLechToiDa(double thoigiandi)
            {
                int max = 240;  //4 giờ
                int min = 15;   //15 phút
                int t = (int)Math.Round(4 * Math.Sqrt(thoigiandi));
                return t < min ? min : (t > max ? max : t);
            }

            bool pointContaintAllOrigrin(Point point)
            {
                foreach (var item in point.dsdiemcungtoado)
                {
                    if (item.id >= lichdangky.Count)
                        return false;
                }
                return true;
            }

            bool pointContaintAllDestination(Point point)
            {
                if (point.dsdiemcungtoado[0].id >= lichdangky.Count)
                    return true;
                return false;
            }

            bool pointIdIsOrigrin(point_detail point)
            {
                return point.id < lichdangky.Count ? true : false;
            }

            bool pointIdIsDestination(point_detail point)
            {
                return point.id >= lichdangky.Count ? true : false;
            }

            int getOriginID(int DestinationID)
            {
                return DestinationID - lichdangky.Count;
            }

            int getDestinationID(int OriginID)
            {
                return OriginID + lichdangky.Count;
            }

            Xe TimXe(int songuoidi)
            {
                foreach (var xe in dsXe)
                {
                    if (xe.trangthai || xe.ThongTinXe.SoCho < songuoidi)
                        continue;
                    xe.trangthai = true;
                    xe.songuoihientai = songuoidi;
                    return xe.ThongTinXe;
                }
                return null;
            }

            Point getOriginByDestination(Point DesinationPoint, List<Point> list)
            {
                if (pointIdIsDestination(DesinationPoint.dsdiemcungtoado[0]))
                    return null;
                foreach (Point point in list)
                {
                    foreach (var item in point.dsdiemcungtoado)
                    {
                        if (item.id == getOriginID(DesinationPoint.dsdiemcungtoado[0].id))
                        {
                            Point originPoint = point;
                            originPoint.dsdiemcungtoado.Clear();
                            originPoint.dsdiemcungtoado.Add(new point_detail
                            {
                                id = item.id,
                                khungthoigianden = item.khungthoigianden,
                                khungthoigianve = item.khungthoigianve
                            });
                            return originPoint;
                        }
                    }
                }
                return null;
            }

        }

        //here
        public static dynamic get_ORG_DES_WAY(List<Point> list)
        {
            Coordinate origin = list[0].location, destination = list[list.Count - 1].location;
            List<Coordinate> waypoints = new List<Coordinate>();
            for (int i = 1; i < list.Count - 1; i++)
            {
                waypoints.Add(list[i].location);
            }
            return new { origin, destination, waypoints };
        }

    }
}
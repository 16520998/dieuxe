using dieuxe.Helpers;
using dieuxe.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Linq;
using System;
using dieuxe.Services;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Windows.Input;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;

namespace dieuxe.ViewModels
{
    class ChiTietChuyenDiViewModel : INotifyPropertyChanged
    {

        private bool _laydulieuxong;
        public bool checkdata
        {
            get { return _laydulieuxong; }
            set
            {
                _laydulieuxong = value;
                OnPropertyChanged();
            }
        }

        private int _MaDieuxe { get; set; }
        private int _DangKyLichChiTietId { get; set; }
        private string _TenNguoiDi { get; set; }
        private int _SoNguoiDi { get; set; }
        private string _ThoiGianDen { get; set; }
        private string _ThoiGianVe { get; set; }
        private string _GioDen { get; set; }
        private string _GioVe { get; set; }
        private string _NgayDen { get; set; }
        private string _NgayVe { get; set; }
        private string _NoiDi { get; set; }
        private string _NoiDen { get; set; }
        private string _Ghichu { get; set; }
        private string _NgayDangKy { get; set; }
        private string _NgayCapNhat { get; set; }
        private List<ChiTietChuyenDi> _ChiTietChuyenDi;
        public List<ChiTietChuyenDi> ChiTietChuyenDi
        {
            get { return _ChiTietChuyenDi; }
            set
            {
                if (_ChiTietChuyenDi != value)
                {
                    _ChiTietChuyenDi = value;
                    OnPropertyChanged();
                }
            }
        }
        public int MaDieuxe
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
        public int DangKyLichChiTietId
        {
            get { return _DangKyLichChiTietId; }
            set
            {
                if (_DangKyLichChiTietId != value)
                {
                    _DangKyLichChiTietId = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TenNguoiDi
        {
            get { return _TenNguoiDi; }
            set
            {
                if (_TenNguoiDi != value)
                {
                    _TenNguoiDi = value;
                    OnPropertyChanged();
                }
            }
        }
        public int SoNguoiDi
        {
            get { return _SoNguoiDi; }
            set
            {
                if (_SoNguoiDi != value)
                {
                    _SoNguoiDi = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ThoiGianDen
        {
            get { return _ThoiGianDen; }
            set
            {
                if (_ThoiGianDen != value)
                {
                    _ThoiGianDen = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ThoiGianVe
        {
            get { return _ThoiGianVe; }
            set
            {
                if (_ThoiGianVe != value)
                {
                    _ThoiGianVe = value;
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
        public string NgayDen
        {
            get { return _NgayDen; }
            set
            {
                if (_NgayDen != value)
                {
                    _NgayDen = value;
                    OnPropertyChanged();
                }
            }
        }
        public string NgayVe
        {
            get { return _NgayVe; }
            set
            {
                if (_NgayVe != value)
                {
                    _NgayVe = value;
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
        public string NoiDen
        {
            get { return _NoiDen; }
            set
            {
                if (_NoiDen != value)
                {
                    _NoiDen = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Ghichu
        {
            get { return _Ghichu; }
            set
            {
                if (_Ghichu != value)
                {
                    _Ghichu = value;
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
        public string NgayCapNhat
        {
            get { return _NgayCapNhat; }
            set
            {
                if (_NgayCapNhat != value)
                {
                    _NgayCapNhat = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand DiemKeTiep { get; private set; }
        public int ChieuCaoListviewDiaChi { get; set; }
        public int ListviewDiaChiRowHeight { get; set; }

        List<Coordinate> DsToaDoDiemDung { get; set; }

        List<ThongTinDiemDung> _DanhSachDiemDung;
        public List<ThongTinDiemDung> DanhSachDiemDung 
        { 
            get { return _DanhSachDiemDung; }
            set
            {
                _DanhSachDiemDung = value;
                RaisePropertyChanged("DanhSachDiemDung");
            }
        }

        int indexToaDoKeTiep;
        string _DiaChiKeTiep;
        public string DiaChiKeTiep 
        {
            get { return _DiaChiKeTiep; }
            set
            {
                _DiaChiKeTiep = value;
                OnPropertyChanged("DiaChiKeTiep");
            }
        }
        Plugin.Geolocator.Abstractions.Position PositionKeTiep = new Plugin.Geolocator.Abstractions.Position();
        bool _DaDiQuaDiaChiKeTiep;
        public bool DaDiQuaDiaChiKeTiep 
        {
            get { return _DaDiQuaDiaChiKeTiep; }
            set
            {
                _DaDiQuaDiaChiKeTiep = value;
                OnPropertyChanged("DaDiQuaDiaChiKeTiep");
            }
        }
        public bool chuahoanthanhmoidiemdung = true;
        public bool ChuaHoanThanhMoiDiemDung
        {
            get { return chuahoanthanhmoidiemdung; }
            set
            {
                chuahoanthanhmoidiemdung = value;
                OnPropertyChanged("ChuaHoanThanhMoiDiemDung");
            }
        }
             List<ThongTinDiemDung> l = new List<ThongTinDiemDung>();
        public List<ThongTinDiemDung> L
        {
            get { return l; }
            set
            {
                l = value;
                OnPropertyChanged("L");
            }
        }
        
       //List<ThongTinDiemDung> _pickerSource = new List<ThongTinDiemDung>();
       // public List<ThongTinDiemDung> pickerSource
       // {
       //     get { return _pickerSource; }
       //     set
       //     {
       //         _pickerSource = value;
       //         OnPropertyChanged("pickerSource");
       //     }
       // }
        //ChuyenXe _selected;
        //public ChuyenXe selected
        //{
        //    get { return _selected; }
        //    set
        //    {
        //        _selected = value;
        //        OnPropertyChanged("selected");
        //    }
        //}
        public ICommand change { get; set; }
        public ChiTietChuyenDiViewModel()
        {
            change = new Command(() =>
            {
                L[0].DiaChi = "ẤDASDSA";
                //l[0].DiaChi = "ASD";
                //L = l;
                //List<ThongTinDiemDung> la = new List<ThongTinDiemDung>() { new ThongTinDiemDung { DiaChi = "đâsdas" } };
                //L = la;
            });
            //l.Add(new ThongTinDiemDung { DiaChi = "a" , pickerSource = new List<ChuyenXe> { new ChuyenXe { bienkiemsoat = "Item1" }, new ChuyenXe { bienkiemsoat = "Item2" }, new ChuyenXe { bienkiemsoat = "Item3" } } });
            //l.Add(new ThongTinDiemDung { DiaChi = "b", pickerSource = new List<ChuyenXe> { new ChuyenXe { bienkiemsoat = "Item1" }, new ChuyenXe { bienkiemsoat = "Item2" }, new ChuyenXe { bienkiemsoat = "Item3" } } });
            //l.Add(new ThongTinDiemDung { DiaChi = "c", pickerSource = new List<ChuyenXe> { new ChuyenXe { bienkiemsoat = "Item1" }, new ChuyenXe { bienkiemsoat = "Item2" }, new ChuyenXe { bienkiemsoat = "Item3" } } });
            //l.Add(new ThongTinDiemDung { DiaChi = "d", pickerSource = new List<ChuyenXe> { new ChuyenXe { bienkiemsoat = "Item1" }, new ChuyenXe { bienkiemsoat = "Item2" }, new ChuyenXe { bienkiemsoat = "Item3" } } });
            //l.Add(new ThongTinDiemDung { DiaChi = "e", pickerSource = new List<ChuyenXe> { new ChuyenXe { bienkiemsoat = "Item1" }, new ChuyenXe { bienkiemsoat = "Item2" }, new ChuyenXe { bienkiemsoat = "Item3" } } });
            

        }

        int TrangThaiChuyenXe;
        Dieuxe ChuyenXeHienTai;
        public ChiTietChuyenDiViewModel(Dieuxe chuyenXe)
        {
            ChuyenXeHienTai = chuyenXe;
            var accessToken = Settings.AccessToken;
            //Laydulieu(chuyenXe);
            ChiTietChuyenDi = new List<ChiTietChuyenDi>()
            {
                new ChiTietChuyenDi{ DangKyLichChiTietId = 1, ToaDoDi = "10.832282, 106.778043", ToaDoDen = "10.838085, 106.634272", NoiDi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", NoiDen = "17/6A Phan Huy Ích, Phường 15, Gò Vấp", SoNguoiDi = 2, TenNguoiDi = "Quốc, Nhân", SdtLienLac = "0123456789"},
                new ChiTietChuyenDi{ DangKyLichChiTietId = 2, ToaDoDi = "10.832282, 106.778043", ToaDoDen = "10.769082, 106.702199", NoiDi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", NoiDen = "16 Nam Kỳ Khởi Nghĩa, Phường Nguyễn Thái Bình, Quận 1", SoNguoiDi = 2, TenNguoiDi = "Hiền, Hậu", SdtLienLac = "035125748"},
                new ChiTietChuyenDi{ DangKyLichChiTietId = 3, ToaDoDi = "10.832282, 106.778043", ToaDoDen = "10.769082, 106.702199", NoiDi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", NoiDen = "16 Nam Kỳ Khởi Nghĩa, Phường Nguyễn Thái Bình, Quận 1", SoNguoiDi = 1, TenNguoiDi = "Long", SdtLienLac = "0351265473"},
                new ChiTietChuyenDi{ DangKyLichChiTietId = 4, ToaDoDi = "10.832282, 106.778043", ToaDoDen = "10.838781, 106.672548", NoiDi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", NoiDen = "236a Nguyễn Văn Lượng, Phường 6, Gò Vấp", SoNguoiDi = 2, TenNguoiDi = "Sơn Tùng MTP, Thiều Bảo Trâm", SdtLienLac = "0905235124"},
                new ChiTietChuyenDi{ DangKyLichChiTietId = 5, ToaDoDi = "10.832282, 106.778043", ToaDoDen = "10.769082, 106.702199", NoiDi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", NoiDen = "16 Nam Kỳ Khởi Nghĩa, Phường Nguyễn Thái Bình, Quận 1", SoNguoiDi = 2, TenNguoiDi = "Chế Linh, Trường Vũ ", SdtLienLac = "05555125484"},
                new ChiTietChuyenDi{ DangKyLichChiTietId = 6, ToaDoDi = "10.832282, 106.778043", ToaDoDen = "10.835409, 106.635244", NoiDi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", NoiDen = "243 Phan Huy Ích, Phường 12, Gò Vấp", SoNguoiDi = 1, TenNguoiDi = " Mỹ Tâm", SdtLienLac = "0666214587"},
                new ChiTietChuyenDi{ DangKyLichChiTietId = 7, ToaDoDi = "10.832282, 106.778043", ToaDoDen = "10.787637, 106.686105", NoiDi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", NoiDen = "209-201 Nam Kỳ Khởi Nghĩa, Phường 7, Quận 3", SoNguoiDi = 1, TenNguoiDi = "Võ Ngọc Tân", SdtLienLac = "0777521574"},
                new ChiTietChuyenDi{ DangKyLichChiTietId = 8, ToaDoDi = "10.832282, 106.778043", ToaDoDen = "10.803820, 106.689993", NoiDi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", NoiDen = "24a Phan Đăng Lưu, Phường 6, Bình Thạnh", SoNguoiDi = 2, TenNguoiDi = "Quang, Thuận", SdtLienLac = "0888215474"}
            };


            //ChiTietChuyenDi = new List<ChiTietChuyenDi>()
            //{
            //    new ChiTietChuyenDi{ DangKyLichChiTietId = 1, ToaDoDi = "10.836075,106.560716", ToaDoDen = "10.835963,106.560530", NoiDi = "đi 1", NoiDen = "đến 1", SoNguoiDi = 2, TenNguoiDi = "ADC, XB", SdtLienLac = "0123"},
            //    new ChiTietChuyenDi{ DangKyLichChiTietId = 2, ToaDoDi = "10.835978,106.560724", ToaDoDen = "10.835967,106.560193", NoiDi = "đi 2", NoiDen = "đến 2", SoNguoiDi = 1, TenNguoiDi = "C, Taoz", SdtLienLac = "0222"},
            //};
            //var lichdangky = new List<lich>() {
            //    new lich { sodangky = 0, ngaydangky = "25/03/2020", nhanviendangky = "SK10", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.845966, 106.765224", diachidi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", diachiden = "30-38 Dân Chủ, Bình Thọ, Thủ Đức", gioden = "08:00", ngayden = "20/03/2020", giove = "16:00", ngayve = "20/03/2020" },//
            //    new lich { sodangky = 1, ngaydangky = "25/03/2020", nhanviendangky = "SK06", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.838085, 106.634272", diachidi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", diachiden = "17/6A Phan Huy Ích, Phường 15, Gò Vấp", gioden = "08:00", ngayden = "20/03/2020", giove = "15:00", ngayve = "20/03/2020" },///////     7
            //    new lich { sodangky = 2, ngaydangky = "18/03/2020", nhanviendangky = "SK06", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.769082, 106.702199", diachidi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", diachiden = "16 Nam Kỳ Khởi Nghĩa, Phường Nguyễn Thái Bình, Quận 1", gioden = "08:00", ngayden = "20/03/2020", giove = "15:00", ngayve = "20/03/2020" },///////     1
            //    new lich { sodangky = 3, ngaydangky = "15/03/2020", nhanviendangky = "SK05", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.769082, 106.702199", diachidi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", diachiden = "16 Nam Kỳ Khởi Nghĩa, Phường Nguyễn Thái Bình, Quận 1", gioden = "08:00", ngayden = "20/03/2020", giove = "16:00", ngayve = "20/03/2020" },///////     2
            //    new lich { sodangky = 4, ngaydangky = "15/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.832282, 106.778043", noiden = "10.868034, 106.780647", diachidi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", diachiden = "940 Xa lộ Đại Hàn, Phường Linh Trung, Thủ Đức", gioden = "09:00", ngayden = "20/03/2020", giove = "15:00", ngayve = "20/03/2020" },//
            //    new lich { sodangky = 5, ngaydangky = "16/03/2020", nhanviendangky = "SK07", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.838781, 106.672548", diachidi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", diachiden = "236a Nguyễn Văn Lượng, Phường 6, Gò Vấp", gioden = "09:00", ngayden = "20/03/2020", giove = "17:00", ngayve = "20/03/2020" },///////     6
            //    new lich { sodangky = 6, ngaydangky = "18/03/2020", nhanviendangky = "SK06", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.769082, 106.702199", diachidi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", diachiden = "16 Nam Kỳ Khởi Nghĩa, Phường Nguyễn Thái Bình, Quận 1", gioden = "08:00", ngayden = "20/03/2020", giove = "15:00", ngayve = "20/03/2020" },///////     3
            //    new lich { sodangky = 7, ngaydangky = "19/03/2020", nhanviendangky = "SK05", songuoi = 2, noidi = "10.832282, 106.778043", noiden = "10.835409, 106.635244", diachidi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", diachiden = "243 Phan Huy Ích, Phường 12, Gò Vấp", gioden = "08:00", ngayden = "20/03/2020", giove = "15:00", ngayve = "20/03/2020" },///////     8
            //    new lich { sodangky = 8, ngaydangky = "21/03/2020", nhanviendangky = "SK09", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.884010, 106.586954", diachidi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", diachiden = "47 Xuyên Á, Xuân Thới Sơn, Hóc Môn", gioden = "08:00", ngayden = "20/03/2020", giove = "16:00", ngayve = "20/03/2020" },
            //    new lich { sodangky = 9, ngaydangky = "24/03/2020", nhanviendangky = "SK07", songuoi = 1, noidi = "10.832282, 106.778043", noiden = "10.787637, 106.686105", diachidi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", diachiden = "209-201 Nam Kỳ Khởi Nghĩa, Phường 7, Quận 3", gioden = "08:00", ngayden = "20/03/2020", giove = "16:00", ngayve = "20/03/2020" },///////     4
            //    new lich { sodangky = 10, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.832282, 106.778043", noiden = "10.803820, 106.689993", diachidi = "48 Đường Tăng Nhơn Phú, Tăng Nhơn Phú B, Quận 9", diachiden = "24a Phan Đăng Lưu, Phường 6, Bình Thạnh", gioden = "09:00", ngayden = "20/03/2020", giove = "16:00", ngayve = "20/03/2020" },///////    5
            //    new lich { sodangky = 11, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.803109, 106.737720", noiden = "10.842808, 106.615312", diachidi = "70 Nguyễn Duy Hiệu, Thảo Điền, Quận 2", diachiden = "An Sương 84, Xa lộ Đại Hàn, Đông Hưng Thuận, Hóc Môn", gioden = "08:00", ngayden = "20/03/2020", giove = "16:00", ngayve = "20/03/2020" },
            //    new lich { sodangky = 12, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.886754, 106.759661", noiden = "10.836570, 106.654286", diachidi = "338-350 Nguyễn Tri Phương, An Bình, Dĩ An, Bình Dương", diachiden = "693 Quang Trung, Phường 8, Gò Vấp", gioden = "08:00", ngayden = "20/03/2020", giove = "16:00", ngayve = "20/03/2020" },
            //    new lich { sodangky = 13, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.774183, 106.722131", noiden = "10.813318, 106.578647", diachidi = "232 Đường Mai Chí Thọ, An Lợi Đông, Quận 2", diachiden = "2300 Đường Vĩnh Lộc, Vĩnh Lộc B, Bình Chánh", gioden = "08:00", ngayden = "20/03/2020", giove = "16:00", ngayve = "20/03/2020" },
            //    new lich { sodangky = 14, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.848119, 106.718481", noiden = "10.806229, 106.627437", diachidi = "1/109 QL13, Hiệp Bình Phước, Thủ Đức", diachiden = "34 Lê Trọng Tấn, Sơn Ký, Tân Phú", gioden = "08:00", ngayden = "20/03/2020", giove = "16:00", ngayve = "20/03/2020" },
            //    new lich { sodangky = 15, ngaydangky = "24/03/2020", nhanviendangky = "SK08", songuoi = 2, noidi = "10.795682, 106.675471", noiden = "10.832169, 106.621990", diachidi = "163 Nguyễn Văn Trỗi, Phường 11, Phú Nhuận", diachiden = "Chùa Vĩnh Phước, Trường Chinh, Vinh Phuoc Pagoda, Quận 12", gioden = "08:00", ngayden = "20/03/2020", giove = "16:00", ngayve = "20/03/2020" }
            //};

            DiemKeTiep = new Command(DiemKeTiepAction);

            DsToaDoDiemDung = MapFunction.Decode(chuyenXe.DsDiemDung).ToList();
            TrangThaiChuyenXe = chuyenXe.TrangThai;
            if (TrangThaiChuyenXe == 1)        // đã đi lượt đi
                DsToaDoDiemDung.Reverse();

            _DanhSachDiemDung = new List<ThongTinDiemDung>();
            ListviewDiaChiRowHeight = 64;
            ChieuCaoListviewDiaChi = DsToaDoDiemDung.Count * ListviewDiaChiRowHeight;

            for (int i = 0; i < DsToaDoDiemDung.Count; i++)
            {
                //Substring(0, input.Length - 1)
                //var precision = 0;
                //var doubleLat = DsToaDoDiemDung[i].Latitude;
                //var doubleLng = DsToaDoDiemDung[i].Longitude;

                //while (doubleLat * Math.Pow(10, precision) != Math.Round(doubleLat * Math.Pow(10, precision)))
                //    precision++;
                //string lat = Math.Round(DsToaDoDiemDung[i].Latitude, precision - 1).ToString();

                //precision = 0;
                //while (doubleLng * Math.Pow(10, precision) != Math.Round(doubleLng * Math.Pow(10, precision)))
                //    precision++;
                //string lng = Math.Round(DsToaDoDiemDung[i].Longitude, precision - 1).ToString();

                string lat = DsToaDoDiemDung[i].Latitude.ToString().Substring(0, DsToaDoDiemDung[i].Latitude.ToString().Length - 1);
                string lng = DsToaDoDiemDung[i].Longitude.ToString().Substring(0, DsToaDoDiemDung[i].Longitude.ToString().Length - 1);

                _DanhSachDiemDung.Add(new ThongTinDiemDung { DsNguoiDi = new List<NguoiDi>() });


                //while (checkdata == true)
                //{
                    for (int j = 0; j < ChiTietChuyenDi.Count; j++)
                    {
                        var chitietchuyendi = ChiTietChuyenDi[j];

                        if (chitietchuyendi.ToaDoDi.Contains(lat) && chitietchuyendi.ToaDoDi.Contains(lng))
                        {
                            if (string.IsNullOrEmpty(_DanhSachDiemDung[i].DiaChi))
                            {
                                _DanhSachDiemDung[i].DiaChi = chitietchuyendi.NoiDi;
                                _DanhSachDiemDung[i].lat = lat;
                                _DanhSachDiemDung[i].lng = lng;
                                _DanhSachDiemDung[i].DaDiQua = false;
                            }
                            _DanhSachDiemDung[i].DsNguoiDi.Add(new NguoiDi
                            {
                                TenNguoiDi = chitietchuyendi.TenNguoiDi,
                                SoNguoiDi = chitietchuyendi.SoNguoiDi,
                                SdtLienLac = chitietchuyendi.SdtLienLac,
                                LenXe = TrangThaiChuyenXe == 0 ? true : false,
                                XuongXe = TrangThaiChuyenXe == 0 ? false : true
                            });
                        }
                        if (chitietchuyendi.ToaDoDen.Contains(lat) && chitietchuyendi.ToaDoDen.Contains(lng))
                        {
                            if (string.IsNullOrEmpty(_DanhSachDiemDung[i].DiaChi))
                            {

                                _DanhSachDiemDung[i].DiaChi = chitietchuyendi.NoiDen;
                                var l = Convert.ToDouble(lat);
                                _DanhSachDiemDung[i].lat = lat;
                                _DanhSachDiemDung[i].lng = lng;
                                _DanhSachDiemDung[i].DaDiQua = false;
                            }

                            _DanhSachDiemDung[i].DsNguoiDi.Add(new NguoiDi
                            {
                                TenNguoiDi = chitietchuyendi.TenNguoiDi,
                                SoNguoiDi = chitietchuyendi.SoNguoiDi,
                                SdtLienLac = chitietchuyendi.SdtLienLac,
                                LenXe = TrangThaiChuyenXe == 0 ? false : true,
                                XuongXe = TrangThaiChuyenXe == 0 ? true : false
                            });
                        }
                    //}
                }
            }
            indexToaDoKeTiep = 0;
            DaDiQuaDiaChiKeTiep = false;
            DiaChiKeTiep = DanhSachDiemDung[indexToaDoKeTiep].DiaChi;
            PositionKeTiep.Latitude = DsToaDoDiemDung[indexToaDoKeTiep].Latitude;
            PositionKeTiep.Longitude = DsToaDoDiemDung[indexToaDoKeTiep].Longitude;

            TheoDoi();
        }

        public async Task Laydulieu(Dieuxe chuyenXe)
        {
            var accessToken = Settings.AccessToken;
            GetChiTietLichDiAsync(" https://apidieuxe.azurewebsites.net/api/getlichdicuaTaixe?id=" + chuyenXe.MaDieuxe, accessToken);
            if (ChiTietChuyenDi != null)
            {
                checkdata = true;
            }
        }



        void DiemKeTiepAction(object obj)
        {
            //DanhSachDiemDung[0].DaDiQua = true;
            //DanhSachDiemDung = DanhSachDiemDung;
            DaDiQuaDiaChiKeTiep = true;
        }

        double? KcNhoNhat = null;
        double KcChapNhan = 3;     //mét
        bool DaDenDiaDiem = false;
        async void TheoDoi()
        {
            double ThoiGianCapNhat = 0;     //giây
            double KhoangCachCapNhat = 1;   //mét

            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                if (!hasPermission)
                    return;

                //if (tracking)
                //{
                //    CrossGeolocator.Current.PositionChanged -= CrossGeolocator_Current_PositionChanged;
                //    CrossGeolocator.Current.PositionError -= CrossGeolocator_Current_PositionError;
                //}
                //else
                //{
                    CrossGeolocator.Current.PositionChanged += CrossGeolocator_Current_PositionChanged;
                    CrossGeolocator.Current.PositionError += CrossGeolocator_Current_PositionError;
                //}

                if (CrossGeolocator.Current.IsListening)
                {
                    await CrossGeolocator.Current.StopListeningAsync();
                    //labelGPSTrack.Text = "Stopped tracking";
                    //ButtonTrack.Text = "Start Tracking";
                    //tracking = false;
                    //count = 0;
                }
                else
                {
                    //Positions.Clear();
                    //if(!CrossGeolocator.Current.IsListening)
                        if (await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(ThoiGianCapNhat), KhoangCachCapNhat, false,
                            new ListenerSettings
                            {
                                ActivityType = ActivityType.Other,
                                AllowBackgroundUpdates = true,
                                DeferLocationUpdates = true,
                                DeferralDistanceMeters = 1,
                                DeferralTime = TimeSpan.FromSeconds(0),
                                ListenForSignificantChanges = true,
                                PauseLocationUpdatesAutomatically = true
                            }))
                        {
                            //
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        void CrossGeolocator_Current_PositionChanged(object sender, PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var position = e.Position;
                //Positions.Add(position);
                //count++;
                //labelGPSTrack.Text = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                //    position.Timestamp, position.Latitude, position.Longitude,
                //    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);
                
                var khoangcach = 1e3* DBSCAN.haversine_distance(new Coordinate(position.Latitude, position.Longitude), new Coordinate(PositionKeTiep.Latitude, PositionKeTiep.Longitude));
                if (!KcNhoNhat.HasValue)
                    KcNhoNhat = khoangcach;
                KcNhoNhat = khoangcach < KcNhoNhat ? khoangcach : KcNhoNhat;
                if (DaDenDiaDiem)
                {
                    if (khoangcach > KcNhoNhat)
                        DaDiQuaDiaChiKeTiep = true;
                }
                else
                    if (KcNhoNhat <= KcChapNhan)
                        DaDenDiaDiem = !DaDenDiaDiem;
            });
        }

        void CrossGeolocator_Current_PositionError(object sender, PositionErrorEventArgs e)
        {
            Console.WriteLine("Location error: " + e.Error.ToString());
        }

        public string LayTuyenDuong()
        {
            return ChuyenXeHienTai.TrangThai == 0 ? ChuyenXeHienTai.TuyenDuongDi : ChuyenXeHienTai.TuyenDuongVe;
        }

        public async void GetChiTietLichDiAsync(string path, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            HttpResponseMessage response = await client.GetAsync(path);

            System.Diagnostics.Debug.WriteLine(response);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                ChiTietChuyenDi = JsonConvert.DeserializeObject<List<ChiTietChuyenDi>>(content);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("error", "không thể get lịch chi tiết 😞 ", "OK");
                ChiTietChuyenDi = null;
            }
        }




        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName.Equals("DaDiQuaDiaChiKeTiep"))
            {
                if (DaDiQuaDiaChiKeTiep && indexToaDoKeTiep < DanhSachDiemDung.Count)
                {
                    DanhSachDiemDung[indexToaDoKeTiep].DaDiQua = true;
                    indexToaDoKeTiep++;
                    KcNhoNhat = null;
                    if(indexToaDoKeTiep < DanhSachDiemDung.Count)
                    {
                        DiaChiKeTiep = DanhSachDiemDung[indexToaDoKeTiep].DiaChi;
                        DaDiQuaDiaChiKeTiep = false;

                        PositionKeTiep.Latitude = DsToaDoDiemDung[indexToaDoKeTiep].Latitude;
                        PositionKeTiep.Longitude = DsToaDoDiemDung[indexToaDoKeTiep].Longitude;
                    }
                    else
                    {
                        DiaChiKeTiep = "Đã đi hết điểm dừng";
                        CapNhatChuyenDi();
                        ChuaHoanThanhMoiDiemDung = false;
                    }
                }
            }
        }
        void CapNhatChuyenDi()
        {
            TrangThaiChuyenXe++;

            //cập nhật chuyến đi lên server
        }

        public void RaisePropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
            }
        }
        #endregion
    }

   
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace dieuxe.Models
{
    public class DangKyLichChiTiet:INotifyPropertyChanged
    {
        private int _DangKyLichChiTietId;
        private int _DangKyLichId;
        private string _TenNguoiDi;
        private string _SdtLienLac;
        private string _ThoiGianDen;
        private string _ThoiGianVe;
        private string _NgayDen;
        private string _GioDen;
        private string _NgayVe;
        private string _GioVe;
        private string _NoiDi;
        private string _NoiDen;
        private string _Ghichu;
        private string _NgayDangKy;
        private int _NguoiTao;
        private string _NgayCapNhat;
        private int _NguoiCapnhat;
        private string _ToaDoDi;
        private string _ToaDoDen;
        private int _SoNguoi;



        public int DangKyLichChiTietId 
        {
            get { return _DangKyLichChiTietId; }
            set
            {
                _DangKyLichChiTietId  = value;
                OnPropertyChanged();
            }
        }
        public int DangKyLichId
        {
            get { return _DangKyLichId; }
            set
            {
                _DangKyLichId = value;
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
        public string SdtLienLac
        {
            get { return _SdtLienLac; }
            set
            {
                _SdtLienLac = value;
                OnPropertyChanged();
            }
        }
        public string ThoiGianDen
        {
            get { return _ThoiGianDen; }
            set
            {
                _ThoiGianDen = value;
                OnPropertyChanged();
            }
        }
        public string ThoiGianVe
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
        public string GioDen
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
        public string GioVe
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
        public string NgayDangKy
        {
            get { return _NgayDangKy; }
            set
            {
                _NgayDangKy = value;
                OnPropertyChanged();
            }
        }
        public int NguoiTao
        {
            get { return _NguoiTao; }
            set
            {
                _NguoiTao = value;
                OnPropertyChanged();
            }
        }
        public string NgayCapNhat
        {
            get { return _NgayCapNhat; }
            set
            {
                _NgayCapNhat = value;
                OnPropertyChanged();
            }
        }
        public int NguoiCapnhat
        {
            get { return _NguoiCapnhat; }
            set
            {
                _NguoiCapnhat = value;
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
            get { return _SoNguoi; }
            set
            {
                _SoNguoi = value;
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace dieuxe.Models
{
    public class Dieuxe : INotifyPropertyChanged
    {
        public int MaDieuxe { get; set; }
        public string NoiDi { get; set; }
        public string NoiDen { get; set; }
        public string GioDi { get; set; }
        public string GioVe { get; set; }
        private int _TaiXe;
        public int TrangThai { get; set; }      //string -> int
        public string TuyenDuongDi { get; set; }        //modify
        public string TuyenDuongVe { get; set; }        //add
        public string GhiChu { get; set; }
        public string BienKiemSoat { get; set; }
        public decimal ChiPhi { get; set; }
        public string NgayDangKy { get; set; }
        public int NguoiTao { get; set; }
        public string NgayCapNhat { get; set; }
        public int NguoiCapnhat { get; set; }
        public string NgayDi { get; set; }
        public int SoNguoi { get; set; }
        public string DsDiemDung { get; set; }
        public List<int> listDangKyLichChiTietId { get; set; }
        int index;
        public int IndexDieuXe
        {
            get { return index; }
            set
            {
                index = value;
                OnPropertyChanged("IndexDieuXe");
            }
        }
        public List<LienHeUser> dsTaixe { get; set; }

        private LienHeUser _SelectItem;
        public LienHeUser SelectItem
        {
            get { return _SelectItem; }
            set
            {
                _SelectItem = value;
                TaiXe = _SelectItem.LienHeID;
                OnPropertyChanged();
            }
        }
        public int TaiXe
        {
            get { return _TaiXe; }
            set
            {
                _TaiXe = value;
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
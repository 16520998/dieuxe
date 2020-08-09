using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace dieuxe.Models
{
    public class ThongKe : INotifyPropertyChanged
    {
        private string _BienKiemSoat;
        private decimal _TongChiPhi;
        private int _TongNguoiDiTrongThang;
        private int _Thang;
        public int Thang
        {
            get { return _Thang; }
            set
            {
                _Thang = value;
                OnPropertyChanged();
            }
        }
        public string BienKiemSoat
        {
            get { return _BienKiemSoat; }
            set { _BienKiemSoat = value;
                OnPropertyChanged();
            }
        }
        public decimal TongChiPhi
        {
            get { return _TongChiPhi; }
            set
            {
                _TongChiPhi = value;
                OnPropertyChanged();
            }
        }
        public int TongNguoiDiTrongThang
        {
            get { return _TongNguoiDiTrongThang; }
            set
            {
                _TongNguoiDiTrongThang = value;
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

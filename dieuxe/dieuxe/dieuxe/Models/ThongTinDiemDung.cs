using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;

namespace dieuxe.Models
{
    public class ThongTinDiemDung : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        bool daDiQua;
        public string lat { get; set; }
        public string lng { get; set; }

        public Coordinate toado { get; set; }
        public string DiaChi { get; set; }
        public List<NguoiDi> DsNguoiDi { get; set; }
        public bool DaDiQua 
        { 
            get { return daDiQua; } 
            set 
            { 
                daDiQua = value;
                OnPropertyChanged("DaDiQua"); 
            }
        }

        //List<ChuyenXe> _pickerSource;
        //public List<ChuyenXe> pickerSource
        //{
        //    get { return _pickerSource; }
        //    set
        //    {
        //        _pickerSource = value;
        //        OnPropertyChanged("pickerSource");
        //    }
        //}
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
    }
    public class NguoiDi
    {
        public string TenNguoiDi { get; set; }
        public int SoNguoiDi { get; set; }
        public string SdtLienLac { get; set; }
        public bool LenXe { get; set; }
        public bool XuongXe { get; set; }
    }
}

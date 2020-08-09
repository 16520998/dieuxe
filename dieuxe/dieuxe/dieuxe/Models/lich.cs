using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace dieuxe.Models
{
    public class lich : INotifyPropertyChanged
    {
        
        public int sodangky { get; set; }
        public string ngaydangky { get; set; }
        public string nhanviendangky { get; set; }
        public int songuoi { get; set; }
        public string noidi { get; set; }
        public string noiden { get; set; }
        public string gioden { get; set; }
        public string giove { get; set; }
        public List<LienHeUser> dstaixe { get; set; }
        private int _Mataixe { get; set; }
        private LienHeUser _SelectItem;
        public LienHeUser SelectItem
        {
            get { return _SelectItem; }
            set { _SelectItem = value;
                Mataixe = _SelectItem.LienHeID;
                    OnPropertyChanged(); }
        }
        public int Mataixe
        {
            get { return _Mataixe; }
            set { _Mataixe = value; OnPropertyChanged(); }
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

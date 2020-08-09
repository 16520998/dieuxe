using dieuxe.Models;
using dieuxe.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dieuxe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ThongkeView : ContentPage,INotifyPropertyChanged
    {
        private int _chon;
        private bool _check;

        public bool check
        {
            get { return _check; }
            set
            {
                _check = value;
                OnPropertyChanged();
            }
        }
        public int chon
        {
            get { return _chon; }
            set
            {
                _chon = value;
                OnPropertyChanged();
            }
        }
        ThongKeViewModel thongke;
        public ThongkeView()
        {
            InitializeComponent();
            thongke = new ThongKeViewModel();
            
            string now = DateTime.Now.ToString("MM");
            string Nam = DateTime.Now.ToString("yyyy");
            nam.Text = Nam;
            var selectThang = new List<int>();
            selectThang.Add(01);
            selectThang.Add(02);
            selectThang.Add(03);
            selectThang.Add(04);
            selectThang.Add(05);
            selectThang.Add(06);
            selectThang.Add(07);
            selectThang.Add(08);
            selectThang.Add(09);
            selectThang.Add(10);
            selectThang.Add(11);
            selectThang.Add(12);
            chonthang.ItemsSource = selectThang;
            BindingContext = thongke;
            chonthang.SetBinding(Picker.SelectedItemProperty, "chon");
            chonthang.SelectedIndex = Convert.ToInt32(now)-1;
            
            //while (!thongke.check)
            //{

            //}
            //var ThongkeCuoiThang = thongke.ThongkeCuoiThang.Where(i => i.Thang.ToString().Contains(now));
            //listhongke.ItemsSource = ThongkeCuoiThang;
        }

        private void chonthang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = (Picker)sender;
            Debug.WriteLine(s.SelectedItem);
            string now = DateTime.Now.ToString("MM");
           
            if (thongke.check)
            {
                var ThongkeCuoiThang = thongke.ThongkeCuoiNam.Where(i => i.Thang == Convert.ToInt32(s.SelectedItem)).ToList();
                if (ThongkeCuoiThang.Count() == 0 || ThongkeCuoiThang == null)
                {
                    check = false;
                }
                else check = true;
                var TongNguoi = ThongkeCuoiThang.Sum(i => i.TongNguoiDiTrongThang);
                var TongChiPhi = ThongkeCuoiThang.Sum(i => i.TongChiPhi);
                thongke.Thanghientai = ThongkeCuoiThang;
                listhongke.ItemsSource = thongke.Thanghientai;
                tcp.Text = TongChiPhi.ToString();
                tn.Text = TongNguoi.ToString();


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

        private void xemtongclicked(object sender, EventArgs e)
        {
            if (thongke.ThongKeNam)
            {
                
                var TongNguoi = thongke.Thanghientai.Sum(i => i.TongNguoiDiTrongThang);
                var TongChiPhi = thongke.Thanghientai.Sum(i => i.TongChiPhi);
                tcp.Text = TongChiPhi.ToString();
                tn.Text = TongNguoi.ToString();
                listhongke.ItemsSource = thongke.Thanghientai;
            }
            else {
                listhongke.ItemsSource = thongke.ThongkeCuoiNam;
                var TongNguoi = thongke.ThongkeCuoiNam.Sum(i => i.TongNguoiDiTrongThang);
                var TongChiPhi = thongke.ThongkeCuoiNam.Sum(i => i.TongChiPhi);
                tcp.Text = TongChiPhi.ToString();
                tn.Text = TongNguoi.ToString();
                
            }
            thongke.ThongKeNam = !thongke.ThongKeNam;
        }
    }
}
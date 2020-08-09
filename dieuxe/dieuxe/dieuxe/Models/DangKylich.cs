using System;
using System.Collections.Generic;
using System.Text;

namespace dieuxe.Models
{
    public class DangKylich
    {
        public int DangKyLichId { get; set; }
        public string NgayDangky { get; set; }
        public int LienHeId { get; set; }
        public string SdtLienLac { get; set; }
        public int NguoiTao { get; set; }
        public string NgayCapNhat { get; set; }
        public int NguoiCapnhat { get; set; }
        public int SoNguoiDi { get; set; }
    }
}

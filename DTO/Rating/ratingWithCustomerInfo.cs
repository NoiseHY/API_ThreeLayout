using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Rating
{
    public class ratingWithCustomerInfo
    {
        public int MaDanhGia { get; set; }
        public int MaSP { get; set; }
        public int MaTK { get; set; }
        public float DanhGia { get; set; }
        public string BinhLuan { get; set; }
        public DateTime ThoiGian { get; set; }

        public int MaKH { get; set; }
        public string UserName { get; set; }
        public string DiachiKH { get; set; }
        public string Sdt { get; set; }
        public DateTime Ngaysinh { get; set; }
        public string UserImg { get; set; }
    }
}

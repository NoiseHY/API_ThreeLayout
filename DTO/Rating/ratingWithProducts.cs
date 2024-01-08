using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Rating
{
    public class ratingWithProducts
    {
        public int MaDanhGia { get; set; }
        public int MaSP { get; set; }
        public int MaTK { get; set; }
        public float DanhGia { get; set; }
        public string BinhLuan { get; set; }
        public DateTime ThoiGian { get; set; }
        public string TenSanPham { get; set; }
        public string AnhSanPham { get; set; }
    }
}

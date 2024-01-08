using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Admin
{
    public class product
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; } = null!;
        public string? Mota { get; set; }
        public int SoLuong { get; set; }
        public decimal Dongia { get; set; }
        public int? MaTL { get; set; }
        public string? Img { get; set; }
    }
}

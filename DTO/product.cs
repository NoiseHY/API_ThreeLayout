using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class product
    {
        public int MaSp { get; set; }
        public string TenSp { get; set; } = null!;
        public string? Mota { get; set; }
        public int SoLuong { get; set; }
        public decimal Dongia { get; set; }
        public int MaTl { get; set; }
    }
}

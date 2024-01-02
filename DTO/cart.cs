using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class cart
    {
        public int MaGiohang {  get; set; }
        public int MaKH { get; set; }
        public int MaSP { get; set; }
        public int Soluong { get; set; }
        public decimal Dongia { get; set; }
        public decimal Thanhtien { get; set; }
        public DateTime Thoidiemtao { get; set; }
    }
}

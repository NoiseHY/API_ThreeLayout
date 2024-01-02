using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class bill
    {
        public decimal Tongtien { get; set; }
        public int MaKH {  get; set; }
        public DateTime Ngayban {  get; set; }
        

        public int MaSP { get; set; }
        public int Soluong { get; set; }
        public decimal Gia { get; set; }
        public decimal Thanhtien { get; set; }

    }
}

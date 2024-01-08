using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
    public class bill
    {
        public int MaHDB { get; set; }
        public decimal Tongtien { get; set; }
        public int MaKH { get; set; }
        public DateTime Ngayban { get; set; }


        public int MaSP { get; set; }
        public string? ProductName { get; set; }
        public int Soluong { get; set; }
        public decimal Gia { get; set; }
        public decimal Thanhtien { get; set; }

    }
}

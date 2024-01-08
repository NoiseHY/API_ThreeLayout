using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
    public class cart
    {
        public int MaGiohang { get; set; }
        public int MaKH { get; set; }
        public int MaSP { get; set; }
        public decimal Dongia { get; set; }
        public DateTime Thoidiemtao { get; set; }
        public string? ProductName { get; set; }
        public string? ProductImg { get; set; }

    }
}

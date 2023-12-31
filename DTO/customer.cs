using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class customer
    {
        public int MaKH { get; set; }
        public string TenKH { get; set; } = null!;
        public string? DiachiKH { get; set; }
        public string? Sdt { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public string? Img {  get; set; }
    }
}

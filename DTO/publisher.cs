using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class publisher
    {
        public int MaNcc { get; set; }
        public string TenNcc { get; set; } = null!;
        public string? Diachi { get; set; }
        public string? Sdt { get; set; }
    }
}

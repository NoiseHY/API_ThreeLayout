using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class staff
    {
        public int MaNv { get; set; }
        public string TenNv { get; set; } = null!;
        public DateTime? Ngaysinh { get; set; }
        public string? Gioitinh { get; set; }
        public string? Sdt { get; set; }
        public string? Diachi { get; set; }
    }
}

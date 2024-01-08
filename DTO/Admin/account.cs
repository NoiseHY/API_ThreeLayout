using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Admin
{
    public class account
    {
        public int MaTK { get; set; }
        public string TenTK { get; set; } = null!;
        public string MkTK { get; set; } = null!;
        public string? Email { get; set; }
        public int? MaPQ { get; set; }
        public int? MaKH { get; set; }
        public int? MaNV { get; set; }
    }
}

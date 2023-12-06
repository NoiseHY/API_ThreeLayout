using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class account
    {
        public int MaTk { get; set; }
        public string TenTk { get; set; } = null!;
        public string MkTk { get; set; } = null!;
        public string? Email { get; set; }
        public int? MaPq { get; set; }
        public int? MaKh { get; set; }
        public int? MaNv { get; set; }
    }
}

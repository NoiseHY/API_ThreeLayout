using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    internal class user_authorization
    {
        public int MaPq { get; set; }
        public string TenPq { get; set; } = null!;
        public string? Mota { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO.Admin
{
    public class user_authorization
    {
        public int MaPQ { get; set; }
        public string TenPQ { get; set; } = null!;
        public string? Mota { get; set; }
    }
}

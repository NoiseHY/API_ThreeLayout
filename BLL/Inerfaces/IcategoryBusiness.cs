using DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Inerfaces
{
    public partial interface IcategoryBusiness
    {
        List<category> GetCategories();
        List<category> GetNameCategories(int id);
    }
}

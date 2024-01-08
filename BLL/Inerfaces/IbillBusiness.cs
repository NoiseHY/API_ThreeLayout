using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Inerfaces
{
    public partial interface IbillBusiness
    {
        bool CreateTemp(List<bill> bill);
        bool Create(bill bill);
        List<bill> GetAllCategory(int id);
        List<bill> GetAllCategoryInfo(int id);
    }
}

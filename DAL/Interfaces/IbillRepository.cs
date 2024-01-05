using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public partial interface IbillRepository
    {
        bool Create(bill bill);
        bool CreateTemp(List<bill> bill);
        List<bill> GetAllCategory(int id);
        List<bill> GetAllCategoryInfo(int id);
    }
}

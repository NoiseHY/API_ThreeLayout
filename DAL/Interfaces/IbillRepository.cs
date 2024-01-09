using DTO.User;
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
        List<bill> GetAllBill(int id);
        List<bill> GetAllBillInfo(int id);
        List<bill> GetAllBillByCustomerID(int id);
        bool Delete(int id);
    }
}

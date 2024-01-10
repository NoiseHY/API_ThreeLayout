using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Interfaces
{
    public partial interface IcustomerRepository
    {
        customer GetCustomerByID(int id);
        List<customer> GetAll(int pageNumber, int pageSize);
        bool UpdateImageFilePath(int customerId, string imagePath);
        bool Create(customer customer);
        bool Update(customer customer);
        bool Delete(int id);
    }
}

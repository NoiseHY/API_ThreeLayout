using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Inerfaces
{
    public interface IcustomerBusiness 
    {
        customer GetCustomerByID(int id);
        List<customer> GetAll(int pageNumber, int pageSize);
        bool Create(customer customer);
        bool Update(customer customer);
        bool Delete(int id);
        bool UpdateImageFilePath(int customerId, string imagePath);
    }
}

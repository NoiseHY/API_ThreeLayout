using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Inerfaces
{
    public partial interface IcartBusiness
    {
        List<cart> GetAll(int id);
        bool Create(cart cart);
        bool Update(cart cart);
        bool Delete(int id);
        int Count(int id);
        bool CheckProductInCart(int id, int productId);
    }
}

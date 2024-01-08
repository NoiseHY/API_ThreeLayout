using DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Inerfaces
{
    public partial interface IproductBusiness
    {
        product GetProductByID(int id);
        List<product> GetAll(int pageNumber, int pageSize);
        List<product> GetNewProducts(int pageNumber, int pageSize);
        List<product> SearchProductByName(string Name, int pageNumber, int pageSize);
        bool Create(product product);
        bool Update(product product);
        bool Delete(int id);
        bool UpdateImageFilePath(int productId, string imagePath);
    }
}

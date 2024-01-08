using DTO.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Inerfaces
{
    public partial interface IratingBusiness
    {
        List<rating> GetAll(int pageNumber, int pageSize);
        List<ratingWithCustomerInfo> GetAllRatingCmt(int id);
        List<ratingWithProducts> GetCommentsByMaTK(int id);
        bool Create(rating rating);
        bool Update(rating rating);
        bool Delete(int id);
    }
}

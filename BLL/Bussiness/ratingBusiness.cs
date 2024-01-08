using BLL.Inerfaces;
using DAL.Interfaces;
using DTO.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Bussiness
{
    public partial class ratingBussiness : IratingBusiness
    {
        private IratingRepository _iratingRepository;
        public ratingBussiness(IratingRepository iratingRepository)
        {
            _iratingRepository = iratingRepository;
        }
        public List<ratingWithCustomerInfo> GetAllRatingCmt(int id)
        {
            return _iratingRepository.GetAllRatingCmt( id);
        }
        public List<rating> GetAll(int pageNumber, int pageSize)
        {
            return _iratingRepository.GetAll(pageNumber, pageSize);
        }
        public List<ratingWithProducts> GetCommentsByMaTK(int id)
        {
            return _iratingRepository.GetCommentsByMaTK(id);
        }
        public bool Create(rating rating)
        {
            return _iratingRepository.Create(rating);
        }
        public bool Update(rating rating)
        {
            return _iratingRepository.Update(rating);
        }   
        public bool Delete(int id)
        {
            return _iratingRepository.Delete(id);
        }
    }
}

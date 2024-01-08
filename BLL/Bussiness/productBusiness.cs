using BLL.Inerfaces;
using DAL.Interfaces;
using DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Bussiness
{
    public partial class productBusiness :IproductBusiness
    {
        private IproductRepository iproductRepository;
        public productBusiness(IproductRepository _iproductRepository)
        {
            iproductRepository = _iproductRepository;
        }
        public product GetProductByID(int id)
        {
            return iproductRepository.GetProductByID(id);
        }
        public List<product> GetAll(int pageNumber, int pageSize)
        {
            return iproductRepository.GetAll(pageNumber, pageSize);
        }
        public List<product> GetNewProducts(int pageNumber, int pageSize)
        {
            return iproductRepository.GetNewProducts(pageNumber, pageSize);
        }
        public List<product> SearchProductByName(string Name, int pageNumber, int pageSize)
        {
            return iproductRepository.SearchProductByName(Name, pageNumber, pageSize);
        }
        public bool Create(product product)
        {
            return iproductRepository.Create(product);
        }
        public bool Update(product product)
        {
            return iproductRepository.Update(product);
        }
        public bool Delete(int id)
        {
            return iproductRepository.Delete(id);
        }
        public bool UpdateImageFilePath(int productId, string imagePath)
        {
            return iproductRepository.UpdateImageFilePath(productId, imagePath);
        }
    }
}

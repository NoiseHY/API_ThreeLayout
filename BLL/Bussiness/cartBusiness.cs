using BLL.Inerfaces;
using DAL.Interfaces;
using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Bussiness
{
    public partial class cartBusiness:IcartBusiness
    {
        private IcartRepository _icartRepository;
        public cartBusiness (IcartRepository icartRepository)
        {
            _icartRepository = icartRepository;
        }
        public List<cart> GetAll(int id)
        {
            return _icartRepository.GetAll(id);
        }
        public bool Create(cart cart)
        {
            return _icartRepository.Create(cart);
        }
        public bool Update(cart cart)
        {
            return _icartRepository.Update(cart);
        }
        public bool Delete(int id)
        {
            return _icartRepository.Delete(id);
        }
        public int Count(int id)
        {
            return _icartRepository.Count(id);
        }

        public bool CheckProductInCart(int id, int productId)
        {
            return _icartRepository.CheckProductInCart(id, productId);
        }
    }
}

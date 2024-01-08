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
    public class billBusiness : IbillBusiness
    {
        private IbillRepository _ibillRepository;
        public billBusiness(IbillRepository ibillRepository)
        {
            _ibillRepository = ibillRepository;
        }
        public bool Create(bill bill)
        {
            return _ibillRepository.Create(bill);
        }
        public bool CreateTemp(List<bill> bill)
        {
            return _ibillRepository.CreateTemp(bill);
        }
        public List<bill> GetAllCategoryInfo(int id)
        {
            return _ibillRepository.GetAllCategoryInfo(id);
        }
        public List<bill> GetAllCategory(int id)
        {
            return _ibillRepository.GetAllCategory(id);
        }
    }
}

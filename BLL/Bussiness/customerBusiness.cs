using BLL.Inerfaces;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Bussiness
{
    public partial class customerBusiness:IcustomerBusiness
    {
        private IcustomerRepository icustomerRepository;
        public customerBusiness(IcustomerRepository _icustomerRepository)
        {
            icustomerRepository = _icustomerRepository;
        }
        public bool UpdateImageFilePath(int customerId, string imagePath)
        {
            return icustomerRepository.UpdateImageFilePath(customerId, imagePath);
        }
        public customer GetCustomerByID(int id)
        {
            return icustomerRepository.GetCustomerByID(id);
        }
        public List<customer> GetAll(int pageNumber, int pageSize)
        {
            return icustomerRepository.GetAll(pageNumber, pageSize);
        }
        public bool Create(customer customer)
        {
            return icustomerRepository.Create(customer);
        }
        public bool Update(customer customer)
        {
            return icustomerRepository.Update(customer);
        }
        public bool Delete(int id)
        {
            return icustomerRepository.Delete(id);
        }
    }
}

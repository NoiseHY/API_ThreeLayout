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
    public class accountBusiness : IaccountBusiness
    {
        private IaccountRepository _accountRepository;
        public accountBusiness(IaccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public List<account> GetAll(int pageNumber, int pageSize)
        {
            return _accountRepository.GetAll(pageNumber, pageSize);
        }
        public bool Create(account account)
        {
            return _accountRepository.Create(account);
        }
        public bool Update(account account)
        {
            return _accountRepository.Update(account);
        }
        public bool Delete(int id)
        {
            return _accountRepository.Delete(id);
        }
    }
}

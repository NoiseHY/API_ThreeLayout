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
    public class loginBusiness : ILoginBusiness
    {
        private ILoginRepository _loginRepository;
        public loginBusiness (ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }
        public account Login(string username, string password)
        {
            return _loginRepository.Login(username, password);
        }
    }
}

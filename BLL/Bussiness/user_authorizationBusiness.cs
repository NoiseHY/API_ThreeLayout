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
    public class user_authorizationBusiness : Iuser_authorizationBusiness
    {
        private Iuser_authorizationRepository _iuser_AuthorizationRepository;
        public user_authorizationBusiness(Iuser_authorizationRepository iuser_AuthorizationRepository)
        {
            _iuser_AuthorizationRepository = iuser_AuthorizationRepository;
        }

        public List<user_authorization> Getuser_authorization()
        {
            return _iuser_AuthorizationRepository.Getuser_authorization();
        }
    }
}

using DAL.Helper;
using DAL.Interfaces;
using DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class user_authorizationRepository :Iuser_authorizationRepository
    {
        private ExcuteProcedure _excuteProcedure;
        public user_authorizationRepository(ExcuteProcedure excuteProcedure)
        {
            _excuteProcedure = excuteProcedure;
        }

        public List<user_authorization> Getuser_authorization()
        {
            string msg = "";
            try
            {
                var category = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msg, "GetAllPermissionNames");
                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);
                return category.ConvertTo<user_authorization>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

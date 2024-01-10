using DAL.Helper;
using DAL.Interfaces;
using DTO;
using DTO.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public partial class accountRepository : IaccountRepository
    {
        public ExcuteProcedure _excuteProcedure;
        public accountRepository(ExcuteProcedure excuteProcedure)
        {
            _excuteProcedure = excuteProcedure;
        }
        public List<account> GetAll(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msgError, "GetAccountsPaged",
                    "@PageNumber", pageNumber,
                    "@PageSize", pageSize);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<account>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(account account)
        {
            string msgError = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(
                    out msgError, "AddAccount",
                    "@TenTK", account.TenTK,
                    "@MkTK", account.MkTK,
                    "@Email", account.Email,
                    "@MaPQ", account.MaPQ);

                if (result != null || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Update(account account)
        {
            string msgError = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(out msgError, "UpdateAccount",
                 "@MaTK", account.MaTK,
                 "@TenTK", account.TenTK,
                 "@MkTK", account.MkTK,
                 "@Email", account.Email,
                 "@MaPQ", account.MaPQ);

                if (result != null || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public bool Delete(int id)
        {
            string msgError = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(out msgError, "DeleteAccount",
                    "@MaTK", id);

                if (result != null || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

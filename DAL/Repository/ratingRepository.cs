using DAL.Helper;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ratingRepository : IratingRepository
    {
        private ExcuteProcedure _excuteProcedure;
        public ratingRepository (ExcuteProcedure excuteProcedure)
        {
            _excuteProcedure = excuteProcedure;
        }
        public List<rating> GetAll()
        {
            string msgError = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msgError, "LayTatCaKhachHang");
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<rating>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

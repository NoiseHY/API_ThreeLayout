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
    public class categoryRepository : IcategoryRepository
    {
        private ExcuteProcedure _excuteProcedure;
        public categoryRepository (ExcuteProcedure excutrProcedure)
        {
            _excuteProcedure = excutrProcedure;
        }

        public List<category> GetCategories()
        {
            string msg = "";
            try
            {
                var category = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msg, "GetAllLoaiSP");
                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);
                return category.ConvertTo<category>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<category> GetNameCategories(int id)
        {
            string msg = "";
            try
            {
                var category = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msg, "GetTenLoaiByMaLoai",
                    "@MaLoai", id);
                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);
                return category.ConvertTo<category>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

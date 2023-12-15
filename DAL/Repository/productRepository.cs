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
    public class productRepository : IproductRepository
    {
        private ExcuteProcedure _excuteProcedure;
        public productRepository (ExcuteProcedure excuteProcedure)
        {
            _excuteProcedure = excuteProcedure;
        }
      

        public product GetProductByID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msgError, "GetProductById",
                     "@ProductId", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<product>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<product> GetAll(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msgError, "GetPaginatedProducts",
                    "@PageNumber", pageNumber,
                    "@PageSize", pageSize);

                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);

                return dt.ConvertTo<product>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Create(product product)
        {
            string msgError = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(
                    out msgError, "AddProduct",
                    "@TenSP", product.TenSP,
                    "@Mota", product.Mota,
                    "@SoLuong", product.SoLuong,
                    "@Dongia", product.Dongia,
                    "@MaTL", product.MaTL,
                    "@Img", product.Img);

                if (result != null || !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(Convert.ToString(result) + msgError);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool Update(product product)
        {
            string msgError = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(
                     out msgError, "AddProduct",
                     "@MaSP",product.MaSP,
                     "@TenSP", product.TenSP,
                     "@Mota", product.Mota,
                     "@SoLuong", product.SoLuong,
                     "@Dongia", product.Dongia,
                     "@MaTL", product.MaTL,
                     "@Img", product.Img);

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
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(out msgError, "DeleteProduct",
                    "@MaSP", id);

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

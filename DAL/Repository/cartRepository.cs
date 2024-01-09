using DAL.Helper;
using DAL.Interfaces;
using DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public partial class cartRepository : IcartRepository
    {
        public ExcuteProcedure _excuteProcedure;
        public cartRepository(ExcuteProcedure excuteProcedure)
        {
            _excuteProcedure = excuteProcedure;
        }
        public List<cart> GetAll(int id)
        {
            string msgError = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msgError, "GetCartWithProductImgAndNameByCustomerId",
                     "@CustomerId", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<cart>().ToList();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Create(cart cart)
        {
            string msgError = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(
                    out msgError, "AddToCart",
                    "@CustomerId", cart.MaKH,
                    "@ProductId", cart.MaSP,
                    "@UnitPrice", cart.Dongia);

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
        public bool Update(cart cart)
        {
            string msgError = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(
                    out msgError, "UpdateCartItem",
                    "@CartId", cart.MaGiohang);

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
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(out msgError, "RemoveFromCart",
                    "@CartId", id);

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

        public int Count(int id)
        {
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(out string msgError, "CountProductsInCartByCustomerId",
                    "@CustomerId", id);

                if (result == null && !string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }


                if (int.TryParse(result.ToString(), out int count))
                {
                    return count;
                }

                throw new Exception("Lỗi !");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckProductInCart(int id, int productId)
        {
            string msgError = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(
                    out msgError, "CheckProductInCart",
                    "@MaKH", id,
                    "@MaSP", productId);


                if (result != null && (bool)result)
                {
                    
                    return true;
                }

                
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }
}

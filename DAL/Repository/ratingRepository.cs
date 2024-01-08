using DAL.Helper;
using DAL.Interfaces;
using DTO.Rating;
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
        public List<ratingWithProducts> GetCommentsByMaTK(int id)
        {
            string msg = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msg, "GetCommentsByMaTK",
                     "@MaTK", id);
                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);

                return dt.ConvertTo<ratingWithProducts>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<rating> GetAll(int pageNumber, int pageSize)
        {
            string msg = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msg, "PaginateRatings",
                    "@PageNumber", pageNumber,
                    "@PageSize", pageSize);

                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);

                return dt.ConvertTo<rating>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ratingWithCustomerInfo> GetAllRatingCmt(int id)
        {
            string msg = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msg, "GetReviewsWithUserInfoByProductId",
                     "@ProductId", id);
                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);

                return dt.ConvertTo<ratingWithCustomerInfo>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(rating rating)
        {
            string msg = "";
            try
            {
                var productId = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(
                    out msg, "InsertRating",
                    "@MaSP", rating.MaSP,
                    "@MaTK", rating.MaTK,
                    "@DanhGia", rating.DanhGia,
                    "@BinhLuan", rating.BinhLuan);

                if (productId != null || !string.IsNullOrEmpty(msg))
                {
                    throw new Exception(Convert.ToString(productId) + msg);
                }


                return true;
            }
            catch
            {
                return false;
            }

        }

        public bool Update(rating rating)
        {
            string msg = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(
                     out msg, "UpdateRating",
                     "@MaDanhGia", rating.MaDanhGia,
                     "@DanhGia", rating.DanhGia,
                     "@BinhLuan", rating.BinhLuan);

                if (result != null || !string.IsNullOrEmpty(msg))
                {
                    throw new Exception(Convert.ToString(result) + msg);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            string msg = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(out msg, "DeleteRating",
                    "@MaDanhGia", id);

                if (result != null || !string.IsNullOrEmpty(msg))
                {
                    throw new Exception(Convert.ToString(result) + msg);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

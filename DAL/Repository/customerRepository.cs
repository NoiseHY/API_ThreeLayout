using DAL.Helper;
using DAL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAL.Repository
{
    public class customerRepository : IcustomerRepository
    {
        private ExcuteProcedure _excuteProcedure;
        public customerRepository(ExcuteProcedure excuteProcedure)
        {
            _excuteProcedure = excuteProcedure;
        }

        public bool UpdateImageFilePath(int customerId, string imagePath)
        {
            string msg = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msg, "UpdateCustomerImage",
                     "@MaKH", customerId,
                     "@NewImg", imagePath);
                if (!string.IsNullOrEmpty(msg))
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<customer> GetAll(int pageNumber, int pageSize)
        {
            string msgError = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msgError, "GetCustomersPaged",
                    "@PageNumber", pageNumber,
                    "@PageSize", pageSize);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<customer>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public customer GetCustomerByID(int id)
        {
            string msgError = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msgError, "LayKhachHangTheoMa",
                     "@MaKH", id);
                if (!string.IsNullOrEmpty(msgError))
                    throw new Exception(msgError);
                return dt.ConvertTo<customer>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public bool Create(customer  customer)
        {
            string msgError = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(
                    out msgError, "ThemKhachHang",
                    "@TenKH", customer.TenKH,
                    "@DiaChi", customer.DiachiKH,
                    "@SDT", customer.Sdt,
                    "@NgaySinh", customer.Ngaysinh);

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


        public bool Update(customer customer)
        {
            string msgError = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(out msgError, "SuaKhachHang",
                "@MaKh", customer.MaKH,
                "@TenKH", customer.TenKH,
                "@DiaChi", customer.DiachiKH,
                "@SDT", customer.Sdt,
                "@NgaySinh", customer.Ngaysinh);

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
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(out msgError, "XoaKhachHang",
                    "@MaKH", id);

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

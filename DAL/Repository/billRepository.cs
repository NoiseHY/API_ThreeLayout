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
    public class billRepository : IbillRepository
    {
        public ExcuteProcedure _excuteProcedure;
        public billRepository(ExcuteProcedure excuteProcedure)
        {
            _excuteProcedure = excuteProcedure;
        }

        public bool CreateTemp(List<bill> bills)
        {
            string msgError = "";
            try
            {
                foreach (var bill in bills)
                {
                    var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(
                        out msgError, "AddTempChiTietHDBan",
                        "@MaSP", bill.MaSP,
                        "@SoLuong", bill.Soluong,
                        "@Gia", bill.Gia,
                        "@ThanhTien", bill.Thanhtien);

                    if (result != null || !string.IsNullOrEmpty(msgError))
                    {
                        throw new Exception(Convert.ToString(result) + msgError);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Create(bill bill)
        {
            string msgError = "";
            try
            {
                var result = _excuteProcedure.ExecuteScalarSProcedureWithTransaction(
                    out msgError, "AddHoaDonBan_ChiTiet",
                    "@TongTien", bill.Tongtien,
                    "@MaKH", bill.MaKH,
                    "@NgayBan", bill.Ngayban);

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

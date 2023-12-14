using DAL.Helper;
using DAL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace DAL.Repository
{
    public class loginRepository : ILoginRepository
    {
        private ExcuteProcedure _excuteProcedure;
        private readonly AppSettings _appSettings;

        public loginRepository(ExcuteProcedure excuteProcedure, IOptions<AppSettings> appSettings)
        {
            _excuteProcedure = excuteProcedure;
            _appSettings = appSettings.Value;
        }
        public account Login(string username, string password)
        {
            account accountInfo = new account();
            string msg = "";

            try
            {
                var login = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msg, "GetTaiKhoanInfo",
                    "@TenTK", username,
                    "@MkTK", password);

                if (login != null && login.Rows.Count > 0 && string.IsNullOrEmpty(msg))
                {
                    var user = login.Rows[0];

                    accountInfo.MaTk = (int)user["MaTK"];
                    accountInfo.TenTk = user["TenTK"].ToString();
                    accountInfo.MkTk = user["MkTK"].ToString();
                    //accountInfo.Email = user["Email"] != DBNull.Value ? user["Email"].ToString() : null;
                    //accountInfo.MaPq = user["MaPQ"] != DBNull.Value ? (int)user["MaPQ"] : (int?)null;
                    //accountInfo.MaKh = user["MaKH"] != DBNull.Value ? (int)user["MaKH"] : (int?)null;
                    //accountInfo.MaNv = user["MaNV"] != DBNull.Value ? (int)user["MaNV"] : (int?)null;
                }

                else
                {
                    
                    accountInfo = null;
                }
            }
            catch (Exception e)
            {
                // Xử lý ngoại lệ nếu cần
                msg = e.Message;
                accountInfo = null;
            }

            return accountInfo;
        }

    }
}

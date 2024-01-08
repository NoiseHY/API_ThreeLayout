using DAL.Helper;
using DAL.Interfaces;
using DTO.Admin;
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

                    accountInfo.MaTK = (int)user["MaTK"];
                    accountInfo.TenTK = user["TenTK"].ToString();
                    accountInfo.MkTK = user["MkTK"].ToString();
                    accountInfo.MaPQ = Convert.ToInt32(user["MaPQ"].ToString());
                    if (string.IsNullOrEmpty(user["MaKH"].ToString()))
                    {
                        // Xử lý khi MaKH rỗng
                    }
                    else
                    {
                        accountInfo.MaKH = Convert.ToInt32(user["MaKH"].ToString());
                    }


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

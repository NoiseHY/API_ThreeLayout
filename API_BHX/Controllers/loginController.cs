
using BLL.Bussiness;
using BLL.Inerfaces;
using DAL.Helper;
using DAL.Interfaces;
using DTO.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace API_BHX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class loginController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly ILoginBusiness _loginBusiness;

        public loginController(IOptions<AppSettings> appSettings, ILoginBusiness loginBusiness)
        {
            _appSettings = appSettings.Value;
            _loginBusiness = loginBusiness;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] account loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.TenTK) || string.IsNullOrEmpty(loginRequest.MkTK))
            {
                return BadRequest(new { error = "Vui lòng cung cấp tên đăng nhập và mật khẩu." });
            }

            var accountInfo = _loginBusiness.Login(loginRequest.TenTK, loginRequest.MkTK);

            if (accountInfo != null)
            {
                if (accountInfo.MaPQ == 1)
                {
                    var token = GenerateJwtToken(accountInfo);
                    return Ok(new { MaTK = accountInfo.MaTK, TenTK = accountInfo.TenTK, Token = token, MaPQ = accountInfo.MaPQ  });
                }
                else
                {
                    return Ok(new { MaTK = accountInfo.MaTK, TenTK = accountInfo.TenTK ,MaKH = accountInfo.MaKH });
                }
            }
            else
            {
                return BadRequest(new { error = "Tài khoản hoặc mật khẩu không đúng." });
            }
        }

        private string GenerateJwtToken(account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.TenTK),
                    new Claim(ClaimTypes.Role, account.MaPQ.ToString())

                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

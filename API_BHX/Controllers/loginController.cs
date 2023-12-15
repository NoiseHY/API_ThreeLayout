
using BLL.Bussiness;
using BLL.Inerfaces;
using DAL.Helper;
using DAL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        public IActionResult Login(string username, string password)
        {
            var accountInfo = _loginBusiness.Login(username, password);

            if (accountInfo != null)
            {
                // Nếu đăng nhập thành công, tạo và trả về JWT
                var token = GenerateJwtToken(accountInfo);
                return Ok(new { MaTK = accountInfo.MaTk, TenTK = accountInfo.TenTk, Token = token });
            }
            else
            {
                // Nếu đăng nhập không thành công, trả về thông báo lỗi
                return BadRequest(new { error = "Tài khoản hoặc mật khẩu sai " });
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
                    new Claim(ClaimTypes.Name, account.TenTk),
                    new Claim(ClaimTypes.Role, account.MaPq.ToString())
                    // Thêm các thông tin khác nếu cần
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

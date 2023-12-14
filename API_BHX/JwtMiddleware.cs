using DAL.Helper;
using DTO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;


namespace API_BHX_Gateway
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;
        private ExcuteProcedure _excuteProcedure;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appsettings, IConfiguration configuration, ExcuteProcedure databaseHelper)
        {
            _next = next;
            _appSettings = appsettings.Value;
            _excuteProcedure = databaseHelper;
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Expose-Headers", "*");
            if (!context.Request.Path.Equals("/api/token", StringComparison.Ordinal))
            {
                return _next(context);
            }
            if (context.Request.Method.Equals("POST") && context.Request.HasFormContentType)
            {
                return GenerateToken(context);
            }
            context.Response.StatusCode = 400;
            return context.Response.WriteAsync("Bad request.");
        }


        public async Task GenerateToken(HttpContext context)
        {
            string msg = "";
            try
            {
                var dt = _excuteProcedure.ExecuteSProcedureReturnDataTable(out msg, "GetTaiKhoanInfo");
                if (!string.IsNullOrEmpty(msg))
                    throw new Exception(msg);
                if (dt == null || dt.Rows.Count == 0) // Kiểm tra nếu không có dữ liệu trả về từ stored procedure
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var result = JsonConvert.SerializeObject(new { code = (int)HttpStatusCode.BadRequest, error = "Tài khoản hoặc mật khẩu không đúng" });
                    await context.Response.WriteAsync(result);
                    return;
                }

                var users = dt.AsEnumerable().Where(row => row.Field<string>("MaPQ") == "1").ToList();

                var user = users.FirstOrDefault();


                if (user == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    var result = JsonConvert.SerializeObject(new { code = (int)HttpStatusCode.Forbidden, error = "Không có quyền truy cập" });
                    await context.Response.WriteAsync(result);
                    return;
                }

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, user["TenTK"].ToString()), // Sử dụng thông tin tài khoản từ dữ liệu trả về
                new Claim(ClaimTypes.Role, user["MaPQ"].ToString()),
                new Claim(ClaimTypes.DenyOnlyWindowsDeviceGroup, user["MkTK"].ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var tmp = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(tmp);
                var response = new { MaTaiKhoan = user["MaTK"],Token = token };
                var serializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response, serializerSettings));
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

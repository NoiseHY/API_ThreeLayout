using BLL.Bussiness;
using BLL.Inerfaces;
using DTO.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_BHX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class user_authorizationController : ControllerBase
    {
        private Iuser_authorizationBusiness _iuser_AuthorizationBusiness;
        public user_authorizationController(Iuser_authorizationBusiness iuser_AuthorizationBusiness)
        {
            _iuser_AuthorizationBusiness = iuser_AuthorizationBusiness;
        }

        [Route("GetAll")]
        [HttpGet]
        public List<user_authorization> GetAll()
        {
            return _iuser_AuthorizationBusiness.Getuser_authorization();
        }
    }
}

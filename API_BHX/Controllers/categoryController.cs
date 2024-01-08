using BLL.Bussiness;
using BLL.Inerfaces;
using DTO.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_BHX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoryController : ControllerBase
    {
        private IcategoryBusiness _icategoryBusiness;
        public categoryController(IcategoryBusiness icategoryBusiness)
        {
            _icategoryBusiness = icategoryBusiness;
        }

        [Route("GetAll")]
        [HttpGet]
        public List<category> GetAll()
        {
            return _icategoryBusiness.GetCategories();
        }

        [Route("GetNameCategoryByID")]
        [HttpGet]
        public List<category> GetNameCategories(int id)
        {
            return (_icategoryBusiness.GetNameCategories(id));
        }
    }
}

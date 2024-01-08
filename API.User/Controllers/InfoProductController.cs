using BLL.Inerfaces;
using DTO.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoProductController : ControllerBase
    {
        private IproductBusiness _iproductBusiness;

        public InfoProductController(IproductBusiness iproductBusiness)
        {
            _iproductBusiness = iproductBusiness;
        }


        [Route("SearchProductByName")]
        [HttpGet]
        public IActionResult SearchProductByName(string Name, int pageNumber, int pageSize)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Không thể tạo !!");
            }

            if (Name == null)
            {
                return Ok("Không có sản phẩm !");
            }

            var products = _iproductBusiness.SearchProductByName(Name, pageNumber, pageSize);
            return Ok(products);
        }


        [Route("GetNewProductsAll")]
        [HttpGet]
        public IActionResult GetNewProducts(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Không thể tạo !!");
            }

            var products = _iproductBusiness.GetNewProducts(pageNumber, pageSize);
            return Ok(products);
        }

        [Route("GetProductByID")]
        [HttpGet]
        public product GetProductByID(int id)
        {
            return _iproductBusiness.GetProductByID(id);
        }
    }
}

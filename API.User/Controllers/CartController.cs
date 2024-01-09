using BLL.Bussiness;
using BLL.Inerfaces;
using DTO.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private IcartBusiness _icartBusiness;
        public CartController(IcartBusiness icartBusiness)
        {
            _icartBusiness = icartBusiness;
        }

        [Route("GetAll/{id}")]
        [HttpGet]
        public List<cart> GetCustomerByID(int id)
        {
            return _icartBusiness.GetAll(id);
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] cart cart)
        {
            
            bool isProductInCart = _icartBusiness.CheckProductInCart(cart.MaKH, cart.MaSP);

            if (isProductInCart == true)
            {
                return BadRequest("Sản phẩm đã có trong giỏ hàng!");
            }
            else
            {
                bool isSuccess = _icartBusiness.Create(cart);

                if (isSuccess)
                {
                    return Ok("Thêm sản phẩm vào giỏ hàng thành công !");
                }
                else
                {
                    return BadRequest("Đã xảy ra lỗi khi thêm !");
                }
            }
        }


        [Route("Update")]
        [HttpPut]
        public IActionResult Update([FromBody] cart cart)
        {
            bool isSuccess = _icartBusiness.Update(cart);

            if (isSuccess)
            {
                return Ok("Sửa sản phẩm trong giỏ hàng thành công !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi sửa !");
            }
        }


        [Route("Delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool isSuccess = _icartBusiness.Delete(id);
            if (isSuccess)
            {
                return Ok("Xóa thành công giỏ hàng !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi xóa !");
            }
        }

        [Route("Count/{id}")]
        [HttpGet]
        public IActionResult Count(int id)
        {
            try
            {
                int productCount = _icartBusiness.Count(id);
                //return Ok($"Số lượng sản phẩm trong giỏ hàng của khách hàng có ID {id} là: {productCount}");
                return Ok(productCount);
            }
            catch (Exception ex)
            {
                return BadRequest($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        
    }
}

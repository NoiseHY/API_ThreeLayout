using BLL.Inerfaces;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoCustomerController : ControllerBase
    {
        private IcustomerBusiness _customerBusiness;

        public InfoCustomerController(IcustomerBusiness icustomerBusiness)
        {
            _customerBusiness = icustomerBusiness;
        }

        [Route("GetCustomerByID/{id}")]
        [HttpGet]
        public customer GetCustomerByID(int id)
        {
            return _customerBusiness.GetCustomerByID(id);
        }

        [Route("Update")]
        [HttpPut]
        public IActionResult Update([FromBody] customer customer)
        {
            bool isSuccess = _customerBusiness.Update(customer);

            if (isSuccess)
            {
                return Ok("Sửa khách hàng mã  " + customer.MaKH + " thành công !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi sửa !");
            }
        }


    }
}

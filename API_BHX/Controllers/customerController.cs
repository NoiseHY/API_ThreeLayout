using BLL.Inerfaces;
using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_BHX.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class customerController : ControllerBase
    {
        private IcustomerBusiness _customerBusiness;

        public customerController(IcustomerBusiness icustomerBusiness)
        {
            _customerBusiness = icustomerBusiness;
        }


        [Route("GetCustomerByID/{id}")]
        [HttpGet]
        public customer GetCustomerByID(int id)
        {
            return _customerBusiness.GetCustomerByID(id);
        }

        [Route("GetAll")]
        [HttpGet]
        public List<customer> GetAll(int pageNumber, int pageSize)
        {
            return _customerBusiness.GetAll(pageNumber, pageSize);
        }

        
        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] customer customer)
        {
            bool isSuccess = _customerBusiness.Create(customer);

            if (isSuccess)
            {
                return Ok("Tạo khách hàng  thành công !"); 
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi tạo khách hàng ."); 
            }
        }


        [Route("Update")]
        [HttpPut]
        public IActionResult Update([FromBody] customer customer)
        {
            bool isSuccess = _customerBusiness.Update(customer);

            if (isSuccess)
            {
                return Ok("Sửa khách hàng  mã  " + customer.MaKH + " thành công !");
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
            bool isSuccess = _customerBusiness.Delete(id);
            if (isSuccess)
            {
                return Ok("Xóa thành công khách hàng  !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi xóa !");
            }
        }

    }
}

using BLL.Bussiness;
using BLL.Inerfaces;
using DTO.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private IbillBusiness _ibillBusiness;
        public BillController(IbillBusiness ibillBusiness)
        {
            _ibillBusiness = ibillBusiness;
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] bill bill)
        {
            bool isSuccess = _ibillBusiness.Create( bill);

            if (isSuccess)
            {
                return Ok("Thêm hóa đơn thành công !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi thêm hóa đơn !");
            }
        }


        [Route("CreateTemp")]
        [HttpPost]
        public IActionResult CreateTemp([FromBody] List<bill> bill)
        {
            bool isSuccess = _ibillBusiness.CreateTemp(bill);

            if (isSuccess)
            {
                return Ok("Thêm bảng tạm thành công !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi thêm !");
            }
        }

        [Route("GetAllBill/{id}")]
        [HttpGet]
        public List<bill> GetAllCategory(int id)
        {
            return _ibillBusiness.GetAllBill(id);
        }

        [Route("GetAllBillInfo/{id}")]
        [HttpGet]
        public List<bill> GetAllBillInfo(int id)
        {
            return _ibillBusiness.GetAllBiillInfo(id);
        }

        [Route("GetAllBillByCustomerID/{id}")]
        [HttpGet]
        public List<bill> GetAllBillByCustomerID(int id)
        {
            return _ibillBusiness.GetAllBillByCustomerID(id);
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public IActionResult  Delete(int id)
        {
            bool isSuccess = _ibillBusiness.Delete(id);
            if (isSuccess)
            {
                return Ok("Xóa thành công !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi xóa !");
            }
        }
    }
}

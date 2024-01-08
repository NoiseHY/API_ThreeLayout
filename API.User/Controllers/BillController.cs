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

        [Route("GetAllCategory/{id}")]
        [HttpGet]
        public List<bill> GetCustomerByID(int id)
        {
            return _ibillBusiness.GetAllCategory(id);
        }

        [Route("GetAllCategoryInfo/{id}")]
        [HttpGet]
        public List<bill> GetAllCategoryInfo(int id)
        {
            return _ibillBusiness.GetAllCategoryInfo(id);
        }
    }
}

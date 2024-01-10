using BLL.Bussiness;
using BLL.Inerfaces;
using DTO;
using DTO.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_BHX.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class accountController : ControllerBase
    {
        private IaccountBusiness _accountBusiness;
        public accountController(IaccountBusiness accountBusiness)
        {
            _accountBusiness = accountBusiness;
        }

        [Route("GetAll")]
        [HttpGet]
        public List<account> GetAll(int pageNumber, int pageSize)
        {
            return _accountBusiness.GetAll(pageNumber, pageSize);
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] account account)
        {
            bool isSuccess = _accountBusiness.Create(account);

            if (isSuccess)
            {
                return Ok("Tạo tài khoản thành công !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi tạo tài khoản .");
            }
        }


        [Route("Update")]
        [HttpPut]
        public IActionResult Update([FromBody] account account)
        {
            bool isSuccess = _accountBusiness.Update(account);

            if (isSuccess)
            {
                return Ok("Sửa tài khoản  mã  " + account.MaTK + " thành công !");
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
            bool isSuccess = _accountBusiness.Delete(id);
            if (isSuccess)
            {
                return Ok("Xóa thành công tài khoản  !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi xóa !");
            }
        }
    }
}

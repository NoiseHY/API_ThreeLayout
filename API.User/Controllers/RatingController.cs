using BLL.Bussiness;
using BLL.Inerfaces;
using DTO.Rating;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private IratingBusiness _iratingBusiness;
        public RatingController(IratingBusiness iratingBusiness)
        {
            _iratingBusiness = iratingBusiness;
        }

        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Không thể tạo !!");
            }

            var products = _iratingBusiness.GetAll(pageNumber, pageSize);

            return Ok(products);
        }

        [Route("GetAllRatingCmt")]
        [HttpGet]
        public IActionResult GetAllRatingCmt(int id)
        {
            try
            {
                var ratings = _iratingBusiness.GetAllRatingCmt(id);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("GetCommentsByMaTK")]
        [HttpGet]
        public IActionResult GetCommentsByMaTK(int id)
        {
            try
            {
                var ratings = _iratingBusiness.GetCommentsByMaTK(id);
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("Create")]
        [HttpPost]  
        public IActionResult Create([FromBody] rating rating)
        {
            if (rating == null)
            {
                return BadRequest("Dữ liệu không hợp lệ !");
            }

            bool isSuccess = _iratingBusiness.Create(rating);

            if (isSuccess)
            {
                return Ok("Thêm đánh giá thành công !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi tạo đánh giá !");
            }
        }

        [Route("Update")]
        [HttpPut]
        public IActionResult Update([FromBody] rating rating)
        {
            if (rating == null)
            {
                return BadRequest("Dữ liệu không hợp lệ !");
            }

            bool isSuccess = _iratingBusiness.Update(rating);

            if (isSuccess)
            {
                return Ok("Sửa đánh giá mã " + rating.MaDanhGia + " thành công !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi sửa đánh giá !");
            }
        }

        [Route("Delete")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool isSuccess = _iratingBusiness.Delete(id);
            if (isSuccess)
            {
                return Ok("Xóa thành công đánh giá !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi xóa đánh giá !");
            }
        }
    }
}

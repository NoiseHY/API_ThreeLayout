using BLL.Bussiness;
using BLL.Inerfaces;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_BHX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private IproductBusiness _iproductBusiness;

        public productController(IproductBusiness iproductBusiness ) 
        {
            _iproductBusiness = iproductBusiness;
        }

        [Route("api/product/uploadImage")]
        [HttpPost]
        public async Task<IActionResult> UploadImage(int productID ,IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    return BadRequest("File không hợp lệ.");
                }

                string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

                // Tạo thư mục uploads nếu nó chưa tồn tại
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Lưu đường dẫn của ảnh vào cơ sở dữ liệu
                bool success = _iproductBusiness.UpdateImageFilePath(productID, filePath);

                if (success)
                {
                    return Ok("Tải ảnh lên và cập nhật thành công!");
                }
                else
                {
                    return BadRequest("Đã xảy ra lỗi khi cập nhật đường dẫn ảnh vào cơ sở dữ liệu.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }


        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll(int pageNumber = 1, int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1)
            {
                return BadRequest("Không thể tạo !!");
            }

            var products = _iproductBusiness.GetAll(pageNumber, pageSize);
            return Ok(products);
        }



        [Route("GetProductByID")]
        [HttpGet]
        public product GetProductByID(int id)
        {
            return _iproductBusiness.GetProductByID(id);
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] product product)
        {
            if (product == null )
            {
                return BadRequest("Dữ liệu sản phẩm hoặc ảnh không hợp lệ !");
            }

            bool isSuccess = _iproductBusiness.Create(product);

            if (isSuccess)
            {
                return Ok("Thêm sản phẩm thành công !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi tạo sản phẩm !");
            }
        }

        [Route("Update")]
        [HttpPut]
        public IActionResult Update([FromBody] product product)
        {
            if (product == null)
            {
                return BadRequest("Dữ liệu sản phẩm hoặc ảnh không hợp lệ !");
            }

            bool isSuccess = _iproductBusiness.Update(product);

            if (isSuccess)
            {
                return Ok("Sửa sản phẩm mã " + product.MaSP + " thành công !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi sửa sản phẩm !");
            }
        }




        [Route("Delete/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            bool isSuccess = _iproductBusiness.Delete(id);
            if (isSuccess)
            {
                return Ok("Xóa thành công sản phẩm !");
            }
            else
            {
                return BadRequest("Đã xảy ra lỗi khi xóa sản phẩm !");
            }
        }
    }
}

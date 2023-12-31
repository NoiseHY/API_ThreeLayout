using BLL.Bussiness;
using BLL.Inerfaces;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

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

        [Route("uploadImage")]
        [HttpPost]
        public async Task<IActionResult> UploadImage(int customerID, IFormFile file)
        {
            try
            {
                if (file == null || file.Length <= 0)
                {
                    return BadRequest("File không hợp lệ.");
                }

                // Đọc dữ liệu từ file thành mảng byte
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    byte[] imageBytes = ms.ToArray();

                    // Nén hình ảnh trước khi chuyển đổi thành base64
                    byte[] compressedImageBytes;
                    using (Image image = Image.Load(imageBytes))
                    {
                        var encoder = new JpegEncoder();
                        image.Mutate(x => x.Resize(150, 150));
                        using (var compressedStream = new MemoryStream())
                        {
                            image.Save(compressedStream, encoder);
                            compressedImageBytes = compressedStream.ToArray();
                        }
                    }

                    // Chuyển đổi mảng byte đã nén thành chuỗi base64
                    string base64String = Convert.ToBase64String(compressedImageBytes);

                    if (customerID == 0)
                    {
                        return BadRequest("Mã khách hàng = 0");
                    }

                    // Lưu trữ chuỗi base64 vào cơ sở dữ liệu
                    bool success = _customerBusiness.UpdateImageFilePath(customerID, base64String);

                    if (success)
                    {
                        return Ok("Thêm hình ảnh thành công!");
                    }
                    else
                    {
                        return BadRequest("Đã xảy ra lỗi khi cập nhật base64 vào cơ sở dữ liệu.");
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi: {ex.Message}");
            }
        }

    }
}

using elsaeedTea.service.Services.teaProductServices;
using Microsoft.AspNetCore.Mvc;
using elsaeedTea.service.Services.teaImagesServices;
using elsaeedTea.service.Services.teaImagesServices.Dtos;
using elsaeedTea.data.Entities;
using AutoMapper;



namespace elsaeedTea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeaImagesController : ControllerBase
    {
        private readonly IImageServices _imageServices;
        private readonly ITeaServices _teaServices;
        private readonly IMapper _mapper;

        public TeaImagesController(IImageServices imageServices, ITeaServices teaServices, IMapper mapper)
        {
            _imageServices = imageServices;
            _teaServices = teaServices;
            _mapper = mapper;
        }

        //[HttpGet("GetAllTeaImages")]
        //public async Task<ActionResult<IReadOnlyList<GetTeaImages>>> GetAllTeaImages()
        //{
        //    try
        //    {
        //        var teaImages = await _imageServices.GetAllTeaImages();

        //        if (teaImages == null || teaImages.Count == 0)
        //        {
        //            return NotFound("No tea Images found.");
        //        }

        //        return Ok(teaImages);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}








        [HttpGet("GetAllTeaImages")]
        public async Task<ActionResult<IReadOnlyList<GetTeaImages>>> GetAllTeaImages()
        {
            try
            {
                var teaImages = await _imageServices.GetAllTeaImages();

                if (teaImages == null || teaImages.Count == 0)
                {
                    return NotFound("No tea Images found.");
                }

                // بناء الرابط الكامل للصورة
                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; // يقوم بدمج البروتوكول والمضيف
                foreach (var teaImage in teaImages)
                {
                    teaImage.ImageUrl = $"{baseUrl}{teaImage.ImageUrl}"; // بناء الرابط الكامل مع اسم الصورة
                }

                return Ok(teaImages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






















        [HttpGet("GetTeaImagesById/{id}")]
        public async Task<ActionResult<GetTeaImages>> GetTeaImagesById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                var teaImage = await _imageServices.GetTeaImagesById(id);

                if (teaImage == null)
                {
                    return NotFound("No tea Image found.");
                }
                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; // يقوم بدمج البروتوكول والمضيف
                teaImage.ImageUrl = $"{baseUrl}{teaImage.ImageUrl}"; // بناء الرابط الكامل مع اسم الصورة

                return Ok(teaImage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //[HttpPost("AddTeaImages")]
        //public async Task<ActionResult<GetTeaImages>> AddTeaImages(AddTeaImages teaImageDto)
        //{
        //    try
        //    {
        //        if (teaImageDto == null)
        //        {
        //            return BadRequest("tea Images shouldn't be empty .");
        //        }

        //        var tea = await _teaServices.GetTeaDetailsById(teaImageDto.ProductId);
        //        if (tea == null)
        //        {
        //            return NotFound($"There is no tea with id {teaImageDto.ProductId}");
        //        }

        //        var image = await _imageServices.AddTeaImages(teaImageDto);
        //        return Ok(image);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}










        [HttpPost("AddTeaImages")]
        public async Task<ActionResult<GetTeaImages>> AddTeaImages([FromForm] AddTeaImages teaImageDto)
        {
            try
            {
                if (teaImageDto == null || teaImageDto.ImageFile == null)
                {
                    return BadRequest("Image and details are required.");
                }

                var tea = await _teaServices.GetTeaDetailsById(teaImageDto.ProductId);
                if(tea == null)
                {
                    return NotFound($"there is no tea exist with id {teaImageDto.ProductId}");
                }

                // رفع الصورة وتخزينها في السيرفر
                var folderPath = Path.Combine("wwwroot", "images");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(teaImageDto.ImageFile.FileName)}";
                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await teaImageDto.ImageFile.CopyToAsync(stream);
                }

                // إنشاء الرابط النهائي للصورة
                var imageUrl = $"/images/{fileName}";

                // تحديث الكائن بـ ImageUrl فقط
                var teaImage = new ElsaeedTeaProductImage
                {
                    ImageUrl = imageUrl,
                    ProductId = teaImageDto.ProductId
                };

                // حفظ التفاصيل في قاعدة البيانات
                var result = await _imageServices.AddTeaImages(_mapper.Map<AddTeaImages>(teaImage), imageUrl);

                if(result == null && teaImageDto == null)
                {
                    return BadRequest("Image and details are required.");
                }
                else if (result == null && teaImageDto != null)
                {
                    return BadRequest("Failed to save image to the database.");
                }

                // إرجاع النتيجة للعميل
                return Ok(new
                {
                    Message = "Image uploaded successfully",
                    ImageUrl = imageUrl,
                    ProductId = teaImageDto.ProductId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }








        //[HttpPut("UpdateTeaImages")]
        //public async Task<ActionResult<GetTeaImages>> UpdateTeaImages(int id, AddTeaImages teaImageDto)
        //{
        //    try
        //    {
        //        if (id <= 0)
        //        {
        //            return BadRequest("Invalid ID");
        //        }

        //        if (teaImageDto == null)
        //        {
        //            return BadRequest("tea Image shouldn't be empty .");
        //        }

        //        var image = await _imageServices.UpdateTeaImages(id, teaImageDto);
        //        if (image == null)
        //        {
        //            return BadRequest("there is no tea Image with that id .");
        //        }
        //        return Ok(image);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}















        [HttpPut("UpdateTeaImages/{id}")]
        public async Task<ActionResult<GetTeaImages>> UpdateTeaImages(int id, [FromForm] AddTeaImages teaImageDto)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid ID");
                }

                if (teaImageDto == null)
                {
                    return BadRequest("Tea image details shouldn't be empty.");
                }

                var existingImage = await _imageServices.GetTeaImagesById(id);
                if (existingImage == null)
                {
                    return NotFound($"No tea image found with id {id}.");
                }

                if (teaImageDto.ImageFile != null)
                {
                    if(existingImage.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine("wwwroot", existingImage.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath); // تأكد من أنك تستخدم System.IO.File
                        }
                    }
                    // حذف الصورة القديمة من السيرفر
                    


                    var folderPath = Path.Combine("wwwroot", "images");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(teaImageDto.ImageFile.FileName)}";
                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await teaImageDto.ImageFile.CopyToAsync(stream);
                    }

                    existingImage.ImageUrl = $"/images/{fileName}";
                }

                if (teaImageDto.ProductId != existingImage.ProductId)
                {
                    existingImage.ProductId = teaImageDto.ProductId;
                }

                // حفظ التعديلات في قاعدة البيانات
                var updatedImage = await _imageServices.UpdateTeaImages(id, existingImage);

                if (updatedImage == null && teaImageDto == null)
                {
                    return BadRequest("Image and details are required.");
                }
                else if (updatedImage == null && teaImageDto != null)
                {
                    return BadRequest("Failed to save updated image to the database.");
                }

                return Ok(updatedImage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }









        [HttpDelete("DeleteTeaImage/{id}")]
        public async Task<ActionResult> DeleteTeaImage(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                var teaImage = await _imageServices.GetTeaImagesById(id);

                if (teaImage == null)
                {
                    return NotFound("No tea Image found.");
                }

                var result = await _imageServices.DeleteImage(id);

                if (result == 0 && teaImage != null)
                {
                    return BadRequest("Failed to delete image from the database.");
                }

                var oldImagePath = Path.Combine("wwwroot", teaImage.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath); // تأكد من أنك تستخدم System.IO.File
                }


                return Ok($"Tea image With Id {id} deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }












    }


}

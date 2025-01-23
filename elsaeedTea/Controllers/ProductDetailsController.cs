using elsaeedTea.service.Services.teaProductServices.Dtos;
using elsaeedTea.service.Services.teaProductServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using elsaeedTea.service.Services.TeaDetailsServices;
using elsaeedTea.service.Services.TeaDetailsServices.Dtos;

namespace elsaeedTea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductDetailsController : ControllerBase
    {
        private readonly ITeaDetails _teaDetailsServices;
        private readonly ITeaServices _teaServices;

        public ProductDetailsController(ITeaDetails teaDetailsServices,ITeaServices teaServices)
        {
            _teaDetailsServices = teaDetailsServices;
            _teaServices = teaServices;
        }


        [HttpGet("GetAllTeaDetailsProducts")]
        public async Task<ActionResult<IReadOnlyList<GetTeaDetailsDto>>> GetAllTeaDetailsProducts()
        {
            try
            {
                // الحصول على جميع تفاصيل الشاي
                var teaProducts = await _teaDetailsServices.GetAllTeaDetails();

                // التحقق إذا كانت البيانات فارغة
                if (teaProducts == null || teaProducts.Count == 0)
                {
                    // إرسال استجابة فارغة مع حالة 404 (Not Found)
                    return NotFound("No tea products found.");
                }

                // إرسال الاستجابة الناجحة مع البيانات
                return Ok(teaProducts);
            }
            catch (Exception ex)
            {
                // في حالة حدوث استثناء أثناء تنفيذ الكود
                // يمكنك تسجيل الخطأ أو إضافة تفاصيل إضافية هنا
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }









        [HttpGet("GetTeaDetailsById/{id}")]
        public async Task<ActionResult<GetTeaDetailsDto>> GetTeaDetailsById(int id)
        {
            try
            {


                if (id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                // الحصول على جميع تفاصيل الشاي
                var teaProduct = await _teaDetailsServices.GetTeaDetailsById(id);

                // التحقق إذا كانت البيانات فارغة
                if (teaProduct == null)
                {
                    // إرسال استجابة فارغة مع حالة 404 (Not Found)
                    return NotFound("No tea product found.");
                }

                // إرسال الاستجابة الناجحة مع البيانات
                return Ok(teaProduct);
            }
            catch (Exception ex)
            {
                // في حالة حدوث استثناء أثناء تنفيذ الكود
                // يمكنك تسجيل الخطأ أو إضافة تفاصيل إضافية هنا
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






















        [HttpPost("AddTeaDetailsProduct")]
        public async Task<ActionResult<GetTeaDetailsDto>> AddTeaDetailsProduct(AddTeaDetailsDto teaDto)
        {
            try
            {
                if (teaDto == null)
                {
                    // إرسال استجابة فارغة مع حالة 404 (Not Found)
                    return BadRequest("tea product shouldn't be empty .");
                }

                var teaProduct = await _teaServices.GetTeaDetailsByIdWithoutInclude(teaDto.ProductId);
                if(teaProduct == null)
                {
                    return NotFound($"there is no product found with id {teaDto.ProductId}");
                }

                var tea = await _teaDetailsServices.AddTeaDetails(teaDto);

                if (tea == null && teaDto == null)
                {
                    return BadRequest("tea product shouldn't be empty .");
                }
                else if (tea == null && teaDto != null)
                {
                    return BadRequest("Failed to save Tea to the database.");

                }
                // إرسال الاستجابة الناجحة مع البيانات
                return Ok(tea);
            }
            catch (Exception ex)
            {
                // في حالة حدوث استثناء أثناء تنفيذ الكود
                // يمكنك تسجيل الخطأ أو إضافة تفاصيل إضافية هنا
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }











        [HttpPut("UpdateTeaDetailsProduct/{id}")]
        public async Task<ActionResult<GetTeaDetailsDto>> UpdateTeaDetailsProduct(int id,AddTeaDetailsDto teaDto)
        {
            try
            {

                if (id <= 0)
                {
                    return BadRequest("Invalid ID");
                }

                if (teaDto == null)
                {
                    // إرسال استجابة فارغة مع حالة 404 (Not Found)
                    return BadRequest("tea product shouldn't be empty .");
                }
                var teaProduct = await _teaServices.GetTeaDetailsByIdWithoutInclude(teaDto.ProductId);
                if (teaProduct == null)
                {
                    return NotFound($"there is no product found with id {teaDto.ProductId}");
                }
                var tea = await _teaDetailsServices.UpdateTeaDetails(id, teaDto);
                if (tea == null)
                {
                    return BadRequest("there is no tea product with that id .");
                }

                if (tea == null && teaDto == null)
                {
                    return BadRequest("tea product shouldn't be empty .");
                }
                else if (tea == null && teaDto != null)
                {
                    return BadRequest("Failed to save updated Tea to the database.");

                }


                // إرسال الاستجابة الناجحة مع البيانات
                return Ok(tea);
            }
            catch (Exception ex)
            {
                // في حالة حدوث استثناء أثناء تنفيذ الكود
                // يمكنك تسجيل الخطأ أو إضافة تفاصيل إضافية هنا
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }








        [HttpDelete("DeleteTeaDetailsProduct/{id}")]
        public async Task<ActionResult<IReadOnlyList<TeaDetailsDto>>> DeleteTeaDetailsProduct(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                var teaProduct = await _teaDetailsServices.GetTeaDetailsById(id);

                if (teaProduct == null)
                {
                    return NotFound("No tea product found.");
                }

                var result = await _teaDetailsServices.DeleteTeaDetails(id);
                if (result == 0 && teaProduct == null)
                {
                    return NotFound("No tea product found.");
                }
                else if (result == 0 && teaProduct != null)
                {
                    return NotFound("Failed to delete tea from the database .");

                }

                var productsDetails = await _teaServices.GetAllTeaDetails();
                // إرسال الاستجابة الناجحة مع البيانات
                return Ok(productsDetails);
            }
            catch (Exception ex)
            {
                // في حالة حدوث استثناء أثناء تنفيذ الكود
                // يمكنك تسجيل الخطأ أو إضافة تفاصيل إضافية هنا
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

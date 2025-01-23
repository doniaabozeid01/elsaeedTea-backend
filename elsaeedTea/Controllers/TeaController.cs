using elsaeedTea.service.Services.TeaDetailsServices.Dtos;
using elsaeedTea.service.Services.teaProductServices;
using elsaeedTea.service.Services.teaProductServices.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace elsaeedTea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeaController : ControllerBase
    {
        private readonly ITeaServices _teaServices;

        public TeaController(ITeaServices teaServices)
        {
            _teaServices = teaServices;
        }

        //[HttpGet("GetAllTeaProducts")]
        //public async Task<ActionResult<IReadOnlyList<TeaDetailsDto>>> GetAllTeaProducts ()
        //{
        //    var teaProducts = await _teaServices.GetAllTeaDetails();
        //    return Ok(teaProducts);
        //}



        [HttpGet("GetAllTeaProducts")]
        public async Task<ActionResult<IReadOnlyList<TeaDetailsDto>>> GetAllTeaProducts()
        {
            try
            {
                // الحصول على جميع تفاصيل الشاي
                var teaProducts = await _teaServices.GetAllTeaDetails();

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









        [HttpGet("GetTeaById/{id}")]
        public async Task<ActionResult<TeaDetailsDto>> GetTeaById(int id)
        {
            try
            {


                if (id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                // الحصول على جميع تفاصيل الشاي
                var teaProduct = await _teaServices.GetTeaDetailsById(id);

                // التحقق إذا كانت البيانات فارغة
                if (teaProduct == null )
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






















        [HttpPost("AddTeaProduct")]
        public async Task<ActionResult<TeaDetailsDto>> AddTeaProduct(AddNewTeaDto teaDto)
        {
            try
            {
                if (teaDto == null)
                {
                    // إرسال استجابة فارغة مع حالة 404 (Not Found)
                    return BadRequest("tea product shouldn't be empty .");
                }
                var tea = await _teaServices.AddTeaDetails(teaDto);

                if(tea == null && teaDto == null)
                {
                    return BadRequest("tea product shouldn't be empty .");
                }
                else if(tea == null && teaDto != null)
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











        [HttpPut("UpdateTeaProduct")]
        public async Task<ActionResult<TeaDetailsDto>> UpdateTeaProduct(int id, AddNewTeaDto teaDto)
        {
            try
            {

                if(id <= 0)
                {
                    return BadRequest("Invalid ID");
                }

                if (teaDto == null)
                {
                    // إرسال استجابة فارغة مع حالة 404 (Not Found)
                    return BadRequest("tea product shouldn't be empty .");
                }

                var tea = await _teaServices.UpdateTea(id , teaDto);
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








        [HttpDelete("DeleteTeaProduct/{id}")]
        public async Task<ActionResult<IReadOnlyList<TeaDetailsDto>>> DeleteTeaProduct(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                var teaProduct = await _teaServices.GetTeaDetailsById(id);

                if (teaProduct == null)
                {
                    return NotFound("No tea product found.");
                }

                var result = await _teaServices.DeleteTea(id);
                if(result == 0 && teaProduct == null)
                {
                    return NotFound("No tea product found.");
                }
                else if (result == 0 && teaProduct != null)
                {
                    return NotFound("Failed to delete tea from the database .");

                }

                var AllProducts = await _teaServices.GetAllTeaDetails();
                // إرسال الاستجابة الناجحة مع البيانات
                return Ok(AllProducts);
            }
            catch (Exception ex)
            {
                // في حالة حدوث استثناء أثناء تنفيذ الكود
                // يمكنك تسجيل الخطأ أو إضافة تفاصيل إضافية هنا
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }










        [HttpGet("GetDetailsByProductId/{id}")]
        public async Task<ActionResult<IReadOnlyList<TeaDetailsDto>>> GetDetailsByProductId(int id)
        {
            try
            {


                if (id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                // الحصول على جميع تفاصيل الشاي
                var teaProduct = await _teaServices.GetDetailsByProductId(id);

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
    }
}

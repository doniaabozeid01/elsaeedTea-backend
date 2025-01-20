using elsaeedTea.data.Entities;
using elsaeedTea.service.Services.teaProductServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using elsaeedTea.service.Services.Reviews;
using elsaeedTea.service.Services.Reviews.Dtos;

namespace elsaeedTea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsServices _reviewServices;
        private readonly ITeaServices _teaServices;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewsController(IReviewsServices reviewServices, ITeaServices teaServices, UserManager<ApplicationUser> userManager)
        {
            _reviewServices = reviewServices;
            _teaServices = teaServices;
            _userManager = userManager;
        }



        [HttpGet("GetAllReviews")]
        public async Task<ActionResult<IReadOnlyList<GetReviewDto>>> GetAllReviews()
        {
            try
            {
                // الحصول على جميع تفاصيل الشاي
                var reviews = await _reviewServices.GetAllReviews();

                // التحقق إذا كانت البيانات فارغة
                if (reviews == null || reviews.Count == 0)
                {
                    // إرسال استجابة فارغة مع حالة 404 (Not Found)
                    return NotFound("No reviews found.");
                }

                // إرسال الاستجابة الناجحة مع البيانات
                return Ok(reviews);
            }
            catch (Exception ex)
            {
                // في حالة حدوث استثناء أثناء تنفيذ الكود
                // يمكنك تسجيل الخطأ أو إضافة تفاصيل إضافية هنا
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }









        [HttpGet("GetReviewById/{id}")]
        public async Task<ActionResult<GetReviewDto>> GetReviewById(int id)
        {
            try
            {


                if (id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                // الحصول على جميع تفاصيل الشاي
                var review = await _reviewServices.GetReviewById(id);

                // التحقق إذا كانت البيانات فارغة
                if (review == null)
                {
                    // إرسال استجابة فارغة مع حالة 404 (Not Found)
                    return NotFound("No review found.");
                }

                // إرسال الاستجابة الناجحة مع البيانات
                return Ok(review);
            }
            catch (Exception ex)
            {
                // في حالة حدوث استثناء أثناء تنفيذ الكود
                // يمكنك تسجيل الخطأ أو إضافة تفاصيل إضافية هنا
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






















        [HttpPost("AddReview")]
        public async Task<ActionResult<AddReviewDto>> AddReview(AddReviewDto reviewDto)
        {
            try
            {
                if (reviewDto == null)
                {
                    // إرسال استجابة فارغة مع حالة 404 (Not Found)
                    return BadRequest("Review shouldn't be empty .");
                }
                var product = await _teaServices.GetTeaDetailsByIdWithoutInclude(reviewDto.ProductId);
                if (product == null)
                {
                    return NotFound($"there is no product exist with id {reviewDto.ProductId}");
                }

                var User = await _userManager.FindByIdAsync(reviewDto.UserId);
                if (User == null)
                {
                    return NotFound($"there is no user exist with id {reviewDto.UserId}");
                }

                if (reviewDto.Rating <= 0 || reviewDto.Rating > 5)
                {
                    return BadRequest("review should be from 1 to 5 .");

                }

                var review = await _reviewServices.AddReview(reviewDto);

                if (review == null && reviewDto == null)
                {
                    return BadRequest("Review shouldn't be empty .");
                }
                else if (review == null && reviewDto != null)
                {
                    return BadRequest("Failed to save review to the database.");

                }
                // إرسال الاستجابة الناجحة مع البيانات
                return Ok(review);
            }
            catch (Exception ex)
            {
                // في حالة حدوث استثناء أثناء تنفيذ الكود
                // يمكنك تسجيل الخطأ أو إضافة تفاصيل إضافية هنا
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }











        [HttpPut("UpdateReview")]
        public async Task<ActionResult<GetReviewDto>> UpdateReview(int id, AddReviewDto reviewDto)
        {
            try
            {

                if (id <= 0)
                {
                    return BadRequest("Invalid ID");
                }

                if (reviewDto == null)
                {
                    // إرسال استجابة فارغة مع حالة 404 (Not Found)
                    return BadRequest("Review shouldn't be empty .");
                }

                var product = await _teaServices.GetTeaDetailsByIdWithoutInclude(reviewDto.ProductId);
                if (product == null)
                {
                    return NotFound($"there is no product exist with id {reviewDto.ProductId}");
                }

                var User = await _userManager.FindByIdAsync(reviewDto.UserId);
                if (User == null)
                {
                    return NotFound($"there is no user exist with id {reviewDto.UserId}");
                }


                var review = await _reviewServices.UpdateReview(id, reviewDto);
                if (review == null)
                {
                    return BadRequest("there is no review with that id .");
                }

                if (review == null && reviewDto == null)
                {
                    return BadRequest("Review shouldn't be empty .");
                }
                else if (review == null && reviewDto != null)
                {
                    return BadRequest("Failed to save updated review to the database.");

                }


                // إرسال الاستجابة الناجحة مع البيانات
                return Ok(review);
            }
            catch (Exception ex)
            {
                // في حالة حدوث استثناء أثناء تنفيذ الكود
                // يمكنك تسجيل الخطأ أو إضافة تفاصيل إضافية هنا
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }







        [HttpDelete("DeleteReview/{id}")]
        public async Task<ActionResult> DeleteReview(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid Id");
                }
                var review = await _reviewServices.GetReviewById(id);

                if (review == null)
                {
                    return NotFound("No review found.");
                }

                var result = await _reviewServices.DeleteReview(id);
                if (result == 0 && review == null)
                {
                    return NotFound("No review found.");
                }
                else if (result == 0 && review != null)
                {
                    return NotFound("Failed to delete review from the database .");

                }

                // إرسال الاستجابة الناجحة مع البيانات
                return Ok($"review With Id {id} deleted Successfully");
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

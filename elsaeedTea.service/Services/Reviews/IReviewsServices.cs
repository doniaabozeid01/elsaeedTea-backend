using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.service.Services.Reviews.Dtos;

namespace elsaeedTea.service.Services.Reviews
{
    public interface IReviewsServices
    {
        Task<AddReviewDto> AddReview(AddReviewDto reviewDto);
        Task<GetReviewDto> UpdateReview(int id, AddReviewDto reviewDto);
        Task<int> DeleteReview(int id);
        Task<IReadOnlyList<GetReviewDto>> GetAllReviews();
        Task<GetReviewDto> GetReviewById(int id);
    }
}

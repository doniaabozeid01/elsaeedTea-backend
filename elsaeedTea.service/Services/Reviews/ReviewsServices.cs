using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using elsaeedTea.data.Entities;
using elsaeedTea.repository.Interfaces;
using elsaeedTea.service.Services.Reviews.Dtos;

namespace elsaeedTea.service.Services.Reviews
{
    public class ReviewsServices : IReviewsServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewsServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AddReviewDto> AddReview(AddReviewDto reviewDto)
        {
            if(reviewDto != null)
            {
                var mappedReview = _mapper.Map<ProductReviews>(reviewDto);
                await _unitOfWork.Repository<ProductReviews>().AddAsync(mappedReview);
                var status = await _unitOfWork.CompleteAsync();

                if (status == 0)
                {
                    return null;
                }

                return reviewDto;
            }
            return null;
        }

        public async Task<int> DeleteReview(int id)
        {
            var review = await _unitOfWork.Repository<ProductReviews>().GetByIdAsync(id);
            if(review != null)
            {
                _unitOfWork.Repository<ProductReviews>().Delete(review);
                var status = await _unitOfWork.CompleteAsync();

                if (status == 0)
                {
                    return 0;
                }
                else
                {
                    return status;
                }
            }
            return 0;
        }

        public async Task<IReadOnlyList<GetReviewDto>> GetAllReviews()
        {
            var reviews = await _unitOfWork.Repository<ProductReviews>().GetAllAsync();
            if (reviews.Any()) // يعني فيه عالاقل تقييم واحد او بمعني تاني فيه تقييمات 
            {
                var mappedReviews = _mapper.Map<IReadOnlyList<GetReviewDto>>(reviews);
                return mappedReviews;
            }
            return null ;
        }

        public async Task<GetReviewDto> GetReviewById(int id)
        {
            var review = await _unitOfWork.Repository<ProductReviews>().GetByIdAsync(id);
            if (review != null) // يعني فيه عالاقل تقييم واحد او بمعني تاني فيه تقييمات 
            {
                var mappedReview = _mapper.Map<GetReviewDto>(review);
                return mappedReview;
            }
            return null;
        }

        public async Task<GetReviewDto> UpdateReview(int id, AddReviewDto reviewDto)
        {
            var review = await _unitOfWork.Repository<ProductReviews>().GetByIdAsync(id);
            if (review != null) // يعني فيه عالاقل تقييم واحد او بمعني تاني فيه تقييمات 
            {
                var mappedReview = _mapper.Map<AddReviewDto>(reviewDto);

                review.UserId = mappedReview.UserId;
                review.ProductId = mappedReview.ProductId;
                review.Comment = mappedReview.Comment;
                review.Rating = mappedReview.Rating;


                _unitOfWork.Repository<ProductReviews>().Update(review);
                var status = await _unitOfWork.CompleteAsync();
                if (status == 0)
                {
                    return null;
                }
                return new GetReviewDto
                {
                    Id = id,
                    UserId = mappedReview.UserId,
                    ProductId = mappedReview.ProductId,
                    Comment = mappedReview.Comment,
                    Rating = mappedReview.Rating,
                    CreatedAt = review.CreatedAt,
                    UpdatedAt = review.UpdatedAt,
                };
            }
            return null ;
        }
    }
}

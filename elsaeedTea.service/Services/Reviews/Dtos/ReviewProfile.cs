using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using elsaeedTea.data.Entities;

namespace elsaeedTea.service.Services.Reviews.Dtos
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<AddReviewDto, ProductReviews>().ReverseMap();
            CreateMap<GetReviewDto, ProductReviews>().ReverseMap();

            //CreateMap<ProductReviews, AddReviewDto>();
        }
    }
}

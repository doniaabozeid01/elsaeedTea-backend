using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using elsaeedTea.data.Entities;
using elsaeedTea.service.Services.teaProductServices.Dtos;

namespace elsaeedTea.service.Services.teaImagesServices.Dtos
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<ElsaeedTeaProductImage, GetTeaImages>();
            CreateMap<GetTeaImages, ElsaeedTeaProductImage>();

            CreateMap<ElsaeedTeaProductImage, AddTeaImages>();
            CreateMap<AddTeaImages, ElsaeedTeaProductImage>();

        }
    }
}

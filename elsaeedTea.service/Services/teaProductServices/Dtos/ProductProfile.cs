using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using elsaeedTea.data.Entities;

namespace elsaeedTea.service.Services.teaProductServices.Dtos
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ElsaeedTeaProduct, TeaDetailsDto>();
            CreateMap<TeaDetailsDto, ElsaeedTeaProduct>();

            CreateMap<ElsaeedTeaProduct, AddNewTeaDto>();
            CreateMap<AddNewTeaDto, ElsaeedTeaProduct>();
        }
    }
}

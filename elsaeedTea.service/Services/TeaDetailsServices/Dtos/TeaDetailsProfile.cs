using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using elsaeedTea.data.Entities;

namespace elsaeedTea.service.Services.TeaDetailsServices.Dtos
{
    public class TeaDetailsProfile:Profile
    {
        public TeaDetailsProfile()
        {
            CreateMap<GetTeaDetailsDto, ElsaeedTeaProductDetails>().ReverseMap();
            CreateMap<AddTeaDetailsDto, ElsaeedTeaProductDetails>().ReverseMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using elsaeedTea.data.Entities;

namespace elsaeedTea.service.Services.Order.Dtos
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<addOrder, OrderRequest>();
            CreateMap<OrderRequest, addOrder>();


            CreateMap<GetOrderRequest, OrderRequest>();
            CreateMap<OrderRequest, GetOrderRequest>();
        }
    }
}

using AutoMapper;
using elsaeedTea.data.Entities;

namespace elsaeedTea.service.Services.CartServices.Dtos
{
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartItem, AddCart>();
            CreateMap<AddCart, CartItem>();

            CreateMap<CartItem, GetCart>();
            CreateMap<GetCart, CartItem>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.service.Services.CartServices.Dtos;
using elsaeedTea.service.Services.teaImagesServices.Dtos;

namespace elsaeedTea.service.Services.CartServices
{
    public interface ICartService
    {
        Task<GetCart> GetCartById(int id);
        Task<GetCart> GetCartByIdWithoutInclude(int id);
        Task<IReadOnlyList<GetCart>> GetAllCarts();
        Task<AddCart> AddCart(AddCart cartDto);
        Task<GetCart> UpdateCart(int id, AddCart cartDto);
        //Task DeleteTeaAfterDeleteItsImages(int id); // بتمسح صور اللشاي صاحب ال id المبعوت
        Task<int> DeleteCart(int id); // بتمسح صوره بال id بتاعها
    }
}

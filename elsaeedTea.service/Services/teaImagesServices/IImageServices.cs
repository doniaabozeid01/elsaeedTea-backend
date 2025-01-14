using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.service.Services.teaImagesServices.Dtos;
using elsaeedTea.service.Services.teaProductServices.Dtos;

namespace elsaeedTea.service.Services.teaImagesServices
{
    public interface IImageServices
    {
        Task<GetTeaImages> GetTeaImagesById(int id);
        Task<IReadOnlyList<GetTeaImages>> GetAllTeaImages();
        Task<AddTeaImages> AddTeaImages(AddTeaImages teaImageDto, string path);
        Task<GetTeaImages> UpdateTeaImages(int id, GetTeaImages teaImageDto);
        //Task DeleteTeaAfterDeleteItsImages(int id); // بتمسح صور اللشاي صاحب ال id المبعوت
        Task<int> DeleteImage(int id); // بتمسح صوره بال id بتاعها


    }
}

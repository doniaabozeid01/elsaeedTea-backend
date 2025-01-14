using elsaeedTea.service.Services.teaProductServices.Dtos;

namespace elsaeedTea.service.Services.teaProductServices
{
    public interface ITeaServices 
    {
        Task<TeaDetailsDto> GetTeaDetailsById(int id);
        Task<IReadOnlyList<TeaDetailsDto>> GetAllTeaDetails();
        //Task<IReadOnlyList<TeaDetailsDto>> GetAllTeaImages();
        Task<AddNewTeaDto> AddTeaDetails(AddNewTeaDto addNewTea);
        Task<TeaDetailsDto> UpdateTea(int id, AddNewTeaDto addNewTea);
        Task<int> DeleteTea(int id);
    }
}

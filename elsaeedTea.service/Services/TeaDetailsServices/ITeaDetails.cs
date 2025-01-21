using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.service.Services.TeaDetailsServices.Dtos;

namespace elsaeedTea.service.Services.TeaDetailsServices
{
    public interface ITeaDetails
    {
        Task<GetTeaDetailsDto> UpdateTeaDetails(int id, AddTeaDetailsDto addNewTea);
        Task<GetTeaDetailsDto> GetTeaDetailsById(int id);
        Task<IReadOnlyList<GetTeaDetailsDto>> GetAllTeaDetails();
        Task<int> DeleteTeaDetails(int id);
        Task<AddTeaDetailsDto> AddTeaDetails(AddTeaDetailsDto addNewTea);


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using elsaeedTea.data.Context;
using elsaeedTea.data.Entities;
using elsaeedTea.repository.Interfaces;
using elsaeedTea.service.Services.teaProductServices.Dtos;
using static System.Net.Mime.MediaTypeNames;

namespace elsaeedTea.service.Services.teaProductServices
{
    public class TeaServices : ITeaServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeaServices(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AddNewTeaDto> AddTeaDetails(AddNewTeaDto addNewTea)
        {
            if (addNewTea != null)
            {
                // تحويل الـ DTO إلى الكائن الذي سيتم حفظه في قاعدة البيانات
                var teaEntity = _mapper.Map<ElsaeedTeaProduct>(addNewTea);


                // إضافة الكائن إلى Repository
                await _unitOfWork.Repository<ElsaeedTeaProduct>().AddAsync(teaEntity);

                // حفظ التغييرات في قاعدة البيانات
                var status = await _unitOfWork.CompleteAsync();

                if (status == 0)
                {
                    return null;
                }

                // يمكن العودة بالـ DTO أو الكائن الذي تم إضافته بعد تحويله مرة أخرى إذا لزم الأمر
                return addNewTea; // أو يمكن إنشاء AddNewTeaDto جديد من الكائن الذي تم حفظه
            }

            return null; // في حالة لم يتم تمرير تفاصيل الشاي
        }

        public async Task<int> DeleteTea(int id)
        {
            var teaProduct = await _unitOfWork.Repository<ElsaeedTeaProduct>().GetByIdAsync(id);
            if (teaProduct != null)
            {
                _unitOfWork.Repository<ElsaeedTeaProduct>().Delete(teaProduct);
                var status = await _unitOfWork.CompleteAsync(); //

                if( status == 0)
                {
                    return 0;
                }
                return status;
            }
            return 0;
        }

        public async Task<IReadOnlyList<TeaDetailsDto>> GetAllTeaDetails()
        {
            var teaProduct = await _unitOfWork.Repository<ElsaeedTeaProduct>().GetAllTeaAsync();
            var mappedTea = _mapper.Map<IReadOnlyList<TeaDetailsDto>>(teaProduct);
            return mappedTea;
        }

        public async Task<TeaDetailsDto> GetTeaDetailsById(int id)
        {
            var teaProduct = await _unitOfWork.Repository<ElsaeedTeaProduct>().GetTeaByIdAsync(id);
            var mappedTea = _mapper.Map<TeaDetailsDto>(teaProduct);
            return mappedTea;
        }


        public async Task<TeaDetailsDto> GetTeaDetailsByIdWithoutInclude(int id)
        {
            var teaProduct = await _unitOfWork.Repository<ElsaeedTeaProduct>().GetByIdAsync(id);
            var mappedTea = _mapper.Map<TeaDetailsDto>(teaProduct);
            return mappedTea;
        }


        public async Task<TeaDetailsDto> UpdateTea (int id, AddNewTeaDto addNewTea)
        {
            var productTea = await _unitOfWork.Repository<ElsaeedTeaProduct>().GetByIdAsync(id);
            
            if (productTea != null)
            {
                // استخدام AutoMapper لتحويل الـ DTO إلى الكائن الفعلي (ElsaeedTeaProduct)
                var mappedTea = _mapper.Map<ElsaeedTeaProduct>(addNewTea);

                // تحديث الكائن بالبيانات الجديدة
                productTea.Name = mappedTea.Name;
                productTea.Description = mappedTea.Description;
                productTea.Price = mappedTea.Price;
                productTea.Weight = mappedTea.Weight;

                // تحديث الكائن في الـ Repository
                _unitOfWork.Repository<ElsaeedTeaProduct>().Update(productTea);

                // حفظ التغييرات في قاعدة البيانات
                var status = await _unitOfWork.CompleteAsync();

                if(status == 0)
                {
                    return null;
                }

                var mapToDto = new TeaDetailsDto()
                {
                    Id = id,
                    Name = mappedTea.Name,
                    Description = mappedTea.Description,
                    Price = mappedTea.Price,
                    Weight = mappedTea.Weight,
                    Images = productTea.Images,
                };

                return mapToDto;
            }

            //else
            //{
            //    // إذا لم يتم العثور على المنتج في قاعدة البيانات
            //    throw new Exception("Tea product not found.");
            //}
            return null;
        }
    }
}

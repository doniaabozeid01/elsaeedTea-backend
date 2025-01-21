using AutoMapper;
using elsaeedTea.data.Entities;
using elsaeedTea.repository.Interfaces;
using elsaeedTea.service.Services.TeaDetailsServices.Dtos;

namespace elsaeedTea.service.Services.TeaDetailsServices
{
    public class TeaDetails : ITeaDetails
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TeaDetails(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<AddTeaDetailsDto> AddTeaDetails(AddTeaDetailsDto addNewTea)
        {
            if (addNewTea != null)
            {
                // تحويل الـ DTO إلى الكائن الذي سيتم حفظه في قاعدة البيانات
                var teaEntity = _mapper.Map<ElsaeedTeaProductDetails>(addNewTea);


                // إضافة الكائن إلى Repository
                await _unitOfWork.Repository<ElsaeedTeaProductDetails>().AddAsync(teaEntity);

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

        public async Task<int> DeleteTeaDetails(int id)
        {
            var teaProduct = await _unitOfWork.Repository<ElsaeedTeaProductDetails>().GetByIdAsync(id);
            if (teaProduct != null)
            {
                _unitOfWork.Repository<ElsaeedTeaProductDetails>().Delete(teaProduct);
                var status = await _unitOfWork.CompleteAsync(); //

                if (status == 0)
                {
                    return 0;
                }
                return status;
            }
            return 0;
        }

        public async Task<IReadOnlyList<GetTeaDetailsDto>> GetAllTeaDetails()
        {
            var teaProduct = await _unitOfWork.Repository<ElsaeedTeaProductDetails>().GetAllAsync();
            var mappedTea = _mapper.Map<IReadOnlyList<GetTeaDetailsDto>>(teaProduct);
            return mappedTea;
        }

        public async Task<GetTeaDetailsDto> GetTeaDetailsById(int id)
        {
            var teaProduct = await _unitOfWork.Repository<ElsaeedTeaProductDetails>().GetByIdAsync(id);
            var mappedTea = _mapper.Map<GetTeaDetailsDto>(teaProduct);
            return mappedTea;
        }



        public async Task<GetTeaDetailsDto> UpdateTeaDetails(int id, AddTeaDetailsDto addNewTea)
        {
            var productTea = await _unitOfWork.Repository<ElsaeedTeaProductDetails>().GetByIdAsync(id);

            if (productTea != null)
            {
                // استخدام AutoMapper لتحويل الـ DTO إلى الكائن الفعلي (ElsaeedTeaProduct)
                var mappedTea = _mapper.Map<ElsaeedTeaProductDetails>(addNewTea);

                // تحديث الكائن بالبيانات الجديدة

                productTea.Price = mappedTea.Price;
                productTea.Weight = mappedTea.Weight;

                // تحديث الكائن في الـ Repository
                _unitOfWork.Repository<ElsaeedTeaProductDetails>().Update(productTea);

                // حفظ التغييرات في قاعدة البيانات
                var status = await _unitOfWork.CompleteAsync();

                if (status == 0)
                {
                    return null;
                }

                var mapToDto = new GetTeaDetailsDto()
                {
                    Id = id,

                    Price = mappedTea.Price,
                    Weight = mappedTea.Weight,
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

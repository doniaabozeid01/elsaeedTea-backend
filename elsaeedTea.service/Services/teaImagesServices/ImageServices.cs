using AutoMapper;
using elsaeedTea.data.Entities;
using elsaeedTea.repository.Interfaces;
using elsaeedTea.service.Services.teaImagesServices.Dtos;

namespace elsaeedTea.service.Services.teaImagesServices
{
    public class ImageServices : IImageServices
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AddTeaImages> AddTeaImages(AddTeaImages teaImageDto, string path)
        {
            if (teaImageDto != null)
            {
                var teaEntity = new ElsaeedTeaProductImage
                {
                    ImageUrl = path,
                    ProductId = teaImageDto.ProductId
                }; ;

                await _unitOfWork.Repository<ElsaeedTeaProductImage>().AddAsync(teaEntity);

                var status = await _unitOfWork.CompleteAsync();

                if( status == 0 )
                {
                    return null;
                }
          

                return teaImageDto; // أو يمكن إنشاء AddNewTeaDto جديد من الكائن الذي تم حفظه
            }

            return null; // في حالة لم يتم تمرير تفاصيل الشاي
        }

        public async Task<int> DeleteImage (int id)
        {
            var teaImage = await _unitOfWork.Repository<ElsaeedTeaProductImage>().GetByIdAsync(id);
            if (teaImage != null)
            {
                _unitOfWork.Repository<ElsaeedTeaProductImage>().Delete(teaImage);
                var status = await _unitOfWork.CompleteAsync();

                if (status == 0 )
                {
                    return 0;
                }
                else
                {
                    return status;
                }
            }

            return 0;

        }

        public async Task<IReadOnlyList<GetTeaImages>> GetAllTeaImages()
        {
            var teaImage = await _unitOfWork.Repository<ElsaeedTeaProductImage>().GetAllAsync();
            var mappedTeaImage = _mapper.Map<IReadOnlyList<GetTeaImages>>(teaImage);
            return mappedTeaImage;
        }

        public async Task<GetTeaImages> GetTeaImagesById(int id)
        {
            var teaImage = await _unitOfWork.Repository<ElsaeedTeaProductImage>().GetByIdAsync(id);
            var mappedTeaImage = _mapper.Map<GetTeaImages>(teaImage);
            return mappedTeaImage;
        }

        public async Task<GetTeaImages> UpdateTeaImages(int id, GetTeaImages teaImageDto)
        {
            var teaImage = await _unitOfWork.Repository<ElsaeedTeaProductImage>().GetByIdAsync(id);

            if (teaImage != null)
            {
                var mappedTea = _mapper.Map<ElsaeedTeaProductImage>(teaImageDto);

                teaImage.ProductId = mappedTea.ProductId;
                teaImage.ImageUrl = mappedTea.ImageUrl;

                _unitOfWork.Repository<ElsaeedTeaProductImage>().Update(teaImage);

                var status = await _unitOfWork.CompleteAsync();

                if(status == 0)
                {
                    return null;
                }

                var mapToDto = new GetTeaImages()
                {
                    Id = id,
                    ProductId = mappedTea.ProductId,
                    ImageUrl = mappedTea.ImageUrl,
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

using AutoMapper;
using elsaeedTea.data.Entities;
using elsaeedTea.repository.Interfaces;
using elsaeedTea.service.Services.CartServices.Dtos;

namespace elsaeedTea.service.Services.CartServices
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AddCart> AddCart(AddCart cartDto)
        {
            if (cartDto != null)
            {
                // تحويل الـ DTO إلى الكائن الذي سيتم حفظه في قاعدة البيانات
                var cartEntity = _mapper.Map<CartItem>(cartDto);


                // إضافة الكائن إلى Repository
                await _unitOfWork.Repository<CartItem>().AddAsync(cartEntity);

                // حفظ التغييرات في قاعدة البيانات
                var status = await _unitOfWork.CompleteAsync();

                if (status == 0)
                {
                    return null;
                }

                // يمكن العودة بالـ DTO أو الكائن الذي تم إضافته بعد تحويله مرة أخرى إذا لزم الأمر
                return cartDto; // أو يمكن إنشاء AddNewTeaDto جديد من الكائن الذي تم حفظه
            }

            return null; // في حالة لم يتم تمرير تفاصيل الشاي
        }

        public async Task<int> DeleteCart(int id)
        {
            var cart = await _unitOfWork.Repository<CartItem>().GetByIdAsync(id);
            if (cart != null)
            {
                _unitOfWork.Repository<CartItem>().Delete(cart);
                var status = await _unitOfWork.CompleteAsync(); //

                if (status == 0)
                {
                    return 0;
                }
                return status;
            }
            return 0;
        }

        public async Task<IReadOnlyList<GetCart>> GetAllCarts()
        {
            var cart = await _unitOfWork.Repository<CartItem>().GetAllCartsAsync();
            var mappedcart = _mapper.Map<IReadOnlyList<GetCart>>(cart);
            return mappedcart;
        }

        public async Task<GetCart> GetCartById(int id)
        {
            var cart = await _unitOfWork.Repository<CartItem>().GetCartByIdAsync(id);
            var mappedcart = _mapper.Map<GetCart>(cart);
            return mappedcart;
        }



        public async Task<GetCart> GetCartByIdWithoutInclude(int id)
        {
            var cart = await _unitOfWork.Repository<CartItem>().GetByIdAsync(id);
            var mappedcart = _mapper.Map<GetCart>(cart);
            return mappedcart;
        }




        // بتمسح بدل ما تعدل
        public async Task<GetCart> UpdateCart(int id, AddCart cartDto)
        {
            var cart = await _unitOfWork.Repository<CartItem>().GetByIdAsync(id);

            if (cart != null)
            {
                // استخدام AutoMapper لتحويل الـ DTO إلى الكائن الفعلي (ElsaeedTeaProduct)
                var mappedCart = _mapper.Map<CartItem>(cartDto);

                // تحديث الكائن بالبيانات الجديدة
                cart.ProductId = mappedCart.ProductId;
                cart.Quantity = mappedCart.Quantity;
                cart.UserId = mappedCart.UserId;
                cart.Product = mappedCart.Product;
                cart.User = mappedCart.User;

                // تحديث الكائن في الـ Repository
                _unitOfWork.Repository<CartItem>().Update(cart);

                // حفظ التغييرات في قاعدة البيانات
                var status = await _unitOfWork.CompleteAsync();

                if (status == 0)
                {
                    return null;
                }

                var mapToDto = new GetCart()
                {
                    Id = id,
                    ProductId = mappedCart.ProductId,
                    Quantity = mappedCart.Quantity,
                    UserId = mappedCart.UserId,
                    User = mappedCart.User,
                    Product = mappedCart.Product
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

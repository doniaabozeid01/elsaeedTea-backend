using AutoMapper;
using elsaeedTea.data.Entities;
using elsaeedTea.service.Services.CartServices;
using elsaeedTea.service.Services.CartServices.Dtos;
using elsaeedTea.service.Services.Order;
using elsaeedTea.service.Services.Order.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace elsaeedTea.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {


        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public PaymentController(IOrderService orderService, ICartService cartService, IMapper mapper)
        {
            _orderService = orderService;
            _cartService = cartService;
            _mapper = mapper;
        }

        // إنشاء الطلب
        [HttpPost("CreateOrder")]
        public async Task<ActionResult<OrderRequest>> CreateOrder([FromBody] CreateOrderRequest model)
        {
            
            try
            {
                // الحصول على السلة الخاصة بالمستخدم
                var cartItems = await _cartService.GetCartByUserId(model.UserId);
                if (cartItems == null || cartItems.Count == 0)
                    return BadRequest("Cart is empty!");

                var mappedCartItems = _mapper.Map<IReadOnlyList<CartItem>>(cartItems).ToList();

                // إنشاء DTO للطلب
                var orderDto = new addOrder
                {
                    UserId = model.UserId,
                    PaymentMethod = model.PaymentMethod,
                    Country = model.Country,
                    Governorate = model.Governorate,
                    PhoneNumber = model.PhoneNumber,
                    cartItems = mappedCartItems, // تمرير المنتجات من السلة
                    TotalAmount = cartItems.Sum(item => item.Product.Price * item.Quantity), // حساب الإجمالي
                    CreatedAt = DateTime.Now
                };

                // إنشاء الطلب باستخدام الـ DTO
                var order = await _orderService.CreateOrderAsync(orderDto);

                // معالجة العناصر: الإضافة إلى OrderItem والحذف من السلة
                foreach (var item in cartItems)
                {
                    // إضافة إلى OrderItem
                    var orderItem = new OrderItem
                    {
                        OrderRequestId = order.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.Product.Price, // السعر عند الشراء
                        UserId = model.UserId
                    };

                    await _orderService.AddOrderItemAsync(orderItem);

                    // حذف العنصر من السلة
                    await _cartService.DeleteCart(item.Id);
                }

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating order: {ex.Message}");
            }




        }
    
    
    
        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<GetOrderRequest>> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrderRequests();
                if (orders != null)
                {
                    return Ok(orders);
                }
                return NotFound("there are no orders");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }





        [HttpGet("GetOrderById")]
        public async Task<ActionResult<GetOrderRequest>> GetOrderById(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("invalid id");
                }
                var order = await _orderService.GetOrderRequestById(id);
                if (order != null)
                {
                    return Ok(order);
                }
                return NotFound($"there is no order with id {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }



        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(string id)
        {

            try
            {
                if (id == null)
                {
                    return BadRequest("invalid id");
                }
                var order = await _orderService.GetOrderRequestById(id);
                if (order != null)
                {
                    var result = await _orderService.DeleteOrderRequest(id);

                    if (result == 0 && order == null)
                    {
                        return NotFound("No order found.");
                    }
                    else if (result == 0 && order != null)
                    {
                        return NotFound("Failed to delete order from the database .");

                    }

                    // إرسال الاستجابة الناجحة مع البيانات
                    return Ok($"order With Id {id} deleted Successfully");

                }

                return NotFound($"there is no order with id {id}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }


        }





        [HttpGet("GetOrdersByUserId")]
        public async Task<ActionResult<GetOrderRequest>> GetOrdersByUserId(string id)
        {
            try
            {
                if(id == null)
                {
                    return BadRequest("Invalid id");
                }
                var orders = await _orderService.GetAllOrdersByUserId(id);
                if (orders != null)
                {
                    return Ok(orders);
                }
                return NotFound("there are no orders");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }



    }
}


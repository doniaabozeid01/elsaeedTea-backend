using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Entities;
using elsaeedTea.service.Services.Order.Dtos;

namespace elsaeedTea.service.Services.Order
{
    public interface IOrderService
    {
        Task<OrderRequest> CreateOrderAsync(addOrder orderDto);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task<IReadOnlyList<GetOrderRequest>> GetAllOrderRequests();
        Task<GetOrderRequest> GetOrderRequestById(string id);
        Task<int> DeleteOrderRequest(string id);

        Task<IReadOnlyList<GetOrderRequest>> GetAllOrdersByUserId(string id);

        Task<IReadOnlyList<GetOrderItems>> GetOrderItemsByOrderRequestId(string id);


    }
}

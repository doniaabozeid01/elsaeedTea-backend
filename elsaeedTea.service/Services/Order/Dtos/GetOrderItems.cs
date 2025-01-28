using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Entities;

namespace elsaeedTea.service.Services.Order.Dtos
{
    public class GetOrderItems
    {
        public int Id { get; set; } // معرف العنصر
        public string OrderRequestId { get; set; } // معرف الطلب
        public OrderRequest OrderRequest { get; set; } // الطلب المرتبط
        public int ProductDetailsId { get; set; } // معرف المنتج
        public ElsaeedTeaProductDetails ProductDetails { get; set; } // المنتج المرتبط
        public int Quantity { get; set; } // الكمية المطلوبة
        public decimal Price { get; set; } // السعر عند الشراء
        public string UserId { get; set; } // معرف المستخدم
        public ApplicationUser User { get; set; }
    }
}

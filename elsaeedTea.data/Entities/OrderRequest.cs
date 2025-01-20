using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Enum;

namespace elsaeedTea.data.Entities
{
    public class OrderRequest : BaseEntity
    {
        public string Id { get; set; } // معرف الطلب (يجب أن يكون فريد)
        public string UserId { get; set; } // معرف المستخدم المرتبط بالطلب
        public List<CartItem> cartItems { get; set; } // قائمة الأصناف (يمكنك تخزين معلومات الأصناف بشكل منفصل أو على شكل JSON في قاعدة البيانات)
        public string PaymentMethod { get; set; } // طريقة الدفع (مثال: "Cash on Delivery")
        public decimal TotalAmount { get; set; } // إجمالي المبلغ
        public string Country { get; set; } // البلد
        public string Governorate { get; set; } // المحافظة
        public string PhoneNumber { get; set; } // رقم الهاتف
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } =DateTime.Now;
    }
}

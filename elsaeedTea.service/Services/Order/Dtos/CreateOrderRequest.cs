using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elsaeedTea.service.Services.Order.Dtos
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; } // معرف المستخدم
        public string PaymentMethod { get; set; } // طريقة الدفع
        public string Country { get; set; } // البلد
        public string City { get; set; } // المحافظة
        public string Address { get; set; } // المحافظة
        public string PhoneNumber { get; set; } // رقم الهاتف
    }
}

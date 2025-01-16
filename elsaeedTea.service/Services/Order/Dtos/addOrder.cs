﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Entities;

namespace elsaeedTea.service.Services.Order.Dtos
{
    public class addOrder
    {
        public string UserId { get; set; } // معرف المستخدم المرتبط بالطلب
        public List<CartItem> cartItems { get; set; } // قائمة الأصناف
        public string PaymentMethod { get; set; } // طريقة الدفع (مثال: "Cash on Delivery")
        public decimal TotalAmount { get; set; } // إجمالي المبلغ
        public string Country { get; set; } // البلد
        public string Governorate { get; set; } // المحافظة
        public string PhoneNumber { get; set; } // رقم الهاتف
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    }
}
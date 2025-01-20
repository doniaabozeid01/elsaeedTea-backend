using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elsaeedTea.data.Enum
{
    public enum OrderStatus
    {
        Pending,     // الطلب قيد المراجعة
        Confirmed,   // تم تأكيد الطلب
        Shipped,     // تم شحن الطلب
        Delivered,   // تم تسليم الطلب
        Cancelled    // تم إلغاء الطلب
    }

}

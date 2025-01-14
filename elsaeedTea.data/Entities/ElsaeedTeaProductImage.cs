using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace elsaeedTea.data.Entities
{
    public class ElsaeedTeaProductImage : BaseEntity
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; } // رابط الصورة
        public int ProductId { get; set; } // المفتاح الأجنبي
        [JsonIgnore] // تجاهل هذه الخاصية عند تحويل الكائن إلى JSON
        public ElsaeedTeaProduct? Product { get; set; } // العلاقة مع المنتج
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Entities;
using Microsoft.AspNetCore.Http;

namespace elsaeedTea.service.Services.teaImagesServices.Dtos
{
    public class GetTeaImages
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; } // رابط الصورة
        public int ProductId { get; set; } // المفتاح الأجنبي
        //public ElsaeedTeaProduct? Product { get; set; } // العلاقة مع المنتج
    }
}

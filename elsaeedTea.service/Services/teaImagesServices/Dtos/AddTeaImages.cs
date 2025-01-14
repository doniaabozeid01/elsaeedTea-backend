using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Entities;
using Microsoft.AspNetCore.Http;

namespace elsaeedTea.service.Services.teaImagesServices.Dtos
{
    public class AddTeaImages
    {
        //public string? ImageUrl { get; set; } // رابط الصورة
        public int ProductId { get; set; } // المفتاح الأجنبي

        public IFormFile? ImageFile { get; set; } // الصورة كملف

    }
}

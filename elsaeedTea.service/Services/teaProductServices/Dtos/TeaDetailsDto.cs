using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Entities;

namespace elsaeedTea.service.Services.teaProductServices.Dtos
{
    public class TeaDetailsDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } // ابتاي 
        public string? Description { get; set; } // ليليالااي 
        public decimal Price { get; set; }  // 1 dinar
        public decimal Weight { get; set; } // 300 gram
        public ICollection<ElsaeedTeaProductImage> Images { get; set; } = new List<ElsaeedTeaProductImage>();
    }
}

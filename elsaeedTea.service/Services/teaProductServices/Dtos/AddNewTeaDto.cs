using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elsaeedTea.service.Services.teaProductServices.Dtos
{
    public class AddNewTeaDto
    {
        public string? Name { get; set; } // ابتاي 
        public string? Description { get; set; } // ليليالااي 
        public decimal Price { get; set; }  // 1 dinar
        public decimal Weight { get; set; } // 300 gram
    }
}

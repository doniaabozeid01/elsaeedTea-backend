using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Entities;

namespace elsaeedTea.service.Services.TeaDetailsServices.Dtos
{
    public class GetTeaDetailsDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public ElsaeedTeaProduct Product { get; set; }
        public decimal Price { get; set; }  // 1 dinar
        public decimal Weight { get; set; } // 300 gram
    }
}

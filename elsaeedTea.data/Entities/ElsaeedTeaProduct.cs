using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elsaeedTea.data.Entities
{
    public class ElsaeedTeaProduct : BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string? Name { get; set; } // ابتاي 
        [MaxLength(1000)]
        public string? Description { get; set; } // ليليالااي 
        public decimal Price { get; set; }  // 1 dinar
        public decimal Weight { get; set; } // 300 gram
        public ICollection<ElsaeedTeaProductImage> Images { get; set; } = new List<ElsaeedTeaProductImage>();

    }
}

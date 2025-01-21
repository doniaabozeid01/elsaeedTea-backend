using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace elsaeedTea.data.Entities
{
    public class ElsaeedTeaProductDetails : BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore]
        public ElsaeedTeaProduct Product { get; set; }
        public decimal Price { get; set; }  // 1 dinar
        public decimal Weight { get; set; } // 300 gram
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elsaeedTea.data.Entities
{
    public class ProductReviews : BaseEntity
    {
        public int Id { get; set; }
        public byte? Rating { get; set; } = 5;
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }= DateTime.Now;
        public DateTime? UpdatedAt { get; set; }= DateTime.Now;
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public ElsaeedTeaProduct Product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elsaeedTea.data.Entities
{
    public class CartItem : BaseEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public ElsaeedTeaProduct Product { get; set; }
        public string? OrderRequestId { get; set; } 
        public OrderRequest? OrderRequest { get; set; } // علاقة مع الكائن OrderRequest

        public int Quantity { get; set; }

        // تاريخ الإضافة (اختياري)
        //public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Entities;

namespace elsaeedTea.service.Services.CartServices.Dtos
{
    public class GetCart
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ProductDetailsId { get; set; }
        public ElsaeedTeaProductDetails ProductDetails { get; set; }
        public int Quantity { get; set; }
    }
}

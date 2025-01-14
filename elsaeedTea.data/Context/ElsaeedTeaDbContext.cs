using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using elsaeedTea.data.Entities;
using Microsoft.EntityFrameworkCore;

namespace elsaeedTea.data.Context
{
    public class ElsaeedTeaDbContext : DbContext
    {
        public ElsaeedTeaDbContext(DbContextOptions<ElsaeedTeaDbContext> options) : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // تحديد العلاقة بين ElsaeedTeaProduct و ElsaeedTeaProductImage
        //    modelBuilder.Entity<ElsaeedTeaProductImage>()
        //        .HasOne(x => x.Product)         // العلاقة مع المنتج
        //        .WithMany()                     // المنتج يمكن أن يكون له العديد من الصور
        //        .HasForeignKey(x => x.ProductId) // المفتاح الأجنبي في الصورة
        //        .OnDelete(DeleteBehavior.Cascade); // يمكنك تحديد نوع السلوك عند الحذف إذا أردت
        //}


        public DbSet<ElsaeedTeaProduct> TeaProduct { get; set; }
        public DbSet<ElsaeedTeaProductImage> TeaProductImages { get; set; }
    }
}

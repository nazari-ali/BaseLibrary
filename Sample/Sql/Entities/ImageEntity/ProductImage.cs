using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.ProductEntity;
using System;

namespace Sample.Sql.Entities.ImageEntity
{
    public class ProductImage : Image
    {
        public Guid? ProductId { get; set; }

        public Product Product { get; set; }
    }

    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasOne(p => p.Product).WithMany(c => c.Images).HasForeignKey(p => p.ProductId);
        }
    }
}

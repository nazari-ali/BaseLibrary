using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.ProductEntity;
using System;

namespace Sample.Sql.Entities.TagEntity
{
    public class ProductTag : Tag
    {
        public Guid? ProductId { get; set; }

        public Product Product { get; set; }
    }

    public class ProductTagConfiguration : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder.HasOne(p => p.Product).WithMany(c => c.Tags).HasForeignKey(p => p.ProductId);
        }
    }
}

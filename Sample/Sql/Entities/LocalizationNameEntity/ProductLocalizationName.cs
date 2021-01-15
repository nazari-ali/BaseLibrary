using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.ProductEntity;
using System;

namespace Sample.Sql.Entities.LocalizationNameEntity
{
    public class ProductLocalizationName : LocalizationName
    {
        public Guid? ProductId { get; set; }

        public Product Product { get; set; }
    }

    public class ProductLocalizationNameConfiguration : IEntityTypeConfiguration<ProductLocalizationName>
    {
        public void Configure(EntityTypeBuilder<ProductLocalizationName> builder)
        {
            builder.HasOne(p => p.Product).WithMany(c => c.LocalizationNames).HasForeignKey(p => p.ProductId);
        }
    }
}

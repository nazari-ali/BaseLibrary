using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.ProductEntity;
using System;

namespace Sample.Sql.Entities.GenreEntity
{
    public class ProductGenre : GenreItem
    {
        public Guid? ProductId { get; set; }
        public Product Product { get; set; }
    }

    public class ProductGenreConfiguration : IEntityTypeConfiguration<ProductGenre>
    {
        public void Configure(EntityTypeBuilder<ProductGenre> builder)
        {
            builder.HasOne(p => p.Product).WithMany(c => c.Genres).HasForeignKey(p => p.ProductId);
        }
    }
}

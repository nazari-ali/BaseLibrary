using BaseLibrary.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.ArtistEntity;
using Sample.Sql.Entities.ProductEntity;
using System;

namespace Sample.Sql.Entities.CommonEntity
{
    public class ProductArtist : SqlEntity
    {
        public Guid ProductId { get; set; }
        public Guid ArtistId { get; set; }

        public Product Product { get; set; }
        public Artist Artist { get; set; }
    }

    public class ProductArtistConfiguration : IEntityTypeConfiguration<ProductArtist>
    {
        public void Configure(EntityTypeBuilder<ProductArtist> builder)
        {
            builder.HasOne(p => p.Product).WithMany(c => c.ProductArtists).HasForeignKey(p => p.ProductId);
            builder.HasOne(p => p.Artist).WithMany(c => c.ProductArtists).HasForeignKey(p => p.ArtistId);
        }
    }
}

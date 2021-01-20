using BaseLibrary.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.Enums;
using System;

namespace Sample.Sql.Entities.ProductEntity
{
    public class Quality : SqlEntity
    {
        public string Name { get; set; }
        public FileType FileType { get; set; }
        public decimal Size { get; set; }
        public string Extension { get; set; }
        public QualityType QualityType { get; set; }
        public Guid ProductId { get; set; }

        public Media Media { get; set; }
    }

    public class ProductFileConfiguration : IEntityTypeConfiguration<Quality>
    {
        public void Configure(EntityTypeBuilder<Quality> builder)
        {
            builder.HasOne(p => p.Media).WithMany(c => c.Qualities).HasForeignKey(p => p.ProductId);

            builder.Property(p => p.Name)
                .HasMaxLength(200);

            builder.Property(p => p.Extension)
                .HasMaxLength(10);
        }
    }
}

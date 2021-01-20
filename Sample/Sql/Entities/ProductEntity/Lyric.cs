using BaseLibrary.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.Enums;
using System;

namespace Sample.Sql.Entities.ProductEntity
{
    public class Lyric : SqlEntity
    {
        public LanguageType Type { get; set; }
        public string Text { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public Guid ProductId { get; set; }

        public Media Media { get; set; }
    }

    public class ProductLyricConfiguration : IEntityTypeConfiguration<Lyric>
    {
        public void Configure(EntityTypeBuilder<Lyric> builder)
        {
            builder.HasOne(p => p.Media).WithMany(c => c.Lyrics).HasForeignKey(p => p.ProductId);
        }
    }
}

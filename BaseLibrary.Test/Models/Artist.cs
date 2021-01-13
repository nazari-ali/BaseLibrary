using BaseLibrary.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BaseLibrary.Test.Models
{
    public class Artist : SqlEntity
    {
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public bool IsCertificate { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateAdd { get; set; }
    }

    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.Property(p => p.Id).IsRequired().HasDefaultValue(Guid.NewGuid());
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(200);
            builder.Property(p => p.DateAdd).IsRequired().HasDefaultValue(DateTime.UtcNow);
            builder.Property(p => p.Gender).IsRequired();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.ArtistEntity;
using System;

namespace Sample.Sql.Entities.ImageEntity
{
    public class ArtistImage : Image
    {
        public Guid? ArtistId { get; set; }

        public Artist Artist { get; set; }
    }

    public class ArtistImageConfiguration : IEntityTypeConfiguration<ArtistImage>
    {
        public void Configure(EntityTypeBuilder<ArtistImage> builder)
        {
            builder.HasOne(p => p.Artist).WithMany(c => c.Images).HasForeignKey(p => p.ArtistId);
        }
    }
}
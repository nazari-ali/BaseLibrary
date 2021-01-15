using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.ArtistEntity;
using System;

namespace Sample.Sql.Entities.TagEntity
{
    public class ArtistTag : Tag
    {
        public Guid? ArtistId { get; set; }

        public Artist Artist { get; set; }
    }

    public class ArtistTagConfiguration : IEntityTypeConfiguration<ArtistTag>
    {
        public void Configure(EntityTypeBuilder<ArtistTag> builder)
        {
            builder.HasOne(p => p.Artist).WithMany(c => c.Tags).HasForeignKey(p => p.ArtistId);
        }
    }
}
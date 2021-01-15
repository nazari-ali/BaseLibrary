using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.ArtistEntity;
using System;

namespace Sample.Sql.Entities.LocalizationNameEntity
{
    public class ArtistLocalizationName : LocalizationName
    {
        public Guid? ArtistId { get; set; }

        public Artist Artist { get; set; }
    }

    public class ArtistLocalizationNameConfiguration : IEntityTypeConfiguration<ArtistLocalizationName>
    {
        public void Configure(EntityTypeBuilder<ArtistLocalizationName> builder)
        {
            builder.HasOne(p => p.Artist).WithMany(c => c.LocalizationNames).HasForeignKey(p => p.ArtistId);
        }
    }
}
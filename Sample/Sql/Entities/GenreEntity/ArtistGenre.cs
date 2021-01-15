using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.ArtistEntity;
using System;

namespace Sample.Sql.Entities.GenreEntity
{
    public class ArtistGenre : GenreItem
    {
        public Guid? ArtistId { get; set; }
        public Artist Artist { get; set; }
    }

    public class ArtistGenreConfiguration : IEntityTypeConfiguration<ArtistGenre>
    {
        public void Configure(EntityTypeBuilder<ArtistGenre> builder)
        {
            builder.HasOne(p => p.Artist).WithMany(c => c.Genres).HasForeignKey(p => p.ArtistId);
        }
    }
}
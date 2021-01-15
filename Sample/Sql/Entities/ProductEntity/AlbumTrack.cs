using BaseLibrary.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Sample.Sql.Entities.ProductEntity
{
    public class AlbumTrack : SqlEntity
    {
        public Guid AlbumId { get; set; }
        public Guid TrackId { get; set; }

        public Album Album { get; set; }
        public Media Media { get; set; }
    }

    public class AlbumTrackConfiguration : IEntityTypeConfiguration<AlbumTrack>
    {
        public void Configure(EntityTypeBuilder<AlbumTrack> builder)
        {
            builder.HasOne(p => p.Album).WithMany(c => c.AlbumTracks).HasForeignKey(p => p.AlbumId);
            builder.HasOne(p => p.Media).WithMany(c => c.AlbumTracks).HasForeignKey(p => p.TrackId);
        }
    }
}

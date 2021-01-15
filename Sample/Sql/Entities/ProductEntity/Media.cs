using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Sample.Sql.Entities.ProductEntity
{
    public class Media : Product
    {
        public TimeSpan Duration { get; set; }

        public ICollection<Quality> Qualities { get; set; }
        public ICollection<Lyric> Lyrics { get; set; }
    }

    public class MediaConfiguration : IEntityTypeConfiguration<Media>
    {
        public void Configure(EntityTypeBuilder<Media> builder)
        {

        }
    }
}
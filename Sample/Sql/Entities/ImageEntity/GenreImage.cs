using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.GenreEntity;
using System;

namespace Sample.Sql.Entities.ImageEntity
{
    public class GenreImage : Image
    {
        public Guid? GenreId { get; set; }

        public Genre Genre { get; set; }
    }

    public class GenreImageConfiguration : IEntityTypeConfiguration<GenreImage>
    {
        public void Configure(EntityTypeBuilder<GenreImage> builder)
        {
            builder.HasOne(p => p.Genre).WithMany(c => c.Images).HasForeignKey(p => p.GenreId);
        }
    }
}

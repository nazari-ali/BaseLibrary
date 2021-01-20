using BaseLibrary.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.Enums.Discriminators;
using System;

namespace Sample.Sql.Entities.GenreEntity
{
    public abstract class GenreItem : SqlEntity
    {
        public Guid GenreId { get; set; }
        public Genre Genre { get; set; }
    }

    public class GenreItemConfiguration : IEntityTypeConfiguration<GenreItem>
    {
        public void Configure(EntityTypeBuilder<GenreItem> builder)
        {
            builder.HasDiscriminator<GenreDiscriminator>("Discriminator")
                .HasValue<ProductGenre>(GenreDiscriminator.Product)
                .HasValue<ArtistGenre>(GenreDiscriminator.Artist);

            builder.HasOne(p => p.Genre).WithMany(c => c.GenreItems).HasForeignKey(p => p.GenreId);
        }
    }
}

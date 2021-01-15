using BaseLibrary.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.Enums;
using Sample.Sql.Entities.Enums.Discriminators;

namespace Sample.Sql.Entities.ImageEntity
{
    public abstract class Image : SqlEntity
    {
        public string Name { get; set; }
        public ImageSize ImageSize { get; set; }
    }

    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasDiscriminator<ImageDiscriminator>("Discriminator")
                .HasValue<ProductImage>(ImageDiscriminator.Product)
                .HasValue<ArtistImage>(ImageDiscriminator.Artist)
                .HasValue<GenreImage>(ImageDiscriminator.Genre);

            builder.Property(p => p.Name)
                .HasMaxLength(200);
        }
    }
}

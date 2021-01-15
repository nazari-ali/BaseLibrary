using BaseLibrary.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.Enums;
using Sample.Sql.Entities.Enums.Discriminators;

namespace Sample.Sql.Entities.LocalizationNameEntity
{
    public abstract class LocalizationName : SqlEntity
    {
        public string Title { get; set; }
        public LanguageType LanguageType { get; set; }
    }

    public class LocalizationNameConfiguration : IEntityTypeConfiguration<LocalizationName>
    {
        public void Configure(EntityTypeBuilder<LocalizationName> builder)
        {
            builder.HasDiscriminator<LocalizationNameDiscriminator>("Discriminator")
                .HasValue<ProductLocalizationName>(LocalizationNameDiscriminator.Product)
                .HasValue<ArtistLocalizationName>(LocalizationNameDiscriminator.Artist)
                .HasValue<GenreLocalizationName>(LocalizationNameDiscriminator.Genre);

            builder.Property(p => p.Title)
                .HasMaxLength(200);
        }
    }
}

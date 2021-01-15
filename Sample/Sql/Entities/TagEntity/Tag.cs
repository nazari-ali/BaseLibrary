using BaseLibrary.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.Enums.Discriminators;

namespace Sample.Sql.Entities.TagEntity
{
    public abstract class Tag : SqlEntity
    {
        public string Text { get; set; }
    }

    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasDiscriminator<TagDiscriminator>("Discriminator")
                .HasValue<ProductTag>(TagDiscriminator.Product)
                .HasValue<ArtistTag>(TagDiscriminator.Artist);

            builder.Property(p => p.Text)
                .HasMaxLength(200);
        }
    }
}

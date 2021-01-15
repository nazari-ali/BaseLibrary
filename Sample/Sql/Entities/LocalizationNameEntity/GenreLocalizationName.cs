using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.GenreEntity;
using System;

namespace Sample.Sql.Entities.LocalizationNameEntity
{
    public class GenreLocalizationName : LocalizationName
    {
        public Guid? GenreId { get; set; }

        public Genre Genre { get; set; }
    }

    public class GenreLocalizationNameConfiguration : IEntityTypeConfiguration<GenreLocalizationName>
    {
        public void Configure(EntityTypeBuilder<GenreLocalizationName> builder)
        {
            builder.HasOne(p => p.Genre).WithMany(c => c.LocalizationNames).HasForeignKey(p => p.GenreId);
        }
    }
}

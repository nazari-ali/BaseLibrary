using BaseLibrary.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.ImageEntity;
using Sample.Sql.Entities.LocalizationNameEntity;
using System;
using System.Collections.Generic;

namespace Sample.Sql.Entities.GenreEntity
{
    public class Genre : SqlEntity
    {
        public DateTime DateOfRegistration { get; set; }
        public DateTime? LastModifyDate { get; set; }

        public ICollection<GenreLocalizationName> LocalizationNames { get; set; }
        public ICollection<GenreImage> Images { get; set; }
        public ICollection<GenreItem> GenreItems { get; set; }
    }

    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            
        }
    }
}

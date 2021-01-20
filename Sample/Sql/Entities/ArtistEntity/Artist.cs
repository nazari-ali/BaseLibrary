using BaseLibrary.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.CommonEntity;
using Sample.Sql.Entities.GenreEntity;
using Sample.Sql.Entities.ImageEntity;
using Sample.Sql.Entities.LocalizationNameEntity;
using Sample.Sql.Entities.TagEntity;
using System;
using System.Collections.Generic;

namespace Sample.Sql.Entities.ArtistEntity
{
    public class Artist : SqlEntity
    {
        public DateTime DateOfRegistration { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public string Biography { get; set; }


        public ICollection<ArtistLocalizationName> LocalizationNames { get; set; }
        public ICollection<ProductArtist> ProductArtists { get; set; }
        public ICollection<ArtistImage> Images { get; set; }
        public ICollection<ArtistGenre> Genres { get; set; }
        public ICollection<ArtistTag> Tags { get; set; }
    }

    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            
        }
    }
}

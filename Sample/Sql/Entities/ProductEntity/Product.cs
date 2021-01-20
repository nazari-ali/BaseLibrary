using BaseLibrary.Sql.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Sql.Entities.CommonEntity;
using Sample.Sql.Entities.Enums.Discriminators;
using Sample.Sql.Entities.GenreEntity;
using Sample.Sql.Entities.ImageEntity;
using Sample.Sql.Entities.LocalizationNameEntity;
using Sample.Sql.Entities.TagEntity;
using System;
using System.Collections.Generic;

namespace Sample.Sql.Entities.ProductEntity
{
    public abstract class Product : SqlEntity
    {
        public DateTime DateOfRegistration { get; set; }
        public DateTime? LastModifyDate { get; set; }

        public ICollection<ProductLocalizationName> LocalizationNames { get; set; }
        public ICollection<ProductArtist> ProductArtists { get; set; }
        public ICollection<AlbumTrack> AlbumTracks { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductGenre> Genres { get; set; }
        public ICollection<ProductTag> Tags { get; set; }
    }

    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasDiscriminator<ProductDiscriminator>("Discriminator")
                .HasValue<Album>(ProductDiscriminator.Media)
                .HasValue<Media>(ProductDiscriminator.Album);
        }
    }
}
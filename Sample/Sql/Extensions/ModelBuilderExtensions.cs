using Bogus;
using Microsoft.EntityFrameworkCore;
using Sample.Sql.Entities.Enums;
using Sample.Sql.Entities.GenreEntity;
using Sample.Sql.Entities.ImageEntity;
using Sample.Sql.Entities.LocalizationNameEntity;
using System;

namespace Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            for (int i = 0; i < 100; i++)
            {
                var genreId = Guid.NewGuid();

                var genre = new Faker<Genre>()
                    .RuleFor(g => g.Id, genreId)
                    .RuleFor(g => g.DateOfRegistration, f => f.Date.Past())
                    .RuleFor(g => g.LastModifyDate, f => f.Date.Soon())
                    .Generate(1);

                modelBuilder.Entity<Genre>().HasData(
                    genre
                );

                var genreLocalizeNames = new Faker<GenreLocalizationName>()
                    .RuleFor(gl => gl.GenreId, genreId)
                    .RuleFor(gl => gl.LanguageType, f => f.PickRandomWithout<LanguageType>())
                    .RuleFor(gl => gl.Title, f => f.Music.Genre())
                    .Generate(3);

                modelBuilder.Entity<GenreLocalizationName>().HasData(
                    genreLocalizeNames
                );

                var genreImages = new Faker<GenreImage>()
                    .RuleFor(gi => gi.GenreId, genreId)
                    .RuleFor(gi => gi.ImageSize, f => f.PickRandomWithout<ImageSize>())
                    .RuleFor(gi => gi.Name, f => f.Internet.Avatar())
                    .Generate(4);

                modelBuilder.Entity<GenreImage>().HasData(
                    genreImages
                );
            }
        }
    }
}
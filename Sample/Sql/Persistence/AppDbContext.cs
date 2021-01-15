using BaseLibrary.Sql;
using Microsoft.EntityFrameworkCore;
using Persistence.Extensions;
using Sample.Sql.Entities.GenreEntity;
using Sample.Sql.Entities.ImageEntity;
using Sample.Sql.Entities.LocalizationNameEntity;
using Sample.Sql.Entities.ProductEntity;
using Sample.Sql.Entities.TagEntity;

namespace Sample.Sql.Persistence
{
    public class AppDbContext : SqlDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options, typeof(Product).Assembly)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<LocalizationName>();
            modelBuilder.Entity<Image>();
            modelBuilder.Entity<Tag>();
            modelBuilder.Entity<GenreItem>();

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
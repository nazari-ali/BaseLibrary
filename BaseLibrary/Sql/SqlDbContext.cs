using BaseLibrary.Extensions;
using BaseLibrary.Sql.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLibrary.Sql
{
    public class SqlDbContext : DbContext
    {
        private readonly Assembly[] _assemblies;

        /// <summary>
        /// Using in internal project
        /// </summary>
        /// <param name="options"></param>
        internal SqlDbContext(DbContextOptions options) 
            : base(options)
        {
            _assemblies = new Assembly[] { typeof(ISqlEntity).Assembly };
        }

        /// <summary>
        /// User in external project
        /// </summary>
        /// <param name="options"></param>
        /// <param name="assemblies"></param>
        public SqlDbContext(DbContextOptions options, params Assembly[] assemblies) 
            : base(options)
        {
            _assemblies = assemblies;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.RegisterAllEntities<ISqlEntity>(_assemblies);
            modelBuilder.RegisterEntityTypeConfiguration(_assemblies);
            modelBuilder.AddRestrictDeleteBehaviorConvention();
            modelBuilder.AddSequentialGuidForIdConvention();
            modelBuilder.AddPluralizingTableNameConvention();
        }

        public override int SaveChanges()
        {
            CleanString();
            return base.SaveChanges();
        }

        public override int SaveChanges(
            bool acceptAllChangesOnSuccess
        )
        {
            CleanString();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess, 
            CancellationToken cancellationToken = default
        )
        {
            CleanString();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default
        )
        {
            CleanString();
            return base.SaveChangesAsync(cancellationToken);
        }

        #region Helper

        private void CleanString()
        {
            var changedEntities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var item in changedEntities)
            {
                if (item.Entity == null)
                    continue;

                var properties = item.Entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

                foreach (var property in properties)
                {
                    var propName = property.Name;
                    var val = (string)property.GetValue(item.Entity, null);

                    if (val.HasValue())
                    {
                        var newVal = val.ToEnglishNumber().FixPersianChars();
                        if (newVal == val)
                            continue;

                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }

        #endregion
    }
}
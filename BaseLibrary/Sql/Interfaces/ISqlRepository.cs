using System.Linq;

namespace BaseLibrary.Sql.Interfaces
{
    public interface ISqlRepository<TEntity> : ISqlRepositorySynchronously<TEntity>, ISqlRepositoryAsynchronously<TEntity>, ISqlEntity where TEntity : class
    {
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }
    }
}
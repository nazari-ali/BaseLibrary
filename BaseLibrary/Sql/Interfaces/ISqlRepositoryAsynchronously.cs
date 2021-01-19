using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLibrary.Sql.Interfaces
{
    public interface ISqlRepositoryAsynchronously<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            CancellationToken cancellationToken = default
        );

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes
        );

        Task<IEnumerable<TEntity>> GetAllAsync(
            CancellationToken cancellationToken = default
        );

        Task<TEntity> SingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default
        );

        Task<TEntity> FirstOrDefaultAsync(
            CancellationToken cancellationToken = default
        );

        Task<TEntity> GetByIdAsync(
            CancellationToken cancellationToken = default, 
            params object[] ids
        );

        Task AddAsync(
            TEntity entity, 
            CancellationToken cancellationToken = default
        );

        Task AddRangeAsync(
            IEnumerable<TEntity> entities, 
            CancellationToken cancellationToken = default
        );

        Task LoadCollectionAsync<TProperty>(
            TEntity entity, 
            Expression<Func<TEntity, 
            IEnumerable<TProperty>>> collectionProperty, 
            CancellationToken cancellationToken = default
        ) where TProperty : class;

        Task LoadReferenceAsync<TProperty>(
            TEntity entity, 
            Expression<Func<TEntity, TProperty>> referenceProperty, 
            CancellationToken cancellationToken = default
        ) where TProperty : class;
    }
}
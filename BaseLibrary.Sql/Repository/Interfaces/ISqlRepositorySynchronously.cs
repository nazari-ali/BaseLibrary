using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BaseLibrary.Sql.Repository.Interfaces
{
    public interface ISqlRepositorySynchronously<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
        );

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes
        );

        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> Find(
            Expression<Func<TEntity, bool>> predicate
        );

        TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> predicate
        );

        TEntity FirstOrDefault();

        TEntity GetById(
            params object[] ids
        );

        void Add(
            TEntity entity
        );

        void AddRange(
            IEnumerable<TEntity> entities
        );

        void Update(
            TEntity entity
        );

        void UpdateRange(
            IEnumerable<TEntity> entities
        );

        void Delete(
            TEntity entity
        );

        void DeleteRange(
            IEnumerable<TEntity> entities
        );

        void Attach(
            TEntity entity
        );

        void Detach(
            TEntity entity
        );

        void LoadCollection<TProperty>(
            TEntity entity, 
            Expression<Func<TEntity, 
            IEnumerable<TProperty>>> collectionProperty
        ) where TProperty : class;

        void LoadReference<TProperty>(
            TEntity entity, 
            Expression<Func<TEntity, TProperty>> referenceProperty
        ) where TProperty : class;
    }
}
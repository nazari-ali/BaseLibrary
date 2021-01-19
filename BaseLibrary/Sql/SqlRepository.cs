using BaseLibrary.Extensions;
using BaseLibrary.Sql.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BaseLibrary.Sql
{
    public class SqlRepository<TEntity> : ISqlRepository<TEntity> 
        where TEntity : class
    {
        protected DbSet<TEntity> Entities;
        protected IQueryable<TEntity> Table => Entities;
        protected IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        private readonly DbContext _dbContext;

        public SqlRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            Entities = _dbContext.Set<TEntity>();
        }

        #region IRepositorySynchronously

        /// <summary>  
        /// Initializes a new instance of the <see cref="SqlRepository{TEntity}"/> class.  
        /// Note that here I've stored Context.Set<TEntity>() in the constructor and store it in a private field like _entities.   
        /// This way, the implementation  of our methods would be cleaner:  
        /// _entities.ToList();  
        /// _entities.Where();  
        /// _entities.SingleOrDefault();  
        /// </summary>  
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = ""
        )
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="SqlRepository{TEntity}"/> class.  
        /// Note that here I've stored Context.Set<TEntity>() in the constructor and store it in a private field like _entities.   
        /// This way, the implementation  of our methods would be cleaner:  
        /// _entities.ToList();  
        /// _entities.Where();  
        /// _entities.SingleOrDefault();  
        /// </summary>  
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            params Expression<Func<TEntity, object>>[] includes
        )
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if(includes.Any())
            {
                query = query.IncludeMultiple(includes);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>  
        /// Gets all.  
        /// </summary>  
        /// <returns></returns>  
        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        /// <summary>  
        /// Finds the specified predicate.  
        /// </summary>  
        /// <param name="predicate">The predicate.</param>  
        /// <returns></returns>  
        public IEnumerable<TEntity> Find(
            Expression<Func<TEntity, bool>> predicate
        )
        {
            return Entities.Where(predicate);
        }

        /// <summary>  
        /// Singles the or default.  
        /// </summary>  
        /// <param name="predicate">The predicate.</param>  
        /// <returns></returns>  
        public TEntity SingleOrDefault(
            Expression<Func<TEntity, bool>> predicate
        )
        {
            return Entities.Where(predicate).SingleOrDefault();
        }

        /// <summary>  
        /// First the or default.  
        /// </summary>  
        /// <returns></returns>  
        public TEntity FirstOrDefault()
        {
            return Entities.FirstOrDefault();
        }

        /// <summary>
        /// Get by ids
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual TEntity GetById(
            params object[] ids
        )
        {
            return Entities.Find(ids);
        }

        /// <summary>
        /// Add item
        /// </summary>
        /// <param name="entity"></param>
        public void Add(
            TEntity entity
        )
        {
            Entities.Add(entity);
        }

        /// <summary>
        /// Add items
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(
            IEnumerable<TEntity> entities
        )
        {
            Entities.AddRange(entities);
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(
            TEntity entity
        )
        {
            Attach(entity);

            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Update items
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateRange(
            IEnumerable<TEntity> entities
        )
        {
            Entities.UpdateRange(entities);
        }

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(
            TEntity entity
        )
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                Attach(entity);
            }

            Entities.Remove(entity);
        }

        /// <summary>
        /// Delete items
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRange(
            IEnumerable<TEntity> entities
        )
        {
            Entities.RemoveRange(entities);
        }

        /// <summary>
        /// Attach
        /// </summary>
        /// <param name="entity"></param>
        public void Attach(
            TEntity entity
        )
        {
            var entry = _dbContext.Entry(entity);

            if (entry != null)
            {
                entry.State = EntityState.Detached;
            }
        }

        /// <summary>
        /// Detach
        /// </summary>
        /// <param name="entity"></param>
        public void Detach(
            TEntity entity
        )
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                Entities.Attach(entity);
            }
        }

        /// <summary>
        /// LoadCollection
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="collectionProperty"></param>
        public void LoadCollection<TProperty>(
            TEntity entity, Expression<Func<TEntity, 
            IEnumerable<TProperty>>> collectionProperty
        ) 
            where TProperty : class
        {
            Attach(entity);

            var collection = _dbContext.Entry(entity).Collection(collectionProperty);

            if (!collection.IsLoaded)
            {
                collection.Load();
            }
        }

        /// <summary>
        /// LoadReference
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="referenceProperty"></param>
        public void LoadReference<TProperty>(
            TEntity entity, 
            Expression<Func<TEntity, TProperty>> referenceProperty
        ) 
            where TProperty : class
        {
            Attach(entity);

            var reference = _dbContext.Entry(entity).Reference(referenceProperty);

            if (!reference.IsLoaded)
            {
                reference.Load();
            }
        }

        #endregion

        #region IRepositoryAsynchronously

        /// <summary>  
        /// Initializes a new instance of the <see cref="SqlRepository{TEntity}"/> class.  
        /// Note that here I've stored Context.Set<TEntity>() in the constructor and store it in a private field like _entities.   
        /// This way, the implementation  of our methods would be cleaner:  
        /// _entities.ToList();  
        /// _entities.Where();  
        /// _entities.SingleOrDefault();  
        /// </summary>
        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            CancellationToken cancellationToken = default
        )
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(cancellationToken);
            }
            else
            {
                return await query.ToListAsync(cancellationToken);
            }
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="SqlRepository{TEntity}"/> class.  
        /// Note that here I've stored Context.Set<TEntity>() in the constructor and store it in a private field like _entities.   
        /// This way, the implementation  of our methods would be cleaner:  
        /// _entities.ToList();  
        /// _entities.Where();  
        /// _entities.SingleOrDefault();  
        /// </summary>
        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            CancellationToken cancellationToken = default,
            params Expression<Func<TEntity, object>>[] includes
        )
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes.Any())
            {
                query = query.IncludeMultiple(includes);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync(cancellationToken);
            }
            else
            {
                return await query.ToListAsync(cancellationToken);
            }
        }

        /// <summary>  
        /// Gets all.  
        /// </summary>  
        /// <returns></returns>  
        public async Task<IEnumerable<TEntity>> GetAllAsync(
            CancellationToken cancellationToken = default
        )
        {
            return await Entities.ToListAsync(cancellationToken);
        }

        /// <summary>  
        /// Singles the or default.  
        /// </summary>  
        /// <param name="predicate">The predicate.</param>  
        /// <returns></returns>  
        public async Task<TEntity> SingleOrDefaultAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default
        )
        {
            return await Entities.Where(predicate).SingleOrDefaultAsync(cancellationToken);
        }

        /// <summary>  
        /// First the or default.  
        /// </summary>  
        /// <returns></returns>  
        public async Task<TEntity> FirstOrDefaultAsync(
            CancellationToken cancellationToken = default
        )
        {
            return await Entities.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Get by ids
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(
            CancellationToken cancellationToken = default,
            params object[] ids
        )
        {
            return await Entities.FindAsync(ids, cancellationToken);
        }

        /// <summary>
        /// Add item
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task AddAsync(
            TEntity entity, 
            CancellationToken cancellationToken = default
        )
        {
            await Entities.AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Add items
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task AddRangeAsync(
            IEnumerable<TEntity> entities, 
            CancellationToken cancellationToken = default
        )
        {
            await Entities.AddRangeAsync(entities, cancellationToken);
        }

        /// <summary>
        /// LoadCollection
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="collectionProperty"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task LoadCollectionAsync<TProperty>(
            TEntity entity, Expression<Func<TEntity, 
            IEnumerable<TProperty>>> collectionProperty, 
            CancellationToken cancellationToken = default
        ) 
            where TProperty : class
        {
            Attach(entity);

            var collection = _dbContext.Entry(entity).Collection(collectionProperty);

            if (!collection.IsLoaded)
            {
                await collection.LoadAsync(cancellationToken);
            }
        }

        /// <summary>
        /// LoadReference
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="entity"></param>
        /// <param name="referenceProperty"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task LoadReferenceAsync<TProperty>(
            TEntity entity, 
            Expression<Func<TEntity, TProperty>> referenceProperty, 
            CancellationToken cancellationToken = default
        )
            where TProperty : class
        {
            Attach(entity);

            var reference = _dbContext.Entry(entity).Reference(referenceProperty);

            if (!reference.IsLoaded)
            {
                await reference.LoadAsync(cancellationToken);
            }
        }

        #endregion
    }
}
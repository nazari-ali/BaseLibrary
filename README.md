# Base Library
This library consists of several sections, which are:

### Prerequires:
- .NetCore 5.0.2
- MongoDB
- GridFs

### Sql
- [DbContext](#the-first-part-is-sqldbcontext)
- Repository
- UnitOfWork

### Mongo
- DbContext
- Repository
- UnitOfWork
- GridFs

### Tools
- Extension
  - Reflection
  - Linq
  - ModelBuilder
  - Enum
  - DateTime
  - Exception
  - Identity
  - Secrity
  - Json
  - String
  - Stream
  - Object

- Utilities
  - Download
  - File
  - HttpClient
  - ServiceLocator
  - Utility

### Other
- ApiResponse
- ExceptionMiddleware
- DependencyInjection

# Sql Descrption
The Sql project consists of 3 parts, 

# The first part is SqlDbContext. 

Inherited from the DbContext class, the SqlDbContext class has two components in this class, one of which is internal and not accessible outside the project, and the second, along with DbContextOption, takes an array from the Assembly. OnModelCreating is used.

### The OnModelCreating method does a few important things:
- Dynamicaly register all Entities that inherit from specific BaseType then not inherit from interface
  > *Classes posted in assemblies are in the constructor, and inherit from ISqlEntity.*
- Dynamicaly load all IEntityTypeConfiguration with Reflection
  > *Classes posted in assemblies are in the constructor.*
- Set DeleteBehavior.Restrict by default for relations
- Set NEWSEQUENTIALID() sql function for all columns named "Id"
- Pluralizing table name like Post to Posts or Person to People

> *Also done in this class for SaveChanges method and overriding.*

# Sample

### Base Model
```c#
public interface ISqlEntity
{
    
}
    
public abstract class SqlEntity<TKey> : ISqlEntity
{
    public virtual TKey Id  { get; set; }
}

public abstract class SqlEntity : SqlEntity<Guid>
{
    public override Guid Id 
    {
        get => base.Id == default ? Guid.NewGuid() : base.Id; 
        set => base.Id = value; 
    }
}
```

### Model

```c#
    
public class Quality : SqlEntity
{
    public string Name { get; set; }
    public FileType FileType { get; set; }
    public decimal Size { get; set; }
    public string Extension { get; set; }
    public QualityType QualityType { get; set; }
    public Guid ProductId { get; set; }

    public Media Media { get; set; }
}

public class ProductFileConfiguration : IEntityTypeConfiguration<Quality>
{
    public void Configure(EntityTypeBuilder<Quality> builder)
    {
        builder.HasOne(p => p.Media).WithMany(c => c.Qualities).HasForeignKey(p => p.ProductId);

        builder.Property(p => p.Name)
            .HasMaxLength(200);

        builder.Property(p => p.Extension)
            .HasMaxLength(10);
    }
}
```

### DbContext

``` c#
public class AppDbContext : SqlDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options, typeof(Product).Assembly)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Table per Hierarchy (TPH)
        modelBuilder.Entity<Product>();
        modelBuilder.Entity<LocalizationName>();
        modelBuilder.Entity<Image>();
        modelBuilder.Entity<Tag>();
        modelBuilder.Entity<GenreItem>();

        modelBuilder.Seed();

        base.OnModelCreating(modelBuilder);
    }
}
```

# The second part is Repository, 

in which the main and widely used methods are implemented, which are as follows.

### Repository Interface

```c#
public interface ISqlRepository<TEntity> : ISqlRepositorySynchronously<TEntity>, ISqlRepositoryAsynchronously<TEntity>, ISqlEntity where TEntity : class
{
    
}

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
```

### Repository Implementation

```c#
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

```

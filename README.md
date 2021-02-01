# Base Library
This library consists of several sections, which are:

### Prerequires:
- .NetCore 5.0.2
- MongoDB
- GridFs

### BaseLibrary.Sql
- [DbContext](#the-first-part-is-sqldbcontext)
- [Repository](#the-second-part-is-repository)
- [UnitOfWork](#the-third-part-is-unitofwork)

### BaseLibrary.Mongo
- DbContext
- Repository
- UnitOfWork
- GridFs

### BaseLibrary.Tool
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

# The first part is SqlDbContext,

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

```c#
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

        modelBuilder.RegisterAllEntitiesWithoutInheritFromInterface<ISqlEntity>(_assemblies);
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
```

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

### appsettings.json

```json
{
  "ConnectionStrings": {
    "SqlServer": "Data Source=.;Initial Catalog=Tanino;Integrated Security=true;App=EF807;"
  }
}
```

### Startup Project

>*Put in method ConfigureServices.*

```c#
services.AddDbContextPool<AppDbContext>(options => 
    options.UseSqlServer(Configuration.GetConnectionString("SqlServer"))
);
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

# Sample

```c#
public interface IGenreRepository : ISqlRepository<Genre>
{
}

public class GenreRepository : SqlRepository<Genre>, IGenreRepository
{
    public GenreRepository(AppDbContext dbContext) : base(dbContext)
    {
        
    }
}
```

# The third part is UnitOfWork, 
in which the storage methods and how to access the Repositories are implemented.

### UnitOfWork Interface
```c#
public interface ISqlUnitOfWork : ISqlUnitOfWorkSynchronously, ISqlUnitOfWorkAsynchronously, IDisposable
{
    
}

public interface ISqlUnitOfWorkSynchronously
{
    ISqlRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class;

    void SaveChanges();
    bool SaveChangesTransaction();
}

public interface ISqlUnitOfWorkAsynchronously
{
    Task SaveChangesAsync();
    Task<bool> SaveChangesTransactionAsync();
}
```

### UnitOfWork Implementation
```c#
public class SqlUnitOfWork : ISqlUnitOfWork
{
    protected readonly SqlDbContext Context;
    private readonly Dictionary<Type, object> _repositories;

    public SqlUnitOfWork(DbContext dbContext)
    {
        Context = (SqlDbContext)dbContext;
        _repositories = new Dictionary<Type, object>();
    }

    /// <summary>
    /// SaveChanges
    /// </summary>
    /// <returns></returns>
    public void SaveChanges()
    {
        Context.SaveChanges();
    }

    /// <summary>
    /// SaveChanges
    /// </summary>
    /// <returns></returns>
    public Task SaveChangesAsync()
    {
        return Context.SaveChangesAsync();
    }

    /// <summary>
    /// SaveChanges Transaction
    /// for all command
    /// </summary>
    /// <returns></returns>
    public bool SaveChangesTransaction()
    {
        bool returnValue = true;
        using var dbContextTransaction = Context.Database.BeginTransaction();

        try
        {
            Context.SaveChanges();
            dbContextTransaction.Commit();
        }
        catch (Exception)
        {
            //Log Exception Handling message                      
            returnValue = false;
            dbContextTransaction.Rollback();
        }

        return returnValue;
    }

    /// <summary>
    /// SaveChanges Transaction
    /// for all command
    /// </summary>
    /// <returns></returns>
    public async Task<bool> SaveChangesTransactionAsync()
    {
        bool returnValue = true;
        await using var dbContextTransaction = await Context.Database.BeginTransactionAsync();

        try
        {
            await Context.SaveChangesAsync();
            await dbContextTransaction.CommitAsync();
        }
        catch (Exception)
        {
            //Log Exception Handling message                      
            returnValue = false;
            await dbContextTransaction.RollbackAsync();
        }

        return returnValue;
    }

    /// <summary>
    /// Create new repository for entities
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public ISqlRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class
    {
        // Checks if the Dictionary Key contains the Model class
        if (_repositories.Keys.Contains(typeof(TEntity)))
        {
            // Return the repository for that Model class
            return _repositories[typeof(TEntity)] as ISqlRepository<TEntity>;
        }

        // If the repository for that Model class doesn't exist, create it
        var repository = new SqlRepository<TEntity>(Context);

        // Add it to the dictionary
        _repositories.Add(typeof(TEntity), repository);

        return repository;
    }

    #region IDisposable Support  

    // To detect redundant calls  
    private bool _disposedValue = false; 

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;

        if (disposing)
        {
            //dispose managed state (managed objects).  
            Context.Dispose();
        }

        // free unmanaged resources (unmanaged objects) and override a finalizer below.  
        // set large fields to null.  

        _disposedValue = true;
    }

    /// <summary>
    /// override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.  
    /// </summary>
    ~SqlUnitOfWork()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.  
        Dispose(false);
    }

    /// <summary>
    /// This code added to correctly implement the disposable pattern.  
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.  
        Dispose(true);

        // uncomment the following line if the finalizer is overridden above.  
        GC.SuppressFinalize(this);  
    }

    #endregion
}
```

# Sample

```c#
public interface IUnitOfWork : ISqlUnitOfWork
{
    GenreRepository Genres { get; }
    ISqlRepository<Lyric> Lyrics { get; }
    ISqlRepository<Quality> Qualities { get; }
}

public class UnitOfWork : SqlUnitOfWork, IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public UnitOfWork(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    private GenreRepository _genreRepository;
    public GenreRepository Genres => _genreRepository ??= new GenreRepository(_dbContext);
    
    public ISqlRepository<Lyric> Lyrics => GetRepository<Lyric>();
    
    public ISqlRepository<Quality> Qualities => GetRepository<Quality>();
}
```

### Startup Project 

>*Put in method ConfigureServices.*

```c#
services.AddTransient<IUnitOfWork, UnitOfWork>();
```

# Mongo Descrption
The Mongo project consists of 4 parts, 

# The first part is MongoContext,

In Class MongoContext, access to the Mongo is implemented through both the tcp and socket. Access to the bucket for GridFs is also done in this section.

To connect the Mongo, two modes, Tcp and Socket, are used, which uses the open and closed principle, and the two classes inherit from an interface and implement it, the codes of which are as follows:

### Interface 

```c#
public interface IMongoConnection
{
    ConnectionType Type { get; }
    IMongoClient GetMongoClient(IMongoDbSettings settings);
}
```

### Implementation

```c#
public class MongoTcpConnection : IMongoConnection
{
    public ConnectionType Type => ConnectionType.Tcp;

    public IMongoClient GetMongoClient(IMongoDbSettings settings)
    {
        return new MongoClient(settings.ConnectionString);
    }
}

public class MongoUnixSocketConnection : IMongoConnection
{
    public ConnectionType Type => ConnectionType.UnixSocket;

    public IMongoClient GetMongoClient(IMongoDbSettings settings)
    {
        var unixSocketPath = settings.ConnectionString.Split('@')[1].Split('/')[0];
        settings.ConnectionString.Replace(unixSocketPath, WebUtility.UrlEncode(unixSocketPath));

        var mongoUrl = MongoUrl.Create(settings.ConnectionString);

        var socketSettings = MongoClientSettings.FromUrl(mongoUrl);
        socketSettings.MaxConnectionPoolSize = 100000;
        socketSettings.RetryReads = true;
        socketSettings.RetryWrites = true;

        return new MongoClient(socketSettings.WithUnixDomainSockets());
    }
}
```

### MongoContext

```c#
public class MongoContext : IMongoContext
{
    private readonly IMongoDatabase _database;
    private readonly IClientSessionHandle _session;
    private readonly IGridFSBucket _bucket;
    private List<Action> _commands;

    public MongoContext(IMongoDbSettings settings)
    {
        // Every command will be stored and it'll be processed at SaveChanges
        _commands = new List<Action>();

        var mongoConnections = ReflectionExtensions.InstantiateClass<IMongoConnection>();

        var mongoDbClient = mongoConnections.Single(c => c.Type == settings.ConnectionType).GetMongoClient(settings);

        _database = mongoDbClient.GetDatabase(settings.DatabaseName);

        // bucket settings for gridFS
        _bucket = new GridFSBucket(_database, new GridFSBucketOptions
        {
            BucketName = settings.GridFsSettings.BucketName,
            ChunkSizeBytes = settings.GridFsSettings.BucketSize,
            WriteConcern = WriteConcern.WMajority,
            ReadPreference = ReadPreference.Primary
        });

        _session = mongoDbClient.StartSession();
    }

    public IClientSessionHandle Session => _session;
    public IGridFSBucket Bucket => _bucket;

    /// <summary>
    /// Get collection
    /// If the parameter is sent, the parameter is considered, otherwise it extracts the name based on the type of TDocument that was sent.
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public IMongoCollection<TDocument> GetCollection<TDocument>(string name = null)
    {
        var collectionName = name ?? MongoHelpers.GetCollectionName(typeof(TDocument));

        return _database.GetCollection<TDocument>(collectionName);
    }

    /// <summary>
    /// Add command
    /// </summary>
    /// <param name="func"></param>
    public async Task AddCommand(Action func)
    {
        _commands.Add(func);
    }

    /// <summary>
    /// SaveChanges
    /// </summary>
    /// <returns></returns>
    public int SaveChanges()
    {
        foreach (var a in _commands) a();

        return _commands.Count;
    }

    /// <summary>
    /// Dispose session, clear commands
    /// </summary>
    public void Dispose()
    {
        _commands.Clear();

        GC.SuppressFinalize(this);
    }
}
```

### appsettings.json

```json
{
  "MongoDbSettings": {
    "ConnectionType": 0,
    "ConnectionString": "mongodb://database:password@localhost:27017/?authSource=database&replicaSet=rs0&readPreference=primary&appname=MongoDB%20Compass&ssl=false",
    "DatabaseName": "database",
    "MongoClientSettings": null,
    "GridFsSettings": {
      "BucketName": "fs",
      "BucketSize": 262144
    }
  }
}
```

### Startup Project

>*Put in method ConfigureServices.*

```c#
services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

services.AddSingleton<IMongoContext, MongoContext>();
```

# The second part is Repository, 

in which the main and widely used methods are implemented, which are as follows.

### Repository Interface

```c#
public interface IMongoRepository<TDocument> : IMongoRepositorySynchronously<TDocument>, IMongoRepositoryAsynchronously<TDocument> where TDocument : class
{
    
}

public interface IMongoRepositorySynchronously<TDocument> 
    where TDocument : class
{
    IQueryable<TDocument> AsQueryable();

    IEnumerable<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression
    );

    IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression
    );

    TDocument FindOne(
        Expression<Func<TDocument, bool>> filterExpression
    );

    TDocument FindById(
        string objectId
    );

    void InsertOne(
        TDocument document
    );

    void InsertMany(
        ICollection<TDocument> documents
    );

    void ReplaceOne(
        TDocument document
    );

    void DeleteOne(
        Expression<Func<TDocument, bool>> filterExpression
    );

    void DeleteById(
        string objectId
    );

    void DeleteMany(
        Expression<Func<TDocument, bool>> filterExpression
    );
}

public interface IMongoRepositoryAsynchronously<TDocument> 
    where TDocument : class
{
    Task<TDocument> FindOneAsync(
        Expression<Func<TDocument, bool>> filterExpression,
        CancellationToken cancellationToken = default
    );

    Task<TDocument> FindByIdAsync(
        string objectId,
        CancellationToken cancellationToken = default
    );

    Task InsertOneAsync(
        TDocument document,
        CancellationToken cancellationToken = default
    );

    Task InsertManyAsync(
        ICollection<TDocument> documents,
        CancellationToken cancellationToken = default
    );

    Task ReplaceOneAsync(
        TDocument document,
        CancellationToken cancellationToken = default
    );

    Task DeleteOneAsync(
        Expression<Func<TDocument, bool>> filterExpression,
        CancellationToken cancellationToken = default
    );

    Task DeleteByIdAsync(
        string objectId,
        CancellationToken cancellationToken = default
    );

    Task DeleteManyAsync(
        Expression<Func<TDocument, bool>> filterExpression,
        CancellationToken cancellationToken = default
    );
}
```

### Repository Implementation

```c#
public class MongoRepository<TDocument> : IMongoRepository<TDocument>
        where TDocument : class
{
    protected readonly IMongoCollection<TDocument> Collection;
    private readonly IMongoContext _mongoContext;

    public MongoRepository(IMongoContext mongoContext)
    {
        _mongoContext = mongoContext;
        Collection = mongoContext.GetCollection<TDocument>();
    }

    #region IRepositorySynchronously

    public virtual IQueryable<TDocument> AsQueryable()
    {
        return Collection.AsQueryable();
    }

    public virtual IEnumerable<TDocument> FilterBy(
        Expression<Func<TDocument, bool>> filterExpression
    )
    {
        return Collection.Find(filterExpression).ToEnumerable();
    }

    public virtual IEnumerable<TProjected> FilterBy<TProjected>(
        Expression<Func<TDocument, bool>> filterExpression,
        Expression<Func<TDocument, TProjected>> projectionExpression
    )
    {
        return Collection.Find(filterExpression).Project(projectionExpression).ToEnumerable();
    }

    public virtual TDocument FindOne(
        Expression<Func<TDocument, bool>> filterExpression
    )
    {
        return Collection.Find(filterExpression).FirstOrDefault();
    }

    public virtual TDocument FindById(
        string objectId
    )
    {
        var filter = Builders<TDocument>.Filter.Eq("_id", objectId.ToObjectId());
        return Collection.Find(filter).SingleOrDefault();
    }

    public virtual void InsertOne(
        TDocument document
    )
    {
        _mongoContext.AddCommand(() =>
            Collection.InsertOne(_mongoContext.Session, document)
        );
    }

    public void InsertMany(
        ICollection<TDocument> documents
    )
    {
        _mongoContext.AddCommand(() =>
            Collection.InsertMany(_mongoContext.Session, documents)
        );
    }

    public void ReplaceOne(
        TDocument document
    )
    {
        var filter = Builders<TDocument>.Filter.Eq("_id", document.GetId());

        _mongoContext.AddCommand(() =>
            Collection.FindOneAndReplace(_mongoContext.Session, filter, document)
        );
    }

    public void DeleteOne(
        Expression<Func<TDocument, bool>> filterExpression
    )
    {
        _mongoContext.AddCommand(() =>
            Collection.FindOneAndDelete(_mongoContext.Session, filterExpression)
        );
    }

    public void DeleteById(
        string objectId
    )
    {
        var filter = Builders<TDocument>.Filter.Eq("_id", objectId.ToObjectId());

        _mongoContext.AddCommand(() =>
            Collection.FindOneAndDelete(_mongoContext.Session, filter)
        );
    }

    public void DeleteMany(
        Expression<Func<TDocument, bool>> filterExpression
    )
    {
        _mongoContext.AddCommand(() =>
            Collection.DeleteMany(_mongoContext.Session, filterExpression)
        );;
    }

    #endregion

    #region IRepositoryAsynchronously

    public virtual Task<TDocument> FindOneAsync(
        Expression<Func<TDocument, bool>> filterExpression,
        CancellationToken cancellationToken = default
    )
    {
        return Task.Run(() => Collection.Find(filterExpression).FirstOrDefaultAsync(cancellationToken));
    }

    public virtual Task<TDocument> FindByIdAsync(
        string objectId,
        CancellationToken cancellationToken = default
    )
    {
        return Task.Run(() =>
        {
            var filter = Builders<TDocument>.Filter.Eq("_id", objectId.ToObjectId());

            return Collection.Find(filter).SingleOrDefaultAsync(cancellationToken);
        });
    }

    public virtual Task InsertOneAsync(
        TDocument document,
        CancellationToken cancellationToken = default
    )
    {
        return _mongoContext.AddCommand(() =>
            Collection.InsertOneAsync(_mongoContext.Session, document, null, cancellationToken)
        );
    }

    public virtual Task InsertManyAsync(
        ICollection<TDocument> documents,
        CancellationToken cancellationToken = default
    )
    {
        return _mongoContext.AddCommand(() =>
            Collection.InsertManyAsync(_mongoContext.Session, documents, null, cancellationToken)
        );
    }

    public virtual Task ReplaceOneAsync(
        TDocument document,
        CancellationToken cancellationToken = default
    )
    {
        var filter = Builders<TDocument>.Filter.Eq("_id", document.GetId());

        return _mongoContext.AddCommand(() =>
            Collection.FindOneAndReplaceAsync(_mongoContext.Session, filter, document, null, cancellationToken)
        );
    }

    public Task DeleteOneAsync(
        Expression<Func<TDocument, bool>> filterExpression,
        CancellationToken cancellationToken = default
    )
    {
        return _mongoContext.AddCommand(() =>
            Collection.FindOneAndDeleteAsync(_mongoContext.Session, filterExpression, null, cancellationToken)
        );
    }

    public Task DeleteByIdAsync(
        string objectId,
        CancellationToken cancellationToken = default
    )
    {
        var filter = Builders<TDocument>.Filter.Eq("_id", objectId.ToObjectId());

        return _mongoContext.AddCommand(() =>
            Collection.FindOneAndDeleteAsync(_mongoContext.Session, filter, null, cancellationToken)
        );
    }

    public Task DeleteManyAsync(
        Expression<Func<TDocument, bool>> filterExpression,
        CancellationToken cancellationToken = default
    )
    {
        return _mongoContext.AddCommand(() =>
            Collection.DeleteManyAsync(_mongoContext.Session, filterExpression, null, cancellationToken)
        );
    }

    #endregion
}
```

# Sample

```c#
public interface IProductRepository : IMongoRepository<Product>
{
}

public class ProductRepository : MongoRepository<Product>, IProductRepository
{
    public ProductRepository(IMongoContext mongoContext) : base(mongoContext)
    {

    }
}
```

# The third part is UnitOfWork, 
in which the storage methods and how to access the Repositories are implemented.

### UnitOfWork Interface

```c#
public interface IMongoUnitOfWork : IDisposable
{
    IMongoRepository<TDocument> GetRepository<TDocument>()
        where TDocument : class;

    void SaveChanges();
    bool SaveChangesTransaction();
    Task<bool> SaveChangesTransactionAsync();
}
```

### UnitOfWork Implementation

```c#
public class MongoUnitOfWork : IMongoUnitOfWork
{
    protected readonly IMongoContext MongoContext;
    private readonly Dictionary<Type, object> _repositories;

    public MongoUnitOfWork(IMongoContext mongoContext)
    {
        MongoContext = mongoContext;
        _repositories = new Dictionary<Type, object>();
    }

    /// <summary>
    /// SaveChanges
    /// </summary>
    /// <returns></returns>
    public void SaveChanges()
    {
        MongoContext.SaveChanges();
    }

    /// <summary>
    /// SaveChanges Transaction
    /// for all command
    /// </summary>
    /// <returns></returns>
    public bool SaveChangesTransaction()
    {
        bool returnValue = true;
        MongoContext.Session.StartTransaction();

        try
        {
            SaveChanges();
            MongoContext.Session.CommitTransaction();
        }
        catch (Exception)
        {
            returnValue = false;
            MongoContext.Session.AbortTransaction();
        }

        return returnValue;
    }

    /// <summary>
    /// SaveChanges Transaction
    /// for all command
    /// </summary>
    /// <returns></returns>
    public async Task<bool> SaveChangesTransactionAsync()
    {
        bool returnValue = true;
        MongoContext.Session.StartTransaction();

        try
        {
            SaveChanges();
            await MongoContext.Session.CommitTransactionAsync();
        }
        catch (Exception)
        {
            returnValue = false;
            await MongoContext.Session.AbortTransactionAsync();
        }

        return returnValue;
    }

    /// <summary>
    /// Create new repository for entities
    /// </summary>
    /// <typeparam name="TDocument"></typeparam>
    /// <returns></returns>
    public IMongoRepository<TDocument> GetRepository<TDocument>()
        where TDocument : class
    {
        // Checks if the Dictionary Key contains the Model class
        if (_repositories.Keys.Contains(typeof(TDocument)))
        {
            // Return the repository for that Model class
            return _repositories[typeof(TDocument)] as IMongoRepository<TDocument>;
        }

        // If the repository for that Model class doesn't exist, create it
        var repository = new MongoRepository<TDocument>(MongoContext);

        // Add it to the dictionary
        _repositories.Add(typeof(TDocument), repository);

        return repository;
    }

    #region IDisposable Support  

    // To detect redundant calls  
    private bool _disposedValue = false;

    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;

        if (disposing)
        {
            //dispose managed state (managed objects).  
            MongoContext.Dispose();
        }

        // free unmanaged resources (unmanaged objects) and override a finalizer below.  
        // set large fields to null.  

        _disposedValue = true;
    }

    /// <summary>
    /// override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.  
    /// </summary>
    ~MongoUnitOfWork()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.  
        Dispose(false);
    }

    /// <summary>
    /// This code added to correctly implement the disposable pattern.  
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in Dispose(bool disposing) above.  
        Dispose(true);

        // uncomment the following line if the finalizer is overridden above.  
        GC.SuppressFinalize(this);
    }

    #endregion
}
```

# Sample

```c#
public interface IUnitOfWork : IMongoUnitOfWork
{
    ProductRepository Products { get; }
    ISqlRepository<Lyric> Lyrics { get; }
}

public class UnitOfWork : MongoUnitOfWork, IUnitOfWork
{
    private readonly IMongoContext _mongoContext;

    public UnitOfWork(IMongoContext mongoContext) : base(mongoContext)
    {
        _mongoContext = mongoContext;
    }

    private ProductRepository _productRepository;
    public ProductRepository Products => _productRepository ??= new ProductRepository(_mongoContext);
    
    public IMongoRepository<Lyric> Lyrics => GetRepository<Lyric>();
}
```

### Startup Project 

>*Put in method ConfigureServices.*

```c#
services.AddTransient<IUnitOfWork, UnitOfWork>();
```

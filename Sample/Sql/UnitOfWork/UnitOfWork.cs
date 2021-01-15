using BaseLibrary.Sql;
using Sample.Sql.Persistence;
using Sample.Sql.Repositories;
using Sample.Sql.UnitOfWork;

namespace Sample.Sql.UnitOfWork
{
    public class UnitOfWork : SqlUnitOfWork, IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private GenreRepository _genreRepository;
        public GenreRepository Genres => _genreRepository ??= new GenreRepository(_dbContext);
    }
}
using BaseLibrary.Sql;
using Sample.Sql.Entities.GenreEntity;
using Sample.Sql.Persistence;
using Sample.Sql.Repositories.Interfaces;

namespace Sample.Sql.Repositories
{
    public class GenreRepository : SqlRepository<Genre>, IGenreRepository
    {
        public GenreRepository(AppDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}

using BaseLibrary.Sql.Interfaces;
using Sample.Sql.Entities.GenreEntity;

namespace Sample.Sql.Repositories.Interfaces
{
    public interface IGenreRepository : ISqlRepository<Genre>
    {
    }
}

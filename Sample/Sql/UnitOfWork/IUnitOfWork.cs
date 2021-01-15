using BaseLibrary.Sql.Interfaces;
using Sample.Sql.Repositories;

namespace Sample.Sql.UnitOfWork
{
    public interface IUnitOfWork : ISqlUnitOfWork
    {
        public GenreRepository Genres { get; }
    }
}
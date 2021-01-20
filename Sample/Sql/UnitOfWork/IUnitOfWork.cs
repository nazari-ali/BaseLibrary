using BaseLibrary.Sql.UnitOfWork.Interfaces;
using Sample.Sql.Repositories;

namespace Sample.Sql.UnitOfWork
{
    public interface IUnitOfWork : ISqlUnitOfWork
    {
        public GenreRepository Genres { get; }
    }
}
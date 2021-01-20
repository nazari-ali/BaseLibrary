using BaseLibrary.Mongo.UnitOfWork.Interfaces;
using Sample.Mongo.Repositories;

namespace Sample.Mongo.UnitOfWork
{
    public interface IUnitOfWork : IMongoUnitOfWork
    {
        ProductRepository Products { get; }
    }
}

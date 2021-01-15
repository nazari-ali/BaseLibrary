using BaseLibrary.MongoDB.Interfaces;
using Sample.Mongo.Entities;

namespace Sample.Mongo.Repositories.Interfaces
{
    public interface IProductRepository : IMongoRepository<Product>
    {
    }
}

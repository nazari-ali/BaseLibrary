using BaseLibrary.Mongo.DbContext.Interfaces;
using BaseLibrary.Mongo.UnitOfWork;
using Sample.Mongo.Repositories;

namespace Sample.Mongo.UnitOfWork
{
    public class UnitOfWork : MongoUnitOfWork, IUnitOfWork
    {
        private readonly IMongoContext _mongoContext;

        public UnitOfWork(IMongoContext mongoContext) : base(mongoContext)
        {
            _mongoContext = mongoContext;
        }

        private ProductRepository _productRepository;
        public ProductRepository Products => _productRepository ??= new ProductRepository(_mongoContext);
    }
}

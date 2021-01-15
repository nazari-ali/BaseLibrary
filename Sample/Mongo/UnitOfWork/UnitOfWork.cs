using BaseLibrary.MongoDB;
using BaseLibrary.MongoDB.Interfaces;
using Sample.Mongo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

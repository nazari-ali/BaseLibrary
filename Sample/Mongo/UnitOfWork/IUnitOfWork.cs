using BaseLibrary.MongoDB.Interfaces;
using Sample.Mongo.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Mongo.UnitOfWork
{
    public interface IUnitOfWork : IMongoUnitOfWork
    {
        ProductRepository Products { get; }
    }
}

using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Threading.Tasks;

namespace BaseLibrary.Mongo.DbContext.Interfaces
{
    public interface IMongoContext : IDisposable
    {
        IGridFSBucket Bucket { get; }
        IClientSessionHandle Session { get; }

        IMongoCollection<TDocument> GetCollection<TDocument>(string name = null);
        Task AddCommand(Action func);
        int SaveChanges();
    }
}

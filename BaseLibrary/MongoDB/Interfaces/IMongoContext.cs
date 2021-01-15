using BaseLibrary.Models.MongoDB;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB.Interfaces
{
    public interface IMongoContext : IDisposable
    {
        IMongoDatabase Database { get; }
        GridFsSettings GridFsSettings { get; }
        IClientSessionHandle Session { get; }

        IMongoCollection<TDocument> GetCollection<TDocument>(string name = null);
        Task AddCommand(Action func);
        int SaveChanges();
    }
}

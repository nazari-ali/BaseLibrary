using BaseLibrary.Mongo.Models;
using BaseLibrary.Mongo.Models.Interfaces;
using MongoDB.Driver;

namespace BaseLibrary.Mongo.DbContext.Interfaces
{
    public interface IMongoConnection
    {
        ConnectionType Type { get; }
        IMongoClient GetMongoClient(IMongoDbSettings settings);
    }
}

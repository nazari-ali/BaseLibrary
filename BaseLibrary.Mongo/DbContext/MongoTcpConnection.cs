using BaseLibrary.Mongo.DbContext.Interfaces;
using BaseLibrary.Mongo.Models;
using BaseLibrary.Mongo.Models.Interfaces;
using MongoDB.Driver;

namespace BaseLibrary.Mongo.DbContext.Connection
{
    public class MongoTcpConnection : IMongoConnection
    {
        public ConnectionType Type => ConnectionType.Tcp;

        public IMongoClient GetMongoClient(IMongoDbSettings settings)
        {
            return new MongoClient(settings.ConnectionString);
        }
    }
}

using MongoDB.Driver;

namespace BaseLibrary.Mongo.Models.Interfaces
{
    public interface IMongoDbSettings
    {
        ConnectionType ConnectionType { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        MongoClientSettings MongoClientSettings { get; set; }
        GridFsSettings GridFsSettings { get; set; }
    }
}
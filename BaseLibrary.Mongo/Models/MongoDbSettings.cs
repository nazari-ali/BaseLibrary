using BaseLibrary.Mongo.Models.Interfaces;
using MongoDB.Driver;

namespace BaseLibrary.Mongo.Models
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public ConnectionType ConnectionType { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public MongoClientSettings MongoClientSettings { get; set; }
        public GridFsSettings GridFsSettings { get; set; }
    }
}
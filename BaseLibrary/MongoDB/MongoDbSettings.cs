using BaseLibrary.Models.MongoDB;
using BaseLibrary.MongoDB.Interfaces;
using MongoDB.Driver;

namespace BaseLibrary.MongoDB
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
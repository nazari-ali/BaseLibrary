using BaseLibrary.Extensions;
using BaseLibrary.Models.MongoDB;
using BaseLibrary.MongoDB.Connection;
using BaseLibrary.MongoDB.Extensions;
using BaseLibrary.MongoDB.Interfaces;
using MongoDB.Driver;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB
{
    /// <summary>
    /// Using:
    ///     Add To appsettings.json
    ///     
    ///         "MongoDbSettings": {
    ///             "ConnectionType": ConnectionType.Tcp,
    ///             "ConnectionString": "mongodb://localhost:32772",
    ///             "DatabaseName": "ExampleDatabase",
    ///             "MongoClientSettings": { },
    ///             "GridFsSetting": {
    ///                 "BucketName": "fs",
    ///                 "BucketSize": 262144
    ///             }
    ///         }
    /// 
    /// </summary>
    public class MongoContext : IMongoContext
    {
        private readonly IMongoClient _mongoDbClient;
        private readonly IMongoDatabase _database;
        private readonly GridFsSettings _gridFsSetting;

        public IMongoClient MongoClient => _mongoDbClient;
        public IMongoDatabase Database => _database;
        public GridFsSettings GridFsSettings => _gridFsSetting;

        public MongoContext(IMongoDbSettings settings)
        {
            var mongoConnections = ReflectionExtensions.GetTypesAssignableFrom(typeof(IMongoConnection)).Cast<IMongoConnection>();

            _mongoDbClient = mongoConnections.Single(c => c.Type == settings.ConnectionType).GetMongoClient(settings);

            _database = _mongoDbClient.GetDatabase(settings.DatabaseName);

            // bucket settings for gridFS
            _gridFsSetting = settings.GridFsSettings;
        }

        /// <summary>
        /// Get collection
        /// If the parameter is sent, the parameter is considered, otherwise it extracts the name based on the type of TDocument that was sent.
        /// </summary>
        /// <typeparam name="TDocument"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public IMongoCollection<TDocument> GetCollection<TDocument>(string name = null)
        {
            var collectionName = name ?? MongoHelpers.GetCollectionName(typeof(TDocument));

            return _database.GetCollection<TDocument>(collectionName);
        }
    }
}
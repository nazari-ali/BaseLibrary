using BaseLibrary.Extensions;
using BaseLibrary.Models.MongoDB;
using BaseLibrary.MongoDB.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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
    ///             "GridFsSettings": {
    ///                 "BucketName": "fs",
    ///                 "BucketSize": 262144
    ///             }
    ///         }
    /// 
    /// </summary>
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database;
        private readonly GridFsSettings _gridFsSetting;
        private IClientSessionHandle _session;
        private List<Action> _commands;

        public MongoContext(IMongoDbSettings settings)
        {
            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Action>();

            var mongoConnections = ReflectionExtensions.InstantiateClass<IMongoConnection>();

            var mongoDbClient = mongoConnections.Single(c => c.Type == settings.ConnectionType).GetMongoClient(settings);

            _database = mongoDbClient.GetDatabase(settings.DatabaseName);

            // bucket settings for gridFS
            _gridFsSetting = settings.GridFsSettings;

            _session = mongoDbClient.StartSession();
        }

        public IMongoDatabase Database => _database;
        public GridFsSettings GridFsSettings => _gridFsSetting;
        public IClientSessionHandle Session => _session;

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

        /// <summary>
        /// Add command
        /// </summary>
        /// <param name="func"></param>
        public async Task AddCommand(Action func)
        {
            _commands.Add(func);
        }

        /// <summary>
        /// SaveChanges
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            foreach (var a in _commands) a();

            return _commands.Count;
        }

        /// <summary>
        /// Dispose session, clear commands
        /// </summary>
        public void Dispose()
        {
            _commands.Clear();

            GC.SuppressFinalize(this);
        }
    }
}
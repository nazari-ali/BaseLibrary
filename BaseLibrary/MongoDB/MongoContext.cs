using BaseLibrary.Models.MongoDB;
using BaseLibrary.MongoDB.Interfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB
{
    /// <summary>
    /// Using:
    ///     Add To appsettings.json
    ///     
    ///         "MongoDbSettings": {
    ///             "ConnectionString": "mongodb://localhost:32772",
    ///             "DatabaseName": "ExampleDatabase",
    ///             "BucketName": "fs",
    ///             "BucketSize": 262144
    ///         }
    /// 
    /// </summary>
    public class MongoContext : IMongoContext
    {
        private readonly IMongoClient _mongoDbClient;
        private readonly IMongoDatabase _database;
        private readonly BucketSetting _bucketSetting;

        public IMongoClient MongoClient => _mongoDbClient;
        public IMongoDatabase Database => _database;
        public BucketSetting BucketSettings => _bucketSetting;

        public MongoContext(IMongoDbSettings settings)
        {
            _mongoDbClient = new MongoClient(settings.ConnectionString);

            _database = _mongoDbClient.GetDatabase(settings.DatabaseName);

            // bucket settings for gridFS
            _bucketSetting = new BucketSetting
            {
                BucketName = settings.BucketName,
                BucketSize = settings.BucketSize
            };
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
using BaseLibrary.MongoDB.Interfaces;

namespace BaseLibrary.MongoDB
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public MongoDbSettings()
        {
            BucketName = "fs";
            BucketSize = 262144;
        }

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        /// <summary>
        /// Default: fs
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// Default: 262144
        /// </summary>
        public int BucketSize { get; set; }
    }
}
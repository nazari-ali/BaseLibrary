using BaseLibrary.Models.MongoDB;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BaseLibrary.MongoDB.Interfaces
{
    public interface IMongoContext
    {
        IMongoClient MongoClient { get; }
        IMongoDatabase Database { get; }
        GridFsSettings GridFsSettings { get; }

        IMongoCollection<TDocument> GetCollection<TDocument>(string name = null);
    }
}

namespace BaseLibrary.Mongo.Repository.Interfaces
{
    public interface IMongoRepository<TDocument> : IMongoRepositorySynchronously<TDocument>, IMongoRepositoryAsynchronously<TDocument> where TDocument : class
    {
        
    }
}
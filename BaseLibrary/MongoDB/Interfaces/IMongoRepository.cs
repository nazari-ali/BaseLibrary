namespace BaseLibrary.MongoDB.Interfaces
{
    public interface IMongoRepository<TDocument> : IMongoRepositorySynchronously<TDocument>, IMongoRepositoryAsynchronously<TDocument> where TDocument : IDocument
    {
        
    }
}
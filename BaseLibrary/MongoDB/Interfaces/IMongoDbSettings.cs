namespace BaseLibrary.MongoDB.Interfaces
{
    public interface IMongoDbSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string BucketName { get; set; }
        int BucketSize { get; set; }
    }
}
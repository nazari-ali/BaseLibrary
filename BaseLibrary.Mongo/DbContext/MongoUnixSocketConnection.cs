using BaseLibrary.Mongo.DbContext.Extensions;
using BaseLibrary.Mongo.DbContext.Interfaces;
using BaseLibrary.Mongo.Models;
using BaseLibrary.Mongo.Models.Interfaces;
using MongoDB.Driver;
using System.Net;

namespace BaseLibrary.Mongo.DbContext.Connection
{
    public class MongoUnixSocketConnection : IMongoConnection
    {
        public ConnectionType Type => ConnectionType.UnixSocket;

        public IMongoClient GetMongoClient(IMongoDbSettings settings)
        {
            var unixSocketPath = settings.ConnectionString.Split('@')[1].Split('/')[0];
            settings.ConnectionString.Replace(unixSocketPath, WebUtility.UrlEncode(unixSocketPath));

            var mongoUrl = MongoUrl.Create(settings.ConnectionString);

            var socketSettings = MongoClientSettings.FromUrl(mongoUrl);
            socketSettings.MaxConnectionPoolSize = 100000;
            socketSettings.RetryReads = true;
            socketSettings.RetryWrites = true;

            return new MongoClient(socketSettings.WithUnixDomainSockets());
        }
    }
}

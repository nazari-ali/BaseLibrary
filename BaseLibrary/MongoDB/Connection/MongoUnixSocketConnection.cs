using BaseLibrary.Models.MongoDB;
using BaseLibrary.MongoDB.Extensions;
using BaseLibrary.MongoDB.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BaseLibrary.MongoDB.Connection
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

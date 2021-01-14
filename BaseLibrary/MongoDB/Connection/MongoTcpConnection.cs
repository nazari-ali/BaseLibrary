using BaseLibrary.Models.MongoDB;
using BaseLibrary.MongoDB.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.MongoDB.Connection
{
    public class MongoTcpConnection : IMongoConnection
    {
        public ConnectionType Type => ConnectionType.Tcp;

        public IMongoClient GetMongoClient(IMongoDbSettings settings)
        {
            return new MongoClient(settings.ConnectionString);
        }
    }
}

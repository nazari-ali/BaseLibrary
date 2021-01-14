using BaseLibrary.Models.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseLibrary.MongoDB.Interfaces
{
    public interface IMongoConnection
    {
        ConnectionType Type { get; }
        IMongoClient GetMongoClient(IMongoDbSettings settings);
    }
}

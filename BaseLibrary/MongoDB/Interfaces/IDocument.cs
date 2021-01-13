using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace BaseLibrary.MongoDB.Interfaces
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }
    }
}
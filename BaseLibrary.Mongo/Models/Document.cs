using BaseLibrary.Mongo.Models.Interfaces;
using MongoDB.Bson;
using System;

namespace BaseLibrary.Mongo.Models
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
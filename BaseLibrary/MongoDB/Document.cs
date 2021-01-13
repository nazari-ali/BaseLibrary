using BaseLibrary.MongoDB.Interfaces;
using MongoDB.Bson;
using System;

namespace BaseLibrary.MongoDB
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
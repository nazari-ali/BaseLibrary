using BaseLibrary.Mongo.Attributes;
using System;
using System.Linq;

namespace BaseLibrary.Mongo.Helpers
{
    internal static class MongoHelpers
    {
        internal static string GetCollectionName(
            Type documentType
        )
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(
                    typeof(BsonCollectionAttribute),
                    true)
                .FirstOrDefault())?.CollectionName;
        }
    }
}
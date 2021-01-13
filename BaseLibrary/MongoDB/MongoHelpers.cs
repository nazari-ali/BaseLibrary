using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseLibrary.Attributes;

namespace BaseLibrary.MongoDB
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
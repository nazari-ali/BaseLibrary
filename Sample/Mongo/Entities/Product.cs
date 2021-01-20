using BaseLibrary.Mongo.Attributes;
using BaseLibrary.Mongo.Models;

namespace Sample.Mongo.Entities
{
    [BsonCollection("product")]
    public class Product : Document
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsCertificate { get; set; }
    }
}

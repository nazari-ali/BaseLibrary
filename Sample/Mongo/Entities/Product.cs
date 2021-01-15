using BaseLibrary.Attributes;
using BaseLibrary.MongoDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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

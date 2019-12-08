using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FASTSHOP.Api.Domain.Models
{
    public class Client
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Document { get; set; }
    }
}

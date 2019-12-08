using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FASTSHOP.Api.Domain.Models
{
    public class Order
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int Status { get; set; }
    }
}

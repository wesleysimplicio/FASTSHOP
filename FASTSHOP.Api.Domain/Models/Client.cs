using FASTSHOP.Api.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FASTSHOP.Api.Domain.Models
{
    public class Client
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long? Document { get; set; }
        [Required]
        public string Email { get; set; }
        public StatusEnum? Status { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}

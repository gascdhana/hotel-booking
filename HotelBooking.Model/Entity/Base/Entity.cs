﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace HotelBooking.Models.Entity.Base
{
    public abstract class Entity
    {
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("createdOn")]
        public DateTime CreatedOn { get; set; }

        [BsonElement("CreatedBy")]
        public string? CreatedBy { get; set; }

        [BsonElement("updatedOn")]
        public DateTime? UpdatedOn { get; set; }

        [BsonElement("UpdatedBy")]
        public string? UpdatedBy { get; set; }

        [BsonElement("isDeleted"), JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}

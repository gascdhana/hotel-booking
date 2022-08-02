using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace HotelBooking.Models.Entity
{
    public class Room : Base.Entity
    {
        [BsonElement("hotelId"), BsonRepresentation(BsonType.String), JsonIgnore]
        public string? HotelId { get; set; }

        [BsonElement("name"), Required(ErrorMessage ="Room Name should not be null or empty")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string? Description { get; set; }

        [BsonElement("capacity"), Range(1, int.MaxValue, ErrorMessage = "Room Capacity should be greater than one")]
        public int Capacity { get; set; }

        [BsonElement("basePrice"), Range(1, int.MaxValue, ErrorMessage = "Base Price should be greater than one")]
        public float BasePrice { get; set; }

    }
}

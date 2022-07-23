using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelBooking.Models.Entity
{
    public class Room : Base.Entity
    {
        [BsonElement("hotelId"), BsonRepresentation(BsonType.String)]
        public string HotelId { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("capacity")]
        public int Capacity { get; set; }

        [BsonElement("basePrice")]
        public float BasePrice { get; set; }

    }
}

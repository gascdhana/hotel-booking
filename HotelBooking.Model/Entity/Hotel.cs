using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models.Entity
{
    [BsonIgnoreExtraElements]
    public class Hotel : Base.Entity
    {
        [BsonElement("name"), Required(AllowEmptyStrings = false, ErrorMessage = "Hotel name cannot be null or empty")]
        public string? Name { get; set; }

        [BsonElement("city"), Required(AllowEmptyStrings = false, ErrorMessage = "Hotel city cannot be null or empty")]
        public string? City { get; set; }

        [BsonElement("address")]
        public string? Address { get; set; }

        [BsonElement("admins")]
        public List<string> Admins { get; set; } = new List<string>();
    }
}

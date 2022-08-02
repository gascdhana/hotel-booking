using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Models.Entity
{
    [BsonIgnoreExtraElements]
    public class Hotel : Base.Entity
    {
        /// <summary>
        /// Name of the Hotel
        /// </summary>
        [BsonElement("name"), Required(AllowEmptyStrings = false, ErrorMessage = "Hotel name cannot be null or empty")]
        public string Name { get; set; }

        /// <summary>
        /// City of the Hotel
        /// </summary>
        [BsonElement("city"), Required(AllowEmptyStrings = false, ErrorMessage = "Hotel city cannot be null or empty")]
        public string City { get; set; }

        /// <summary>
        /// Address of the hotel
        /// </summary>
        [BsonElement("address")]
        public string Address { get; set; }

        /// <summary>
        /// Hotel admin's email Ids
        /// </summary>
        [BsonElement("admins")]
        public List<string> Admins { get; set; } = new List<string>();
    }
}

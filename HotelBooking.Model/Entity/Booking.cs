using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HotelBooking.Models.Entity
{
    [BsonIgnoreExtraElements]
    public class Booking : Base.Entity
    {
        [BsonElement("hotelId"), BsonRepresentation(BsonType.ObjectId)]
        public string HotelId { get; set; }

        [BsonElement("roomId"), BsonRepresentation(BsonType.ObjectId)]
        public string RoomId { get; set; }

        [BsonElement("bookingEmailId"), Required(AllowEmptyStrings = false, ErrorMessage = "Booking email cannot be null or empty")]
        public string BookingEmaiId { get; set; }

        [BsonElement("StartDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("endDate")]
        public DateTime EndDate { get; set; }

        [BsonElement("guests")]
        public List<Guest> Guests { get; set; } = new List<Guest>();
        
        [BsonElement("receipt")]
        public Receipt Receipt { get; set; }

        [BsonElement("cancelReason")]
        public string CancelReason { get; set; }
    }

    public class Guest
    {
        [BsonElement("name")]
        public string? Name { get; set; }    

        [BsonElement("age")]
        public string? Age { get; set; }

        [BsonElement("gender"), BsonRepresentation(BsonType.String)]
        public Gender Gender { get; set; }  

        [BsonElement("isMainGuest")]
        public bool IsMainGuest { get; set; }

        [BsonElement("identification")]
        public GuestIdentification? Identification { get; set; }

    }

    public class GuestIdentification
    {
        [BsonElement("type"), BsonRepresentation(BsonType.String)]
        public IdProofType Type { get; set; }
        
        [BsonElement("idNumber")]
        public string? IdNumber { get; set; }
        
        [BsonElement("validity")]
        public DateTime? Validity { get; set; }
    }

    public enum IdProofType
    {
        None,
        PAN,
        Passport,
        AADHAAR,
        DrivingLicense
    }

    public enum Gender
    {
        Unknown, 
        Male,
        Female
    }

    public class Receipt
    {
        [BsonElement("paymentMode"), BsonRepresentation(BsonType.String)]
        public PaymentMode PaymentMode { get; set; }    

        [BsonElement("transactionDetails")]
        public string? TransactionDetails { get; set; }

        [BsonElement("discount")]
        public float Discount { get; set; }

        [BsonElement("totalPrice")]
        public float TotalPrice { get; set; }
    }

    public enum PaymentMode
    {
        None,
        Cash,
        CreditCard,
        UPI
    }
}

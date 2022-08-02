using HotelBooking.Models.Entity;

namespace HotelBooking.Data.Mongo.Repository
{
    public interface IBookingRepository
    {
        Task<List<Booking>> MyOpenBookings(string emailId, bool isHistory = false);

        Task CancelBooking(string id, string reason, string emailId);

        Task<List<Booking>> GetBookings(DateTime startDate, DateTime endDate);
    }
}

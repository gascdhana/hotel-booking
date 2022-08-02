using HotelBooking.Models.Entity;


namespace HotelBooking.Data.Mongo.Repository
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRoomsByHotel(IEnumerable<string> hotelIds);
    }
}
